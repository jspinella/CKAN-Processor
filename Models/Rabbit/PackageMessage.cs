using System;
using System.Collections.Generic;

namespace Models.Rabbit
{
    /// <summary>
    /// Represents a CKAN Package
    /// </summary>
    public class PackageMessage
    {
        public string PackageTitle { get; set; }
        public string OrganizationTitle { get; set; }
        public string OrganizationDescription { get; set; }
        public string OrganizationImageUrl { get; set; }
        public string Author { get; set; }
        public string Notes { get; set; }
        public string SourceUrl { get; set; } // url to the HTML page representing the data set
        public List<string> Resources { get; set; } //list of URLs, each representing a resource (file) of the package
    }
}
