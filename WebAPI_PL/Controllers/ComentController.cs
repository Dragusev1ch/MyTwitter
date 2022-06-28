using BLL.DTOs;
using BLL.DTOs.Coment;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComentController : ControllerBase
    {
        private readonly ComentService _comentS;
        private readonly StatisticService _statisticsS;
        private readonly UserService _userS;

        public ComentController(ComentService comentS, StatisticService statisticsS, UserService userS)
        {
            _comentS = comentS;
            _statisticsS = statisticsS;
            _userS = userS;
        }
        [HttpGet("comentsPreview"), AllowAnonymous]
        public async Task<ActionResult<ComentDTO>> GetComents()
        {
            var list = await _comentS.GetComentShortDTOs(null);

            return Ok(list);
        }

        [HttpGet("comentsPreview/{count:int}"), AllowAnonymous]
        public async Task<ActionResult<ComentDTO>> GetComents(uint count)
        {
            var list = await _comentS.GetComentShortDTOs(count);

            return Ok(list);
        }

        [HttpGet("comentData/{comentID:int}"), AllowAnonymous]
        public async Task<ActionResult<ComentDTO>> GetComentData(int comentID)
        {
            var res = await _statisticsS.AddView(comentID);

            var comentData = await _comentS.GetMainData(comentID);

            if (comentData == null && !res) return NotFound();


            return Ok(comentData);
        }




        [HttpPut("createComent")]
        public async Task<ActionResult<ComentDTO>> CreateComent(ComentDTO coment)
        {
            
                var result = await _comentS.Create(coment);

                if (result == null) return NotFound();

                return Ok(coment);
           
        }

        [HttpPost("updateComent")]
        public async Task<ActionResult<ComentDTO>> UpdateComent(ComentDTO coment)
        {
            var resAdminOrModerator = await UserController.IsUserAdmin(User, _userS);

            if (resAdminOrModerator)
            {
                var res = await _comentS.Update(coment);

                if (res == null) return NotFound();

                return Ok(res);
            }


            return Forbid("User must be admin or moderator!");
        }

        [HttpDelete("deleteComent/{comentID:int}")]
        public async Task<ActionResult<ComentDTO>> DeleteComent(int comentID)
        {
            var comentData = await _comentS.GetMainData(comentID);


            var resAdminOrModerator = await UserController.IsUserAdmin(User, _userS);

            if (resAdminOrModerator)
            {
                await _comentS.Delete(comentID);
                return Ok(comentData);
            }


            return Forbid("User must be admin or moderator!");
        }

        [HttpGet("searchComent/{query}"), AllowAnonymous]
        public async Task<ActionResult<ComentDTO>> SearchComent(string query)
        {
            var list = await _comentS.Search(query);

            return Ok(list);
        }

    }
}
