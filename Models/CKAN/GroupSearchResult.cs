using System;
using System.Collections.Generic;
using System.Text;

namespace Models.CKAN
{
    public class GroupSearchResult
    {
        public string help { get; set; }
        public bool success { get; set; }
        public GroupResult result { get; set; }
    }

    public class User
    {
        public string email_hash { get; set; }
        public object about { get; set; }
        public string capacity { get; set; }
        public string name { get; set; }
        public DateTime created { get; set; }
        public bool sysadmin { get; set; }
        public bool activity_streams_email_notifications { get; set; }
        public string state { get; set; }
        public int number_of_edits { get; set; }
        public string display_name { get; set; }
        public string fullname { get; set; }
        public string id { get; set; }
        public int number_created_packages { get; set; }
    }

    public class Extra
    {
        public string value { get; set; }
        public string state { get; set; }
        public string key { get; set; }
        public string revision_id { get; set; }
        public string group_id { get; set; }
        public string id { get; set; }
    }

    public class GroupResult
    {
        public List<User> users { get; set; }
        public string display_name { get; set; }
        public string description { get; set; }
        public string image_display_url { get; set; }
        public int package_count { get; set; }
        public DateTime created { get; set; }
        public string name { get; set; }
        public bool is_organization { get; set; }
        public string state { get; set; }
        public List<Extra> extras { get; set; }
        public string image_url { get; set; }
        public List<string> groups { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string revision_id { get; set; }
        public int num_followers { get; set; }
        public string id { get; set; }
        public List<string> tags { get; set; }
        public string approval_status { get; set; }
    }
}
