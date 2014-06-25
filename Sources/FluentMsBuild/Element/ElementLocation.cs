using System.Xml;

namespace FluentMsBuild
{
    /// <summary>
    /// The location of an XML node in a file. Any editing of the project XML through the MSBuild API's will invalidate locations in that XML until the XML is reloaded.
    /// </summary>
    public class ElementLocation
    {
        /// <summary>
        /// The column number where this element exists in its file. The first column is numbered 1. Zero indicates "unknown location".
        /// </summary>
        /// <returns>Returns System.Int32.</returns>
        public int Column { get; protected set; }
        /// <summary>
        /// The file from which this particular element originated. It may differ from the ProjectFile if, for instance, it was part of an import or originated in a targets file. If not known, returns empty string.
        /// </summary>
        /// <returns>Returns System.String.</returns>
        public string File { get; protected set; }
        /// <summary>
        /// The line number where this element exists in its file. The first line is numbered 1. Zero indicates "unknown location".
        /// </summary>
        /// <returns>Returns System.Int32.</returns>
        public int Line { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the Microsoft.Build.Construction.ElementLocation class.
        /// </summary>
        /// <param name="file">The file</param>
        /// <param name="li">The line information</param>
        public ElementLocation(string file, IXmlLineInfo li)
        {
            this.File = file;
            this.Line = li.LineNumber;
            this.Column = li.LinePosition;
        }
    }
}
