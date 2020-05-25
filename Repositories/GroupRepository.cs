using Flurl.Http;
using Models;
using Models.CKAN;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Repositories
{
    public class GroupRepository
    {
        public async Task<SearchResult> GetGroups(string[] names = null)
        {
            var url = $"{Shared._baseUrl}/action/group_list";
            var response = await url
                .WithHeaders(new
                {
                    Accept = "application/json",
                    Authorization = Shared._ckanKey,
                    User_Agent = Shared._userAgent
                })
                .PostJsonAsync(new
                {
                    organizations = names
                })
                .ReceiveJson<SearchResult>(); // identical to a would-be "GroupSearchResult"

            return response;
        }

        public async Task<GroupSearchResult> GetGroup(string id)
        {
            var url = $"{Shared._baseUrl}/action/group_show?id={id}";
            var response = await url
                .WithHeaders(new
                {
                    Accept = "application/json",
                    Authorization = Shared._ckanKey,
                    User_Agent = Shared._userAgent
                })
                .GetJsonAsync<GroupSearchResult>();

            return response;
        }

        public async Task<dynamic> UpdateGroup(Group group)
        {
            //Convert List<string> Packages to List<Dictionary<string, string>> Packages
            //apparently we never need to convert the List<Dictionary> back to a List<> so I am converting here quietly for sanity's sake
            var packages = ConvertPackagesObject(group.Packages);

            var url = $"{Shared._baseUrl}/action/group_update";
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
                    group.Id,
                    packages
                })
                .ReceiveJson();

            return response;
        }

        public async Task<SearchResult> PurgeGroup(string id)
        {
            var url = $"{Shared._baseUrl}/action/group_purge";
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

        /// <summary>
        /// CKAN API expects a very specific type of object to contain the packages that belong to a group (List<Dictionary<string, string>>)
        /// </summary>
        /// <param name="packages"></param>
        /// <returns></returns>
        private List<Dictionary<string, string>> ConvertPackagesObject(List<string> packages)
        {
            var results = new List<Dictionary<string, string>>();

            foreach (var package in packages)
            {
                var packageDictionary = new Dictionary<string, string>();
                packageDictionary.Add("id", package);
                results.Add(packageDictionary);
            }

            return results;
        }
    }
}
