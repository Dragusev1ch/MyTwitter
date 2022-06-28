using DAL.EF;
using DAL.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UOW
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly MainContext _mainContext;
        private bool _disposed = false;

        private IRepository<User> _users;
        public IRepository<User> Users => _users ??= new UserRepo(_mainContext);

        private IRepository<Post> _posts;
        public IRepository<Post> Posts => _posts ??= new PostRepo(_mainContext);
        
        private IRepository<Coment> _coments;
        public IRepository <Coment> Coments => _coments ??= new ComentRepo(_mainContext);


        public EFUnitOfWork(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public void Save()
        {
            _mainContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _mainContext.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
