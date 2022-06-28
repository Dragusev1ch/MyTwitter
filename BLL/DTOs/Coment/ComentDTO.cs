using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Coment
{
    public class ComentDTO
    {
        [Key] public int ID { get; set; }
        [Required] public string Description { get; set; } = string.Empty;
        public uint Likes { get; set; } = 0;
    }
}
