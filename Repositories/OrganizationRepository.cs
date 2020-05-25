using Flurl.Http;
using Models.CKAN;
using System.Net;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrganizationRepository
    {
        public async Task<dynamic> CreateOrganization(string name, string title = null,
            string description = null, string imageUrl = null)
        {
            var url = $"{Shared._baseUrl}/action/organization_create";
            var response = await url
                .AllowHttpStatus(HttpStatusCode.Conflict, HttpStatusCode.InternalServerError) // sometimes we are going to get a 409 because CE runs asynchronously (effectively multithreded)
                .WithHeaders(new
                {
                    Accept = "application/json",
                    Authorization = Shared._ckanKey,
                    User_Agent = Shared._userAgent
                })
                .PostJsonAsync(new
                {
                    name, // st_louis_federal_reserve
                    title, // St. Louis Federal Reserve
                    description, // "The index attempts to measure..."
                    image_url = @imageUrl // not neccessarily the logo on their website
                })
                .ReceiveJson();

            return response;
        }

        //get all organizations, optionally by name
        public async Task<SearchResult> GetOrganizations(string[] names = null)
        {
            var url = $"{Shared._baseUrl}/action/organization_list";
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
                .ReceiveJson<SearchResult>();

            return response;
        }

        /// <summary>
        /// Permanently delete an organization
        /// Purging is a hard delete, while Deleting is a soft delete (in the case of Orgs and Groups)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SearchResult> PurgeOrganization(string id)
        {
            var url = $"{Shared._baseUrl}/action/organization_purge";
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
    }
}
