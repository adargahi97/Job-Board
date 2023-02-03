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
    public class InterviewTest
    {
        [TestMethod]

        public void AddInterview()
        {
            Interview sut = new Interview();
            Interview expectedInterview = new Interview(); 
            

            sut.AddInterview(expectedInterview);

            Assert.AreEqual(new Interview(), sut.NewInterview);
        }
    }
}
