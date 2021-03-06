namespace CheckIt
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A set of file utilities.
    /// </summary>
    internal struct FileUtil
    {
        /// <summary>
        ///   Checks if name matches pattern with '?' and '*' wildcards.
        /// </summary>
        /// <param name="filename">
        ///   Name to match.
        /// </param>
        /// <param name="pattern">
        ///   Pattern to match to.
        /// </param>
        /// <returns>
        ///   <c>true</c> if name matches pattern, otherwise <c>false</c>.
        /// </returns>
        internal static bool FilenameMatchesPattern(string filename, string pattern)
        {
            // prepare the pattern to the form appropriate for Regex class
            var sb = new StringBuilder(pattern);

            // if only asterisk '*' is left, the mask is ".*"
            if (sb.ToString().Equals("*"))
            {
                pattern = ".*";
            }
            else
            {
                // replace '.' with "\."
                sb.Replace(".", "\\.");

                // replaces all occurrences of '*' with ".*" 
                sb.Replace("*", ".*");

                // replaces all occurrences of '?' with '.' 
                sb.Replace("?", ".");

                // add "\b" to the beginning and end of the pattern
                sb.Insert(0, "^");
                sb.Append("$");
                pattern = sb.ToString();
            }

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(filename);
        }
    }
}