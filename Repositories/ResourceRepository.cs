using Flurl.Http;
using Models.CKAN.Resources;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Repositories
{
    public class ResourceRepository
    {
        // Create Resource
        // An infinite number of resources with the same data and parameters can be created under the same package,
        //  so checking for existing before adding here is paramount... DataPusher may prevent the file from being
        //  added to the DataStore more than once, but it will do nothing to prevent duplicates in CKAN proper.
        public async Task<dynamic> CreateResource(string packageId, string sourceUrl,
            string format  = null, string name = null)
        {
            var url = $"{Shared._baseUrl}/action/resource_create";
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
                    // only package_id and url are required
                    package_id = @packageId,
                    url = sourceUrl,
                    format, //required for DataPusher to run automatically for resource
                    name // just reusing Package name here as there should only be one Resource per Package MOST of the time
                })
                .ReceiveJson();

            return response;
        }

        // URL is an adequate identifier for now... CKAN API doesn't seem to allow querying by package_id
        public async Task<ResourceSearchResult> GetResourceByUrl(string resourceUrl)
        {
            var url = $"{Shared._baseUrl}/action/resource_search?query=url:{Uri.EscapeUriString(resourceUrl)}";
            var response = await url
                .WithHeaders(new
                {
                    Accept = "application/json",
                    Authorization = Shared._ckanKey,
                    User_Agent = Shared._userAgent
                })
                .GetJsonAsync<ResourceSearchResult>();

            return response;
        }
    }
}
