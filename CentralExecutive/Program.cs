using CentralExecutive.Services;
using CentralExecutive.Utilities;
using Models.Rabbit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralExecutive
{
    public static class Program
    {
        static void Main(string[] args)
        {
            //listen for RabbitMQ messages from centralExecutive queue
            var factory = new ConnectionFactory() { HostName = "ckan.dev.datajax.org", UserName = "usdk", Password = "usdk" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "centralExecutive",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    Console.WriteLine("New message recieved");
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    try
                    {
                        var package = JsonConvert.DeserializeObject<PackageMessage>(message);
                        if (package != null && !string.IsNullOrWhiteSpace(package.PackageTitle) && 
                            package.SourceUrl != null && 
                            !string.IsNullOrWhiteSpace(package.OrganizationTitle))
                            await Process(package).ConfigureAwait(false);
                        else
                            Console.WriteLine($"One or more required fields are NULL, skipping this request...");

                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Something went wrong: {e}");
                        //TODO: send email, log to GrayLog or something to that effect
                    }

                };
                channel.BasicConsume(queue: "centralExecutive",
                                     autoAck: false,
                                     consumer: consumer);

                //code runs until you press enter
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        public async static Task Process(PackageMessage package)
        {
            //get name from title
            var packageName = StringUtils.ParseName(package.PackageTitle);
            Console.WriteLine($"Package name: { packageName }");

            var groupService = new GroupService();
            var groups = await groupService.GetGroups();

            // see if any resources' XML, Excel files contain keyword
            // if there are any relevant packages, process Org, Package and Resources that passed validation (they all belong to same package and org so we don't need all to pass)
            // How do we know if a package is relevant? Look at: OrganizationTitle, PackageName, and eventually contents of the resource files themselves (parser city!)
            var resourceService = new ResourceService();
            var packageService = new PackageService();
            var validResources = await resourceService.ValidateResources(package);
            var matchedGroups = await packageService.ValidatePackage(packageName, validResources, groups); // compares all words in package name and resource URLs with all tags in all groups

            if (validResources.Any() && matchedGroups.Any())
            {
                //Organization
                var orgService = new OrganizationService();
                var orgName = await orgService.ProcessOrganization(package);

                // Package
                var existingPackage = await packageService.ProcessPackage(package, packageName, orgName, matchedGroups);

                // Resource
                foreach (var resource in validResources)
                    await resourceService.ProcessResource(resource, existingPackage, packageName);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Finished processing package { packageName } belonging to { orgName }\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;

                if (!validResources.Any())
                    Console.WriteLine($"No valid resources found for { packageName }");
                if (!matchedGroups.Any())
                    Console.WriteLine($"No groups found for { packageName }");
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
