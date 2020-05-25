using System;
using System.Collections.Generic;
using System.Text;

namespace Models.CKAN
{
    public class PackageSearchResult
    {
        public string help { get; set; }
        public bool success { get; set; }
        public PackageResult result { get; set; }
    }
    
    public class Facets
    {
    }

    public class Resource
    {
        public object mimetype { get; set; }
        public object cache_url { get; set; }
        public string hash { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string format { get; set; }
        public string url { get; set; }
        public bool datastore_active { get; set; }
        public object cache_last_updated { get; set; }
        public string package_id { get; set; }
        public DateTime created { get; set; }
        public string state { get; set; }
        public object mimetype_inner { get; set; }
        public object last_modified { get; set; }
        public int position { get; set; }
        public string revision_id { get; set; }
        public object url_type { get; set; }
        public string id { get; set; }
        public object resource_type { get; set; }
        public object size { get; set; }
    }

    public class Organization
    {
        public string description { get; set; }
        public DateTime created { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public bool is_organization { get; set; }
        public string state { get; set; }
        public string image_url { get; set; }
        public string revision_id { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string approval_status { get; set; }
    }

    public class PackageResult2
    {
        public object license_title { get; set; }
        public object maintainer { get; set; }
        public List<object> relationships_as_object { get; set; }
        public bool @private { get; set; }
        public object maintainer_email { get; set; }
        public int num_tags { get; set; }
        public string id { get; set; }
        public DateTime metadata_created { get; set; }
        public DateTime metadata_modified { get; set; }
        public string author { get; set; }
        public object author_email { get; set; }
        public string state { get; set; }
        public object version { get; set; }
        public string creator_user_id { get; set; }
        public string type { get; set; }
        public List<Resource> resources { get; set; }
        public int num_resources { get; set; }
        public List<object> tags { get; set; }
        public List<object> groups { get; set; }
        public object license_id { get; set; }
        public List<object> relationships_as_subject { get; set; }
        public Organization organization { get; set; }
        public string name { get; set; }
        public bool isopen { get; set; }
        public string url { get; set; }
        public string notes { get; set; }
        public string owner_org { get; set; }
        public List<object> extras { get; set; }
        public string title { get; set; }
        public string revision_id { get; set; }
    }

    public class SearchFacets
    {
    }

    public class PackageResult
    {
        public int count { get; set; }
        public string sort { get; set; }
        public Facets facets { get; set; }
        public List<PackageResult2>? results { get; set; }
        public SearchFacets search_facets { get; set; }
    }
}
