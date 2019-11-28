using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assesment_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assesment_1.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void totalDistanceTest()
        {
            Assesment_1.MainWindow main = new Assesment_1.MainWindow();
            string connstring =
                "server=localhost;" +
                "user=AJC_Car_asses1;" +
                "database=ajc_car_asses1;" +
                "port=3306;" +
                "password=A*crewdev;";

            string Expected = "102350.0";
            string Result = main.totalDistance(2, 20, connstring);
            //Assert
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod()]
        public void ParseInputsTestMouse()
        {
            Assesment_1.MainWindow main = new Assesment_1.MainWindow();
            bool Expected = true;
            bool Result = main.ParseInputs("Mouse", 0, "1", "1", "1", "1", "1", "1");

            Assert.AreEqual(Result, Expected);
        }

        [TestMethod()]
        public void ParseInputsTestSubmitVehicles()
        {
            Assesment_1.MainWindow main = new Assesment_1.MainWindow();
            bool Expected = false;
            bool Result = main.ParseInputs("Submit", 0, "1", "1", "12345", "1", "1", "1");

            Assert.AreEqual(Result, Expected);
        }

        [TestMethod()]
        public void LastServiceTest()
        {
            Assesment_1.MainWindow main = new Assesment_1.MainWindow();
            string connstring =
                "server=localhost;" +
                "user=AJC_Car_asses1;" +
                "database=ajc_car_asses1;" +
                "port=3306;" +
                "password=A*crewdev;";

            string Expected = "30.0";
            string Result = main.LastService(3, 13, connstring);
            //Assert
            Assert.AreEqual(Result, Expected);
        }

        [TestMethod()]
        public void totalFilledTest()
        {
            Assesment_1.MainWindow main = new Assesment_1.MainWindow();
            string connstring =
                "server=localhost;" +
                "user=AJC_Car_asses1;" +
                "database=ajc_car_asses1;" +
                "port=3306;" +
                "password=A*crewdev;";

            string Expected = "51";
            string Result = main.totalFilled(1, 20, connstring);
            //Assert
            Assert.AreEqual(Result, Expected);
        }
    }
}