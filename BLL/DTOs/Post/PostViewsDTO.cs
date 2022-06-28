using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Post
{
    public class PostViewsDTO
    {
        public int ID { get; set; }
        public uint Views { get; set; } = 0;
        public uint Likes { get; set; } = 0;
    }
}
