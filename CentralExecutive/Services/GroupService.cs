using Models;
using Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Models.CKAN;

namespace CentralExecutive.Services
{
    internal class GroupService
    {
        public GroupRepository groupRepo = new GroupRepository();

        public async Task<List<Group>> GetGroups()
        {
            var results = new List<Group>();
            var groupIds = await groupRepo.GetGroups();
            foreach (var groupId in groupIds.result)
            {
                var group = await groupRepo.GetGroup(groupId);
                var tags = group.result.extras
                    .Where(x => x.key.Equals("tags", StringComparison.OrdinalIgnoreCase))
                    .SingleOrDefault()?.value;

                if (tags != null)
                {
                    tags.Replace(" ", ""); // usda, food, farm -> usda,food,farm
                    results.Add(new Group
                    {
                        Id = groupId,
                        Tags = tags.Split(",").ToList(),
                    });
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    //Console.WriteLine($"No tags found for group \"{groupId}\". Please add tags to this group."); // for Prod at least, consider a separate console app anyone can run to see which groups need tags
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            return results;
        }
    }
}
