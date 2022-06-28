using DAL.EF;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class PostRepo : IRepository<Post>
    {
        private readonly MainContext _mainContext;

        public PostRepo(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public async Task<Post> Create(Post item)
        {
            item.ID = 0;
            await _mainContext.Posts.AddAsync(item);

            return item;
        }

        public async Task<bool> Delete(int id)
        {
           var p = await _mainContext.Posts.Include(post => post.Coments).FirstOrDefaultAsync();
            if (p == null) return false;

            _mainContext.Posts.Remove(p);
            return true;
        }

        public IQueryable<Post> Read()
        {
            return _mainContext.Posts.AsQueryable();
        }

        public async Task<bool> Update(Post item)
        {
            _mainContext.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
