using WebApiProject.Models;

namespace WebApiProjectTest.MockData
{
    public static class MockDataTest
    {
        public static SensorDetails AddSensorOk()
        {
            return new SensorDetails{
                SensorType = "test",
                Pressure = "10",
                Temperature = "20",
                SupplyVoltageLevel = "20",
                Accuracy = "15"
            }; 
        }

        public static SensorDetails AddSensorBad()
        {
            return new SensorDetails{
                SensorType = "test",
                Pressure = "10ttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt",
                Temperature = "20ttttttttttttttttttttttttttttttttttttt",
                SupplyVoltageLevel = "1ttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt",
                Accuracy = "15"
            }; 
        }

        public static SensorDetails AddSensorExceptionBad()
        {
            return new SensorDetails{
                SensorType = "test",
                Pressure = "",
                Temperature = "",
                SupplyVoltageLevel = "10",
                Accuracy = "15"
            }; 
        }

        public static MailRequest SendMailOk()
        {
            return new MailRequest{
                ToEmail = "rutuborkar5334@gmail.com",
                Subject = "test",
                Body = "test"
            };
        }

        public static MailRequest SendMailNull()
        {
            return new MailRequest{
                ToEmail = null,
                Subject = "test",
                Body = "test"
            };
        }

        public static MailRequest SendMailEmpty()
        {
            return new MailRequest{
                ToEmail = "",
                Subject = "test",
                Body = "test"
            };
        }

        public static User UserDetails()
        {
            return new User{
                Username = "rutuja",
                Password = "test"
            };
        }

        public static User UserDetailsEmpty()
        {
            return new User{
                Username = "",
                Password = ""
            };
        }

        public static List<User> UserList()
        {
            return new List<User> {
                new User{
                Username = "rutuja",
                Password = "test"
            }
            };
        }
    }
}