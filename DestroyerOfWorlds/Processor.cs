using Repositories;
using System;
using System.Threading.Tasks;

namespace DestroyerOfWorlds
{
    internal class Processor
    {
        public PackageRepository _packageRepo = new PackageRepository();
        public GroupRepository _groupRepo = new GroupRepository();
        public OrganizationRepository _orgRepo = new OrganizationRepository();

        public async Task DeleteAllData()
        {
            await DeletePackages();
            //await DeleteGroups(); // since groups are manually added, let's not automatically delete them
            await DeleteOrganizations();
        }

        private async Task DeletePackages() // also deletes Resources
        {
            var count = 0;
            var packages = await _packageRepo.GetPackages();
            {
                foreach (var package in packages.result)
                {
                    await _packageRepo.PurgePackage(package);
                    count++;
                }
                Console.WriteLine($"Deleted {count} packages");
            }
        }

        private async Task DeleteGroups()
        {
            var count = 0;
            var groups = await _groupRepo.GetGroups();
            if (groups != null) {
                foreach (var group in groups.result)
                {
                    await _groupRepo.PurgeGroup(group);
                    count++;
                }
                Console.WriteLine($"Deleted {count} groups");
            }
        }

        private async Task DeleteOrganizations()
        {
            var count = 0;
            var organizations = await _orgRepo.GetOrganizations();
            if (organizations != null) {
                foreach (var org in organizations.result)
                {
                    await _orgRepo.PurgeOrganization(org);
                    count++;
                }
                Console.WriteLine($"Deleted {count} organizations");
            }
        }

    }
}
