using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApiProject.Models
{
    [ExcludeFromCodeCoverage]
    public class SensorDetails
    {
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [RegularExpression(@"[a-zA-Z0-9]+")]
        public string SensorType  { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")] 
        [RegularExpression(@"[0-9]{1,2}[P][a]")]       
        public string Pressure  { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [RegularExpression(@"[0-9]{2,3}")]
        public string Temperature  { get; set; }       
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [RegularExpression(@"[0-9]{1,2}[V]")]
        public string SupplyVoltageLevel  { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [RegularExpression(@"[0-9]{1,2}[%]")]
        public string Accuracy  { get; set; }   
    }
}