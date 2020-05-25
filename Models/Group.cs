using System.Collections.Generic;

namespace Models
{
    public class Group
    {
        public string  Id { get; set; } // aka name
        public List<string> Tags { get; set; } // e.g. "agriculture, farms, crops, food, vegetables"
        public List<string> Packages { get; set; } // CKAN API expects this as List<Dictionary<string, string>> Packages
    }
}
