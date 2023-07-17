using Microsoft.EntityFrameworkCore;

namespace WebApiProject.Data
{
    public class SensorDetailsContext : DbContext
    {
        public SensorDetailsContext(DbContextOptions<SensorDetailsContext> options) : base(options)
        {

        }
        public DbSet<SensorDetailsNew> SensorDetailsNew {get; set;}
    }
}