using DAL.Models;
using DAL.Repos;


namespace DAL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; } 
        IRepository<Post> Posts { get; }
        IRepository<Coment> Coments { get; }

        void Save();
    }
}
