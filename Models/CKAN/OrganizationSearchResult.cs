using System;
using System.Collections.Generic;

namespace Models.CKAN
{
    public class SearchResult
    {
        public string help { get; set; }
        public bool success { get; set; }
        public List<string> result { get; set; }
    }
}
