using BLL.DTOs.Coment;
using DAL.Models;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class ComentService : AService
    {
        public ComentService(IUnitOfWork uow) : base(uow) { }

        private async Task<List<Coment>> GetComents(uint? count)
        {
            if (count.HasValue && Database.Coments.Read().Count() >= count)
            {
                return await Database.Coments.Read().Take((int)count.Value).AsNoTracking().ToListAsync();
            }
            return await Database.Coments.Read().AsNoTracking().ToListAsync();
        }

        public async Task<List<ComentDTO>> GetComentShortDTOs(uint? count)
        {
            var list = await GetComents(count);

            return list.Select(coment => Mapper.Map<ComentDTO>(coment)).ToList();
        }
        public async Task<ComentDTO?> GetMainData(int comentID)
        {
            var coment = await Database.Coments.Read().FirstOrDefaultAsync(coment => coment.ID == comentID);
            if (coment == null) return null;
            return Mapper.Map<ComentDTO?>(coment);
        }


        public async Task<ComentDTO?> Create(ComentDTO comentDTO)
        {
            var coment = Mapper.Map<Coment>(comentDTO);

            await Database.Coments.Create(coment);
            Database.Save();

            return await GetMainData(coment.ID);
        }

        public async Task<ComentDTO?> Update(ComentDTO comentDTO)
        {
            var coment = Mapper.Map<Coment>(comentDTO);

            await Database.Coments.Update(coment);
            Database.Save();

            return await GetMainData(coment.ID);
        }

        public async Task<List<ComentDTO>?> Search(string query)
        {
            var list = await Database.Coments.Read().Where(coment => coment.Description.ToLower().Contains(query.ToLower()) || coment.Description.ToLower().Contains(query.ToLower()))
                .AsNoTracking().Select(coment => Mapper.Map<ComentDTO>(coment)).ToListAsync();

            return list;
        }


        public async Task<bool> Delete(int comentID)
        {
            var result = await Database.Coments.Delete(comentID);

            Database.Save();

            return result;
        }


    }
}
