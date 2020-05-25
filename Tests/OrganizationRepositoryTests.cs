using Newtonsoft.Json;
using Repositories;
using System;
using Xunit;
using Models.CKAN;
using System.Linq;

namespace Tests
{
    public class OrganizationRepositoryTests
    {
        [Fact]
        public async void GetOrganizations_All_Success()
        {
            var orgRepo = new OrganizationRepository();
            var response = await orgRepo.GetOrganizations();
            Assert.NotNull(response);
            Assert.True(response.result.Count > 0);
        }

        [Fact]
        public async void GetOrganizations_Search_Success()
        {
            var query = "st_louis_federal_reserve";
            var orgRepo = new OrganizationRepository();
            var response = await orgRepo.GetOrganizations();
            Assert.NotNull(response);
            Assert.True(response.result.Count > 0);
            Assert.Equal(response.result.First(), query); //returns st_louis_federal_reserve2 also!
        }
    }
}
