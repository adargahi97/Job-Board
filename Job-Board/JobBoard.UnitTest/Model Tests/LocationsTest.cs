using Job_Board.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.UnitTest
{
    [TestClass]
    public class LocationsTest
    {
        [TestMethod]

        public void AddLocations()
        {
            Locations sut = new Locations();
            Locations expectedLocations = new Locations();


            sut.AddLocations(expectedLocations);

            Assert.AreEqual(new Locations(), sut.NewLocations);
        }
    }
}


