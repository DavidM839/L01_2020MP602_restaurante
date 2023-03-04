using System.ComponentModel.DataAnnotations;

namespace L01_2020MP602.Models
{
    public class motoristas
    {  
        [Key]
        public int motoristaId { get; set; }
        public String nombreMotorista { get; set; }
    }
}
