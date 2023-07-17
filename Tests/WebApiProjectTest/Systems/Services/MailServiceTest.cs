using Microsoft.Extensions.Options;
using Moq;
using WebApiProject.Models;
using WebApiProject.Repository;
using WebApiProjectTest.MockData;

namespace WebApiProjectTest.Systems.Services
{
    public class MailServiceTest
    {
        [Fact]
        public void MailSendTrue()
        {
            ///arrange
            MailSettings mailSettings = new MailSettings() 
            {
                Username = "rutuborkar5334@gmail.com",
                Password = "rsmqjqtlvevsskyt",
                Host = "smtp.gmail.com",
                Port = 587
            };
            var mailRequest = MockDataTest.SendMailOk();
            var mockIOptions = new Mock<IOptions<MailSettings>>();
            mockIOptions.Setup(x => x.Value).Returns(mailSettings);
            MailRepository mailRepository = new MailRepository(mockIOptions.Object);

            ///act
            var result = mailRepository.SendEmail(mailRequest);

            ///assert
            Assert.True(result);
        }

        [Fact]
        public void MailSendFalseNull()
        {
            ///arrange
            MailSettings mailSettings = new MailSettings() 
            {
                Username = "rutuborkar5334@gmail.com",
                Password = "rsmqjqtlvevsskyt",
                Host = "smtp.gmail.com",
                Port = 587
            };
            var mailRequest = MockDataTest.SendMailNull();
            var mockIOptions = new Mock<IOptions<MailSettings>>();
            mockIOptions.Setup(x => x.Value).Returns(mailSettings);
            MailRepository mailRepository = new MailRepository(mockIOptions.Object);

            ///act
            var result = mailRepository.SendEmail(mailRequest);
            
            ///assert
            Assert.False(result);
        }

        [Fact]
        public void MailSendFalseEmpty()
        {
            ///arrange
            MailSettings mailSettings = new MailSettings() 
            {
                Username = "rutuborkar5334@gmail.com",
                Password = "rsmqjqtlvevsskyt",
                Host = "smtp.gmail.com",
                Port = 587
            };
            var mailRequest = MockDataTest.SendMailEmpty();
            var mockIOptions = new Mock<IOptions<MailSettings>>();
            mockIOptions.Setup(x => x.Value).Returns(mailSettings);
            MailRepository mailRepository = new MailRepository(mockIOptions.Object);

            ///act
            var result = mailRepository.SendEmail(mailRequest);
            
            ///assert
            Assert.False(result);
        }
    }
}