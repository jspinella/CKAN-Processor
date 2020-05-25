using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Models.Enums;

namespace CentralExecutive.Utilities
{
    internal static class StringUtils
    {
        //convert "U.S.: Consumer Price Index, 2008" to "US_Consumer_Price_Index_2008" (- and _ are allowed)
        internal static string ParseName(string title)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9_-]"); //allow _ and -
            title = title.Replace(".", ""); //remove . e.g. St. Louis -> St Louis | if this gets more complex, refactor
            return rgx.Replace(title, "_").ToLower().Substring(0, Math.Min(title.Length, 100)); //no uppercase letters allowed, 100 character max
        }

        //determine (guess at this point) file format based on URL
        // Would be more reliable to download the file from the URL and see what the extension is, not sure if
        // all websites will have file extension in the download URL for the resource
        internal static FileType GuessFileFormat(string url)
        {
            if (url.Contains("csv", StringComparison.OrdinalIgnoreCase))
                return FileType.CSV;
            if (url.Contains("xls", StringComparison.OrdinalIgnoreCase))
                return FileType.XLS;
            if (url.Contains("xml", StringComparison.OrdinalIgnoreCase))
                return FileType.XML;
            return FileType.Unsupported;
        }
    }
}
