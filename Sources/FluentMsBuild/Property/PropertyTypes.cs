using PK.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMsBuild
{
    /// <summary>
    /// An enumerated list of property types
    /// </summary>
    /// <typeparam name="T">The CLR type of the property</typeparam>
    public class PropertyTypes<T> : Enumerated<PropertyTypes<T>, Type>
    {
        private Func<T, string> toValue;

        /// <summary>
        /// Creates a new instance of PropertyTypes
        /// </summary>
        /// <param name="toValueFunction">The function to convert the CLR type to a string value used in a MsBuild (Project)file</param>
        public PropertyTypes(Func<T, string> toValueFunction)
            : base(typeof(T))
        {
            toValue = toValueFunction;
        }

        /// <summary>
        /// Gets the PropertyTypes instance based on type T
        /// </summary>
        /// <returns>The instance of PropertyTypes of type T</returns>
        public static PropertyTypes<T> Get()
        {
            return Get(typeof(T));
        }
        /// <summary>
        /// Get the PropertyTypes instance based on it's type
        /// </summary>
        /// <remarks>T and the value must be of the same type</remarks>
        /// <param name="value">The type of the PropertyTypes instance to get</param>
        /// <returns>The instance of PropertyTypes instance to get</returns>
        public new static PropertyTypes<T> Get(Type value)
        {
            PropertyTypes<T> predefinedPropertyType;

            predefinedPropertyType = Enumerated<PropertyTypes<T>, Type>.GetOrDefault(value);
            if (predefinedPropertyType == null)
            {
                predefinedPropertyType = new PropertyTypes<T>((v) => v.ToString());
            }

            return predefinedPropertyType;
        }
        /// <summary>
        /// Converts the CLR type to a MsBuild usable string
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>The converted string</returns>
        public string ToValue(T value)
        {
            return toValue(value);
        }
        /// <summary>
        /// PropertyTypes for string values
        /// </summary>
        public static PropertyTypes<string> String = new PropertyTypes<string>((value) => value);
        /// <summary>
        /// PropertyTypes for Guid values
        /// </summary>
        public static PropertyTypes<Guid> Guid = new PropertyTypes<Guid>(value => string.Format("{{{0}}}", value.ToString()));
        /// <summary>
        /// PropertyTypes for IEnumerable&lt;Guid&gt; values
        /// </summary>
        public static PropertyTypes<IEnumerable<Guid>> Guids = new PropertyTypes<IEnumerable<Guid>>(value => string.Join(";", value.Select(v => Guid.ToValue(v))));
    }
}
