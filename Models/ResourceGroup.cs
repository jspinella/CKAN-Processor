using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ResourceGroup
    {
        public string ResourceUrl { get; set; }
        public string GroupId { get; set; }
        public FileType FileType { get; set; }
    }
}
