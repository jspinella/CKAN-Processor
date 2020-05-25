using CentralExecutive.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class AlgorithmTests
    {
        [Fact]
        public async void DamerauLevenshtein_Test()
        {
            // case-sensitive! distance == number of additions, subtractions, substitutions, transpositions to get from stringA to stringB
            var stringA = "Centers for Medicare &amp; Medicaid Services";
            var stringB = "Centers for Medicare & Medicaid Services";
            var distance = DamerauLevenshtein.Distance(stringA, stringB);
        }
    }
}
