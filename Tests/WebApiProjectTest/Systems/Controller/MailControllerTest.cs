using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiProject.Controllers;
using WebApiProject.Repository;
using WebApiProjectTest.MockData;

namespace WebApiProjectTest.Systems.Controller
{
    public class MailControllerTest
    {
        [Fact]
        public void SendMail_Shouldreturn200StatusCode()
        {
            ///arrange
            var datasource = new Mock<IMailRepository>();
            var sendMail = MockDataTest.SendMailOk();
            var sut = new MailController(datasource.Object);

            ///act
            var result = sut.Send(sendMail);

            ///assert
            Assert.IsType<OkResult>(result);
            
        }

        [Fact]
        public void SendMail_SendMailCalledOnce()
        {
            ///arrange
            var datasource = new Mock<IMailRepository>();
            var sendMail = MockDataTest.SendMailOk();
            var sut = new MailController(datasource.Object);

            ///act
            var result = sut.Send(sendMail);

            ///assert
            datasource.Verify(v => v.SendEmail(sendMail), Times.Exactly(1));
        }

        [Fact]
        public void SendMail_InvalidData_ReturnBadRequest()
        {
            ///arrange
            var datasource = new Mock<IMailRepository>();
            var sendMail = MockDataTest.SendMailEmpty();
            var sut = new MailController(datasource.Object);
            sut.ModelState.AddModelError("Test", "Test");

            ///act
            var result = sut.Send(sendMail);  

            ///assert
            Assert.IsType<BadRequestObjectResult>(result);  
        }
    }
}