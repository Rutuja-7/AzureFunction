using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApiProject.Data
{
    public class SensorDetailsNew
    {
        [ExcludeFromCodeCoverage]
        [Key]
        public virtual Guid SensorId { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string SensorType  { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        [Required]
        public string Pressure  { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        [Required]
        public string Temperature  { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        [Required]
        public string SupplyVoltageLevel  { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        [Required]
        public string Accuracy  { get; set; } 
    }
}