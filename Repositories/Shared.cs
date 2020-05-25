using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public static class Shared
    {
        //TODO should put in a config file or something...
        public const string _userAgent = "USDK CentralExecutive 0.2";
        public const string _ckanKey = "7f738940-deec-43dd-b5d1-df1a22b8a6ff"; //dev
        //public const string _ckanKey = "636777d0-a2a9-4058-be2f-5469333333bf"; //prod
        public const string _baseUrl = "http://ckan.dev.datajax.org/api/3";
    }
}
