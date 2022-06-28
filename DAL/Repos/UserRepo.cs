using DAL.EF;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class UserRepo : IRepository<User>
    {
        private readonly MainContext _mainContext;

        public UserRepo(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public async Task<User> Create(User item)
        {
            item.ID = 0;
            item.IsAdmin = false;

            await _mainContext.AddAsync(item);
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var p = await _mainContext.Users
           .Include(user => user.Posts)
           .FirstOrDefaultAsync(user => user.ID == id);
            if (p == null) return false;

            _mainContext.Users.Remove(p);
            return true;
        }

        public IQueryable<User> Read()
        {
            return _mainContext.Users.AsQueryable();
        }
            
        public async Task<bool> Update(User item)
        {
            _mainContext.Entry(item).State = EntityState.Modified; //TODO may be wrong, check!
            return true; //TODO add check?
        }
    }
}
