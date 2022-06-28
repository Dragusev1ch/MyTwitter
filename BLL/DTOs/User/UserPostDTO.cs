using BLL.DTOs.Post;

namespace BLL.DTOs.User
{
    public class UserPostDTO
    {
        public ICollection<PostDTO> Posts { get; set; }
    }
}
