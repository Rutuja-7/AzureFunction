using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProject.Models;

namespace AzureFunctionProjectTest.MockData
{
    public class MockDataTest
    {
        public static SensorDetails AddSensor()
        {
            return new SensorDetails
            {
                SensorType = "test",
                Temperature = "25",
                Pressure = "5Pa",
                SupplyVoltageLevel = "12V"
            };
        }

        public static SensorDetails AddSensorBadRecord()
        {
            return new SensorDetails
            {
                SensorType = "",
                Temperature = "25",
                Pressure = "5Pa",
                SupplyVoltageLevel = "12V"
            };
        }
    }
}