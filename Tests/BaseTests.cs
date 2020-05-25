//using CentralExecutive;
//using Repositories;
//using System;
//using Xunit;

//namespace Tests
//{
//    public class BaseTests
//    {
//        [Fact]
//        public async void ParseName_Success()
//        {
//            //var title = "Consumer Price Index: All Items in U.S. City Average, All Urban Consumers";
//            var title = "St. Louis Federal Reserve";
//            var result = Program.ParseName(title);
//            Assert.True(result.Length < title.Length);
//        }

//        [Fact]
//        public async void Process_Base()
//        {
//            var result = Program.Process(new Models.Rabbit.PackageMessage
//            {
//                OrganizationTitle = "Centers for Medicare &m Medicaid Services",
//                Title = "U.S.: Consumer Price Index, 2008",
//            });
//        }
//    }
//}
