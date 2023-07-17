using WebApiProject.Data;
using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public interface ISensorDetailsRepository
    {
        SensorDetails AddSensorRecords(SensorDetails sensorDetails);
    }
}