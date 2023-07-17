using WebApiProject.Data;
using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public class SensorDetailsRepository : ISensorDetailsRepository
    {
        private readonly SensorDetailsContext _context;
        public SensorDetailsRepository(SensorDetailsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method will add sensor record to database.
        /// </summary>
        /// <param name= sensor></param>
        /// <returns>SensorDetailsNew</returns>
        public SensorDetails AddSensorRecords(SensorDetails sensor)
        {
            var newSensors = new SensorDetailsNew()
            {
               SensorType = sensor.SensorType,
               Pressure = sensor.Pressure,
               Temperature = sensor.Temperature,
               SupplyVoltageLevel = sensor.SupplyVoltageLevel,
               Accuracy = sensor.Accuracy
            };

            _context.SensorDetailsNew.Add(newSensors);
            _context.SaveChanges();
            return sensor;
        }
    }
}