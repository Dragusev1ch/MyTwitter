using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Post
    {
        [Key] public int ID { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        [Required] public string Description { get; set; } = string.Empty;
        public string? PhotoPath { get; set; }

        public ICollection<Coment> Coments { get; set; }

        public uint Views { get; set; } = 0;
        public uint Likes { get; set; } = 0;
    }
}
