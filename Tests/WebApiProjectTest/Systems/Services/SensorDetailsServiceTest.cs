using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Repository;
using WebApiProjectTest.MockData;

namespace WebApiProjectTest.Systems.Services
{
    public class SensorDetailsServiceTest : IDisposable
    {
        private readonly SensorDetailsContext _dbContext;
        public SensorDetailsServiceTest()
        {
            var options = new DbContextOptionsBuilder<SensorDetailsContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _dbContext = new SensorDetailsContext(options);
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void AddRecord()
        {
            ///arrange
            var sensor = MockDataTest.AddSensorOk();
            var sut = new SensorDetailsRepository(_dbContext); 
            
            //act
            var result = sut.AddSensorRecords(sensor);

            ///assert
            result.Equals(sensor);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}