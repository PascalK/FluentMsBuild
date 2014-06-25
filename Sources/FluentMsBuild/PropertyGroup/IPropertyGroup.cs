using System.Collections.Generic;

namespace FluentMsBuild
{
    public interface IPropertyGroup
    {
        IReadOnlyCollection<Property> Properties { get; }
        IReadOnlyCollection<Property> PropertiesReversed { get; }
        IPropertyActions<TPropertyValue> AddProperty<TPropertyValue>(string name);
        IPropertyActions<TPropertyValue> AddProperty<TPropertyValue>(string name, TPropertyValue unevaluatedValue);
        IPropertyActions<TPropertyValue> AddProperty<TPropertyValue>(string name, TPropertyValue value, string condition);
        IPropertyActions<TPropertyValue> AddProperty<TPropertyValue>(string name, TPropertyValue value, string condition, string label);
        IPropertyActions<TPropertyValue> SetProperty<TPropertyValue>(string name, TPropertyValue unevaluatedValue);
    }
}