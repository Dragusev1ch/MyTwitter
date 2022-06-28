using BLL.DTOs.Post;
using BLL.DTOs.User;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class StatisticsController : ControllerBase
    {
        private readonly ILogger<StatisticsController> _logger;

        private readonly StatisticService _statisticsS;
        private readonly UserService _userS;

        public StatisticsController(ILogger<StatisticsController> logger, StatisticService statisticsService,
        UserService userS)
        {
            _logger = logger;
            _statisticsS = statisticsService;
            _userS = userS;
        }
        [HttpGet("topPostsVisited/{count:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<PostDTO>>> ViewPostsTop(uint count)
        {
            var list = await _statisticsS.GetMostViewedTop(count);

            _logger.LogInformation($"Top viewed {count} posts loaded");

            return Ok(list);
        }

        [HttpGet("topPostsVisited")]
        [AllowAnonymous]
        public async Task<ActionResult<UserMainDataDTO>> ViewPostsAll()
        {
            var list = await _statisticsS.GetMostViewed();

            _logger.LogInformation("Top viewed posts loaded");

            return Ok(list);
        }


        [HttpPost("viewTopLikes/{count:int}")]
        public async Task<ActionResult<UserMainDataDTO>> ViewPost(uint count)
        {
           
                var list = await _statisticsS.GetMostPurchasedTop(count);

                _logger.LogInformation($"Top {count} purchased posts loaded");


                return Ok(list);
        }

        [HttpPost("viewTopLikes")]
        public async Task<ActionResult<UserMainDataDTO>> ViewPost()
        {
           
                var list = await _statisticsS.GetMostPurchased();

                _logger.LogInformation("Top purchased posts loaded");

                return Ok(list);
        }
    }
}
