using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// Represents an MSBuild project, a targets file, or any other file that conforms to MSBuild project file schema. This class and its related classes allow a complete MSBuild project or targets file to be read and written.
    /// </summary>
    public partial class Root : ElementContainer
    {
        /// <summary>
        /// The xml name of this element
        /// </summary>
        protected override string XmlName
        {
            get { return "Project"; }
        }
        /// <summary>
        /// Gets null because the Condition attribute is nonexistent for this element and a nonexistent condition is implicitly true.
        /// </summary>
        /// <remarks>Trying to set this property will result in an exception</remarks>
        /// <returns>Returns a null.</returns>
        public override string Condition
        {
            get { return null; }
            set
            {
                throw new InvalidOperationException("Can not set Condition.");
            }
        }
        string defaultTargets;
        /// <summary>
        /// Gets or sets the value of the DefaultTargets attribute.
        /// </summary>
        /// <returns>Returns the value of the DefaultTargets attribute. Returns an empty string if the attribute is not present.</returns>
        public string DefaultTargets
        {
            get { return defaultTargets ?? String.Empty; }
            set { defaultTargets = value; }
        }
        string fullPath;
        /// <summary>
        /// Gets the full path to the project file.
        /// </summary>
        /// <returns>Returns the full path to the project file. If the project is not loaded from disk, returns null.</returns>
        public string FullPath
        {
            get { return fullPath; }
            set
            {
                fullPath = Path.GetFullPath(value);
                DirectoryPath = Path.GetDirectoryName(fullPath);
            }
        }

        string directoryPath;
        /// <summary>
        /// Gets the directory path to the project file.
        /// </summary>
        /// <returns>Returns the directory path, which is never null. If the project is not loaded from disk, returns the current-directory at the time the project was loaded.</returns>
        public string DirectoryPath
        {
            get { return directoryPath ?? Directory.GetCurrentDirectory(); }
            set { directoryPath = value; }
        }
        /// <summary>
        /// Specifies the character encoding that the project file is to be saved in.
        /// </summary>
        /// <remarks>This has not yet been implemented and will always be UTF8</remarks>
        /// <returns>Returns the character encoding that the project file is to be saved in.</returns>
        public Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        /// <summary>
        /// Determines whether the project has been modified since it was last loaded or saved.
        /// </summary>
        /// <remarks>This has not yet been implemented and will always be true</remarks>
        /// <returns>Returns true if the project has been modified; false otherwise.</returns>
        public bool HasUnsavedChanges
        {
            get { return true; }
        }

        string initialTargets;
        /// <summary>
        /// Gets or sets the value of the InitialTargets attribute.
        /// </summary>
        /// <returns>Returns the InitialTargets attribute value. Returns an empty string if the attribute is not present.</returns>
        public string InitialTargets
        {
            get { return initialTargets ?? string.Empty; }
            set { initialTargets = value; }
        }
        /// <summary>
        /// Gets the last-write-time of the project file.
        /// </summary>
        /// <remarks>This has not yet been implemented and will always be DateTime.MinValue</remarks>
        /// <returns>Gets the last-write-time of the project file.</returns>
        public DateTime LastWriteTimeWhenRead
        {
            get { return DateTime.MinValue; }
        }
        /// <summary>
        /// Gets the XML content that represents this project.
        /// </summary>
        /// <returns>Returns the XML content that represents this project as a string.</returns>
        public string RawXml
        {
            get
            {
                using (var writer = new StringWriter(CultureInfo.InvariantCulture))
                {
                    Save(writer);
                    return writer.ToString();
                }
            }
        }
        /// <summary>
        /// Gets the time that this project was last modified.
        /// </summary>
        /// <remarks>This has not yet been implemented and will always be DateTime.Now</remarks>
        /// <returns>Returns the time that this project was last modified. Returns null if the project hasn't been modified since being created or loaded.</returns>
        public DateTime TimeLastChanged
        {
            get { return DateTime.Now; }
        }

        string toolsVersion;
        /// <summary>
        /// Gets or sets the value of the ToolsVersion attribute.
        /// </summary>
        /// <returns>Returns the ToolsVersion attribute value. Returns an empty string if the attribute is not present.</returns>
        public string ToolsVersion
        {
            get { return toolsVersion ?? string.Empty; }
            set { toolsVersion = value; }
        }
        /// <summary>
        /// Gets the version number of this object.
        /// </summary>
        /// <remarks>This has not yet been implemented and will always be 0</remarks>
        /// <returns>Returns the version number of this object.</returns>
        public int Version
        {
            get { return 0; }
        }

        Root(/*ProjectCollection projectCollection*/)
        {
            ToolsVersion = "4.0";
        }

        //public static Root Create()
        //{
        //    return Create(/*ProjectCollection.GlobalProjectCollection*/);
        //}
        /// <summary>
        /// Creates and initializes an in-memory, empty ProjectRootElement instance and adds it to the global project collection.
        /// </summary>
        /// <returns>Returns the new project root.</returns>
        public static Root Create(/*ProjectCollection projectCollection*/)
        {
            return new Root(/*projectCollection*/);
        }

        //public static Root Create(string path)
        //{
        //    return Create(path/*, ProjectCollection.GlobalProjectCollection*/);
        //}

        //public static Root Create(XmlReader xmlReader)
        //{
        //    return Create(xmlReader/*, ProjectCollection.GlobalProjectCollection*/);
        //}
        /// <summary>
        /// Creates and initializes an in-memory, empty ProjectRootElement instance and adds it to the global project collection. The new project root is initialized from data found at the specified file path.
        /// </summary>
        /// <param name="path">The file path to the data used for initialization.</param>
        /// <returns>Returns the new project root.</returns>
        public static Root Create(string path/*, ProjectCollection projectCollection*/)
        {
            var result = Create(/*projectCollection*/);
            result.FullPath = path;
            return result;
        }
        /// <summary>
        /// Creates and initializes an in-memory, empty ProjectRootElement instance and adds it to the global project collection. The new project root is initialized from data read from the specified XmlReader.
        /// </summary>
        /// <param name="xmlReader">The XML reader used for initialization.</param>
        /// <returns>Returns the new project root.</returns>
        public static Root Create(XmlReader xmlReader/*, ProjectCollection projectCollection*/)
        {
            var result = Create(/*projectCollection*/);
            result.ToolsVersion = null;
            result.Load(xmlReader);
            return result;
        }

        //public static Root Open(string path)
        //{
        //    return Open(path/*, ProjectCollection.GlobalProjectCollection*/);
        //}
        /// <summary>
        /// Initializes a project root in the global project collection by loading data from the specified file path.
        /// </summary>
        /// <param name="path">The file path to the data.</param>
        /// <returns>Returns the initialized project root.</returns>
        public static Root Open(string path/*, ProjectCollection projectCollection*/)
        {
            Root createdResult = Create(path/*, projectCollection*/);
            using (var reader = XmlReader.Create(path))
            {
                createdResult.Load(reader);
            }
            return createdResult;
        }
        /// <summary>
        /// Saves the project, if modified, to the file system.
        /// </summary>
        public void Save()
        {
            if (FullPath == null)
            {
                throw new InvalidOperationException("This project was not given the file path to write to.");
            }
            Save(Encoding);
        }
        /// <summary>
        /// Saves the project, if modified, using the specified character encoding.
        /// </summary>
        /// <param name="saveEncoding">The character encoding used to save the project.</param>
        public void Save(Encoding saveEncoding)
        {
            using (var writer = new StreamWriter(File.Create(FullPath), saveEncoding))
            {
                Save(writer);
            }
        }
        /// <summary>
        /// Saves the project, if modified or if the file path to storage has changed.
        /// </summary>
        /// <param name="path">The file path to the project in storage.</param>
        public void Save(string path)
        {
            Save(path, Encoding);
        }
        /// <summary>
        /// Saves the project to the specified text writer, whether modified or not.
        /// </summary>
        /// <param name="writer">The text writer to write the project to.</param>
        public void Save(TextWriter writer)
        {
            using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings
            {
                Indent = true,
                NewLineChars = "\r\n"
            }))
            {
                Save(xmlWriter);
            }
        }
        /// <summary>
        /// Saves the project, if modified or if the file path to storage has changed. Uses the specified character encoding..
        /// </summary>
        /// <param name="path">The file path to the project in storage.</param>
        /// <param name="encoding">The character encoding used to save the project.</param>
        public void Save(string path, Encoding encoding)
        {
            FullPath = path;
            Save(encoding);
        }

        //public static Root TryOpen(string path)
        //{
        //    return TryOpen(path, ProjectCollection.GlobalProjectCollection);
        //}

        //public static Root TryOpen(string path, ProjectCollection projectCollection)
        //{
        //    // this should be non-null only if the project is already cached
        //    // and caching is not yet implemented
        //    throw new NotImplementedException("Caching is not yet implemented");
        //}

        internal override void Load(XmlReader reader)
        {
            try
            {
                base.Load(reader);
            }
            catch (XmlException ex)
            {
                throw new InvalidProjectFileException(FullPath, ex.LineNumber, ex.LinePosition, 0, 0,
                        ex.Message, null, null, null);
            }
        }

        /// <summary>
        /// Loads a child element from an XmlReader
        /// </summary>
        /// <param name="reader">The XmlReader to load elements from</param>
        /// <returns>The loaded element</returns>
        protected override Element LoadChildElement(XmlReader reader)
        {
            string localName;
            Element loadedElement;

            localName = reader.LocalName;
            switch (localName)
            {
                case "Choose":
                    Choose choose;

                    choose = CreateChooseElement();
                    AppendChild(choose);

                    loadedElement = choose;
                    break;
                case "Import":
                    loadedElement = AddImport(null).Element;
                    break;
                case "ImportGroup":
                    loadedElement = AddImportGroup().Element;
                    break;
                case "ItemDefinitionGroup":
                    var def = CreateItemDefinitionGroupElement();
                    AppendChild(def);
                    loadedElement = def;
                    break;
                case "ItemGroup":
                    var itemGroup = CreateItemGroupElement();
                    AppendChild(itemGroup);
                    loadedElement = itemGroup;
                    break;
                case "ProjectExtensions":
                    Extensions extensions;

                    extensions = CreateProjectExtensionsElement();
                    AppendChild(extensions);

                    loadedElement = extensions;
                    break;
                case "PropertyGroup":
                    var prop = CreatePropertyGroupElement();
                    AppendChild(prop);
                    loadedElement = prop;
                    break;
                case "Target":
                    loadedElement = AddTarget(null).Element;
                    break;
                case "UsingTask":
                    var ut = AddUsingTask(null, null, null).Element;
                    loadedElement = ut;
                    break;


                //case "Item":
                //    var item = CreateItemElement(null);
                //    AppendChild(item);
                //    loadedElement = item;
                //    break;
                //case "ItemDefinition":
                //    var itemDefinition = CreateItemDefinitionElement(null);
                //    AppendChild(itemDefinition);
                //    loadedElement = itemDefinition;
                //    break;

                default:
                    throw CreateError(reader, string.Format("Child \"{0}\" is not a known node type.", localName), -1);
            }

            return loadedElement;
        }
        internal override void LoadAttribute(string name, string value)
        {
            switch (name)
            {
                case "xmlns":
                    break;
                case "ToolsVersion":
                    ToolsVersion = value;
                    break;
                case "DefaultTargets":
                    DefaultTargets = value;
                    break;
                case "InitialTargets":
                    InitialTargets = value;
                    break;
                default:
                    throw new InvalidProjectFileException(string.Format("Unknown attribute: '{0}'", name));
            }
        }
        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(XmlName, MSBuildNamespace);
            SaveValue(writer);
            writer.WriteEndElement();
        }
        internal override void SaveValue(XmlWriter writer)
        {
            SaveAttribute(writer, "ToolsVersion", ToolsVersion);
            SaveAttribute(writer, "DefaultTargets", DefaultTargets);
            SaveAttribute(writer, "InitialTargets", InitialTargets);
            base.SaveValue(writer);
        }
    }
}