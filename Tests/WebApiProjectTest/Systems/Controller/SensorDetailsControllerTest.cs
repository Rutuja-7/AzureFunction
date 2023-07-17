using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiProject.Controllers;
using WebApiProject.Repository;
using WebApiProjectTest.MockData;

namespace WebApiProjectTest.Systems.Controller
{
    public class SensorDetailsControllerTest
    {
        [Fact]
        public void AddRecod_ShouldCalledAddRecordOnce()
        {
            ///arrange
            var datasource = new Mock<ISensorDetailsRepository>();
            var sensor = MockDataTest.AddSensorOk();
            var sut = new SensorDetailsNewController(datasource.Object);
            
            ///act
            var result = sut.AddRecord(sensor);

            ///assert
            datasource.Verify(v => v.AddSensorRecords(sensor), Times.Exactly(1));
        }

        [Fact]
        public void AddRecord_Shouldreturn200StatusCode()
        {
            ///arrange
            var datasource = new Mock<ISensorDetailsRepository>();
            var sensor = MockDataTest.AddSensorOk();
            var sut = new SensorDetailsNewController(datasource.Object);

            ///act
            var result = sut.AddRecord(sensor);

            ///assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void AddRecord_InvaliData_ReturnBadRequest()
        {
            ///arrange
            var datasource = new Mock<ISensorDetailsRepository>();
            var sensor = MockDataTest.AddSensorBad();
            var sut = new SensorDetailsNewController(datasource.Object);
            sut.ModelState.AddModelError("Test", "Test");
            
            ///act
            var result = sut.AddRecord(sensor);

            ///assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AddRecord_ValidData_MatchData()
        {
            ///arrange
            var datasource = new Mock<ISensorDetailsRepository>();
            var sensor = MockDataTest.AddSensorOk();
            var sut = new SensorDetailsNewController(datasource.Object);

            ///act
            var result = sut.AddRecord(sensor);

            ///assert
            Assert.IsType<OkObjectResult>(result);
            result.Equals(sensor);
        }
    }
}