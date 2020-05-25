using Flurl.Http;
using Models.CKAN;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Repositories
{
    public class PackageRepository
    {
        #region Create
        // Create Package (DataSet)
        // NOTE: private packages must have an owner_org so I am defaulting isPrivate to false
        /// <summary>
        /// Create a new CKAN Package/DataSet
        /// </summary>
        /// <param name="name">Name of the package (Note: package_id will be generated automatically)</param>
        /// <returns></returns>
        public async Task<dynamic> CreatePackage(string name, string sourceUrl, List<string> groups, string title = null,
            string author = null, string notes = null, string ownerOrg = null, bool isPrivate = false)
        {
            //Convert List<string> groups to List<Dictionary<string, string>>
            var convertedGroups = ConvertGroupsObject(groups);
            var url = $"{Shared._baseUrl}/action/package_create";

            var response = await url
            .AllowHttpStatus(HttpStatusCode.Conflict)
            .WithHeaders(new
            {
                Accept = "application/json",
                Authorization = Shared._ckanKey,
                User_Agent = Shared._userAgent
            })
            .PostJsonAsync(new
            {
                name,   // 2-100 character requirement
                title,  // defaults to name if null - no character limit
                author, // optional
                notes,  // optional
                owner_org = ownerOrg,
                url = sourceUrl,
                groups = convertedGroups,
                @as = "table",
                @private = isPrivate
            })
            .ReceiveJson();

            //if response is 409 throw an exception with the CKAN validation message
            return response;
        }

        #endregion
        #region Read
        //get Package/DatSet by id (e.g. industrial_production_index)
        /// <summary>
        /// Get a Package by package_id or name (or something else, but you probably shouldn't be querying by anything else)
        /// </summary>
        /// <param name="packageId">package_id or name</param>
        /// <returns></returns>
        public async Task<PackageSearchResult> GetPackage(string query = null)
        {
            //var encoded = System.Web.HttpUtility.UrlEncode(query);
            var url = $"{Shared._baseUrl}/action/package_search?q={Uri.EscapeUriString(query)}";
            var response = await url
                .WithHeaders(new
                {
                    Accept = "application/json",
                    Authorization = Shared._ckanKey,
                    User_Agent = Shared._userAgent
                })
                .GetJsonAsync<PackageSearchResult>();

            return response;
        }

        public async Task<SearchResult> GetPackages()
        {
            var url = $"{Shared._baseUrl}/action/package_list";
            var response = await url
                .WithHeaders(new
                {
                    Accept = "application/json",
                    Authorization = Shared._ckanKey,
                    User_Agent = Shared._userAgent
                })
                .GetJsonAsync<SearchResult>();

            return response;
        }

        //get Resource by name and org
        #endregion
        #region Update
        //update a Package/Dataset (e.g. change name)

        //update a Resource (e.g. update the file.. this is done by DataPusher for the *DataStore* version of the file, determining whether a file is new/different from existing)
        // I'm not sure that CE will need to compare file hashes. It may just do an "update" to the URL (set it to the same value it currently is) to trigger DataPusher to see if it needs
        // to update the file in DataStore. We may want to update the file in FileStore. If it's even there...
        #endregion
        #region Delete
        public async Task<SearchResult> PurgePackage(string id)
        {
            var url = $"{Shared._baseUrl}/action/package_delete";
            var response = await url
                .WithHeaders(new
                {
                    Accept = "application/json",
                    Authorization = Shared._ckanKey,
                    User_Agent = Shared._userAgent
                })
                .PostJsonAsync(new { id })
                .ReceiveJson<SearchResult>();

            return response;
        }
        #endregion

        private List<Dictionary<string, string>> ConvertGroupsObject(List<string> groups)
        {
            var results = new List<Dictionary<string, string>>();

            foreach (var group in groups)
            {
                var groupDictionary = new Dictionary<string, string>();
                groupDictionary.Add("name", group);
                results.Add(groupDictionary);
            }

            return results;
        }
    }
}
