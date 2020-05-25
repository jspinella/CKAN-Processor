using System;
using System.Collections.Generic;
using System.Text;

namespace Models.CKAN.Resources
{
    public class ResourceSearchResult
    {
        public string help { get; set; }
        public bool success { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public int count { get; set; }
        public List<Result2> results { get; set; }
    }

    public class Result2
    {
        public object mimetype { get; set; }
        public object cache_url { get; set; }
        public string state { get; set; }
        public string hash { get; set; }
        public string description { get; set; }
        public string format { get; set; }
        public string url { get; set; }
        public DateTime created { get; set; }
        public object cache_last_updated { get; set; }
        public string package_id { get; set; }
        public object mimetype_inner { get; set; }
        public object last_modified { get; set; }
        public int position { get; set; }
        public string revision_id { get; set; }
        public object size { get; set; }
        public object url_type { get; set; }
        public string id { get; set; }
        public object resource_type { get; set; }
        public string name { get; set; }
    }
}
