using DAL.EF;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class ComentRepo : IRepository<Coment>
    {
        private readonly MainContext _mainContext;

        public ComentRepo(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public async Task<Coment> Create(Coment item)
        {
            item.ID = 0;
            await _mainContext.Coments.AddAsync(item);

            return item;
        }

        public async Task<bool> Delete(int id)
        {
           var c = await _mainContext.Coments.FindAsync(id);
            if(c == null) return false;

            _mainContext.Coments.Remove(c);
            return true;
        }

        public IQueryable<Coment> Read()
        {
            return _mainContext.Coments.AsQueryable();
        }

        public async Task<bool> Update(Coment item)
        {
            _mainContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return true;
        }
    }
}
