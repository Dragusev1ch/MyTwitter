using BLL.DTOs.Post;
using DAL.Models;
using DAL.UOW;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StatisticService : AService
    {
        public StatisticService(IUnitOfWork uow) : base(uow) { }

        private async Task<Post?> GetPostData(int id)
        {
            return await Database.Posts.Read()
                .AsNoTracking().FirstOrDefaultAsync(coment => coment.ID == id);
        }

        private async Task<bool> ActionWithPost(int postID, Action<Post> action)
        {
            var post = await GetPostData(postID);

            if(post != null)
            {
                action(post);

                var result = await Database.Posts.Update(post);

                if(result)
                {
                    Database.Save();
                    return result;
                }
            }
            return false;
        }

        public async Task<bool> AddView(int postId)
        {
            return await ActionWithPost(postId, post => post.Views++);
        }
        public async Task<bool> AddLike(int postId)
        {
            return await ActionWithPost(postId, post => post.Likes++);
        }

        private async Task<List<Post>> GetPosts(uint? count)
        {
            if (count.HasValue && Database.Posts.Read().Count() >= count)
            {
                return await Database.Posts.Read().Take((int)count.Value).AsNoTracking().ToListAsync();
            }
            return await Database.Posts.Read().AsNoTracking().ToListAsync();
        }
        private async Task<List<PostDTO>> GetPostSpecialSorted(uint? count, Comparison<PostDTO> comparer)
        {
            var list = await GetPosts(count);

            var postDtos = list.Select(post => Mapper.Map<PostDTO>(post)).ToList();

            postDtos.Sort(comparer);

            return postDtos;
        }
        public async Task<List<PostDTO>> GetMostViewed()
        {
            return await GetPostSpecialSorted(null, (dto, postDto) => dto.Views.CompareTo(postDto.Views));
        }
        public async Task<List<PostDTO>> GetMostViewedTop(uint count)
        {
            return await GetPostSpecialSorted(count, (dto, postDto) => dto.Views.CompareTo(postDto.Views)); 
        }

        public async Task<List<PostDTO>> GetMostPurchased()
        {
            return await GetPostSpecialSorted(null, (dto, postDto) => dto.Likes.CompareTo(postDto.Likes));
        }
        public async Task<List<PostDTO>> GetMostPurchasedTop(uint count)
        {
            return await GetPostSpecialSorted(count, (dto, postDto) => dto.Likes.CompareTo(postDto.Likes));
        }
    }
}
