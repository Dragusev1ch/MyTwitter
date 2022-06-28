using AutoMapper;
using BLL.Mapper;
using DAL.UOW;

namespace BLL.Services
{
    public class AService : IDisposable
    {
        protected IUnitOfWork Database;
        protected AutoMapper.Mapper Mapper;

        protected AService(IUnitOfWork uow)
        {
            Database = uow;

            var configuratin = new MapperConfiguration(cfg => { cfg.AddProfile<MainProfile>(); });
            Mapper = new AutoMapper.Mapper(configuratin);
        }

        public void Dispose()
        {
           Database.Dispose();
        }
    }
}
