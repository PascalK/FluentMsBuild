using System;

namespace FluentMsBuild
{
    /// <summary>
    /// This exception is thrown whenever there is a problem with the user's XML project file. The problem might be semantic or syntactical. The latter would be of a type typically caught by XSD validation (if it was performed by the project writer). 
    /// </summary>
    [Serializable]
    public class InvalidProjectFileException : Exception
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public InvalidProjectFileException()
        {
        }
        /// <summary>
        /// Creates an instance of this exception using the specified error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public InvalidProjectFileException(string message)
            : base(message)
        {
        }
        //private InvalidProjectFileException(SerializationInfo info, StreamingContext context)
        //    : base(info, context)
        //{
        //    ProjectFile = info.GetString("projectFile");
        //    LineNumber = info.GetInt32("lineNumber");
        //    ColumnNumber = info.GetInt32("columnNumber");
        //    EndLineNumber = info.GetInt32("endLineNumber");
        //    EndColumnNumber = info.GetInt32("endColumnNumber");
        //    ErrorSubcategory = info.GetString("errorSubcategory");
        //    ErrorCode = info.GetString("errorCode");
        //    HelpKeyword = info.GetString("helpKeyword");
        //    HasBeenLogged = info.GetBoolean("hasBeenLogged");
        //}
        /// <summary>
        /// Creates an instance of this exception using the specified error message and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InvalidProjectFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        //internal InvalidProjectFileException(ILocation start, string message,
        //                                      string errorSubcategory = null, string errorCode = null, string helpKeyword = null)
        //    : this(start != null ? start.File : null, 0, start != null ? start.Column : 0, 0, 0, message, errorSubcategory, errorCode, helpKeyword)
        //{
        //}
        //internal InvalidProjectFileException(ElementLocation start, ElementLocation end, string message,
        //                                    string errorSubcategory = null, string errorCode = null, string helpKeyword = null)
        //    : this(start != null ? start.File : null, start != null ? start.Line : 0, start != null ? start.Column : 0,
        //            end != null ? end.Line : 0, end != null ? end.Column : 0,
        //            message, errorSubcategory, errorCode, helpKeyword)
        //{
        //}
        /// <summary>
        /// Creates an instance of this exception using rich error information.
        /// </summary>
        /// <param name="projectFile">The invalid project file (can be empty string).</param>
        /// <param name="lineNumber">The invalid line number in the project (set to zero if not available).</param>
        /// <param name="columnNumber">The invalid column number in the project (set to zero if not available).</param>
        /// <param name="endLineNumber">The end of a range of invalid lines in the project (set to zero if not available).</param>
        /// <param name="endColumnNumber">The end of a range of invalid columns in the project (set to zero if not available).</param>
        /// <param name="message">Error message for exception.</param>
        /// <param name="errorSubcategory">Error sub-category that describes the error (can be null).</param>
        /// <param name="errorCode">The error code (can be null).</param>
        /// <param name="helpKeyword">The F1-help keyword for the host IDE (can be null).</param>
        public InvalidProjectFileException(string projectFile, int lineNumber, int columnNumber,
                                            int endLineNumber, int endColumnNumber, string message,
                                            string errorSubcategory, string errorCode, string helpKeyword)
            : base(message)
        {
            ProjectFile = projectFile;
            LineNumber = lineNumber;
            ColumnNumber = columnNumber;
            EndLineNumber = endLineNumber;
            EndColumnNumber = endColumnNumber;
            ErrorSubcategory = errorSubcategory;
            ErrorCode = errorCode;
            HelpKeyword = helpKeyword;
        }
        //public override void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    base.GetObjectData(info, context);
        //    info.AddValue("projectFile", ProjectFile);
        //    info.AddValue("lineNumber", LineNumber);
        //    info.AddValue("columnNumber", ColumnNumber);
        //    info.AddValue("endLineNumber", EndLineNumber);
        //    info.AddValue("endColumnNumber", EndColumnNumber);
        //    info.AddValue("errorSubcategory", ErrorSubcategory);
        //    info.AddValue("errorCode", ErrorCode);
        //    info.AddValue("helpKeyword", HelpKeyword);
        //    info.AddValue("hasBeenLogged", HasBeenLogged);
        //}
        /// <summary>
        /// Gets the exception message not including the project file.
        /// </summary>
        /// <returns>The error message string only.</returns>
        public string BaseMessage
        {
            get { return base.Message; }
        }
        /// <summary>
        /// Gets the invalid column number (if any) in the project.
        /// </summary>
        /// <returns>The invalid column number, or zero.</returns>
        public int ColumnNumber { get; private set; }
        /// <summary>
        /// Gets the last column number (if any) of a range of invalid columns in the project.
        /// </summary>
        /// <returns>The last invalid column number, or zero.</returns>
        public int EndColumnNumber { get; private set; }
        /// <summary>
        /// Gets the last line number (if any) of a range of invalid lines in the project.
        /// </summary>
        /// <returns>The last invalid line number, or zero.</returns>
        public int EndLineNumber { get; private set; }
        /// <summary>
        /// Gets the error code (if any) associated with the exception message.
        /// </summary>
        /// <returns>Error code string, or null.</returns>
        public string ErrorCode { get; private set; }
        /// <summary>
        /// Gets the error sub-category (if any) that describes the type of this error.
        /// </summary>
        /// <returns>The sub-category string, or null.</returns>
        public string ErrorSubcategory { get; private set; }
        /// <summary>
        /// Gets a flag that determines whether the exception has already been logged. Allows the exception to be logged at the most appropriate location, but continue to be propagated.
        /// </summary>
        /// <returns>Returns a flag that determines whether the exception has already been logged.</returns>
        public bool HasBeenLogged { get; private set; }
        /// <summary>
        /// Gets the F1-help keyword (if any) associated with this error, for the host IDE.
        /// </summary>
        /// <returns>The keyword string, or null.</returns>
        public string HelpKeyword { get; private set; }
        /// <summary>
        /// Gets the invalid line number (if any) in the project.
        /// </summary>
        /// <returns>The invalid line number, or zero.</returns>
        public int LineNumber { get; private set; }
        /// <summary>
        /// Gets the exception message including the affected project file (if any).
        /// </summary>
        /// <returns>The complete message string.</returns>
        public override string Message
        {
            get { return ProjectFile == null ? base.Message : base.Message + " " + GetLocation(); }
        }
        /// <summary>
        /// Gets the file (if any) associated with this exception. This may be an imported file.
        /// </summary>
        /// <returns>Project filename/path string, or null.</returns>
        public string ProjectFile { get; private set; }

        /// <summary>
        /// Gets a location of where the exception originated in a file
        /// </summary>
        /// <returns>The location where the exception originated</returns>
        public string GetLocation()
        {
            string start = LineNumber == 0 ? string.Empty : ColumnNumber > 0 ? string.Format("{0},{1}", LineNumber, ColumnNumber) : string.Format("{0}", LineNumber);
            string end = EndLineNumber == 0 ? string.Empty : EndColumnNumber > 0 ? string.Format(" - {0},{1}", EndLineNumber, EndColumnNumber) : string.Format(" - {0}", EndLineNumber);
            return ProjectFile == null ? string.Format(" at: {0} ({1}{2})", "[UnknownFile]", start, end) : string.Format(" at: {0} ({1}{2})", ProjectFile, start, end);
        }
    }
}
