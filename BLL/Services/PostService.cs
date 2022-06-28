using BLL.DTOs.Post;
using DAL.Models;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class PostService : AService
    {
        public PostService(IUnitOfWork uow) : base(uow) { }

        private async Task<List<Post>> GetPosts(uint? count)
        {
            if (count.HasValue && Database.Posts.Read().Count() >= count)
            {
                return await Database.Posts.Read().Take((int)count.Value).AsNoTracking().ToListAsync();
            }
            return await Database.Posts.Read().AsNoTracking().ToListAsync();
        }

        public async Task<List<PostDTO>> GetPostDTOs(uint? count)
        {
            var list = await GetPosts(count);

            return list.Select(post => Mapper.Map<PostDTO>(post)).ToList();
        }
        public async Task<PostDTO?> GetMainData(int postID)
        {
            var post = await Database.Posts.Read().FirstOrDefaultAsync(post => post.ID == postID);
            if (post == null) return null;
            return Mapper.Map<PostDTO?>(post);
        }

        public async Task<PostDTO?> Create(PostCreateDTO postDTO
            )
        {
            var post = Mapper.Map<Post>(postDTO);

            await Database.Posts.Create(post);
            Database.Save();

            return await GetMainData(post.ID);
        }

        public async Task<PostDTO?> Update(PostDTO postDTO)
        {
            var post = Mapper.Map<Post>(postDTO);

            await Database.Posts.Update(post);
            Database.Save();

            return await GetMainData(post.ID);
        }

        public async Task<List<PostDTO>?> Search(string query)
        {
            var list = await Database.Posts.Read().Where(post => post.Title.ToLower().Contains(query.ToLower()) || post.Description.ToLower().Contains(query.ToLower()))
                .AsNoTracking().Select(post => Mapper.Map<PostDTO>(post)).ToListAsync();


            return list;
        }


        public async Task<bool> Delete(int postID)
        {
            var result = await Database.Posts.Delete(postID);

            Database.Save();

            return result;
        }

    }

}
