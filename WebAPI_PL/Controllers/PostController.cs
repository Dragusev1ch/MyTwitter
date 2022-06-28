using BLL.DTOs.Post;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly PostService _postS;
        private readonly StatisticService _statisticsS;
        private readonly UserService _userS;

        public PostController(PostService postS, StatisticService statisticsS, UserService userS)
        {
            _postS = postS;
            _statisticsS = statisticsS;
            _userS = userS;
        }

        [HttpGet("postsPreview"), AllowAnonymous]
        public async Task<ActionResult<PostDTO>> GetPosts()
        {
            var list = await _postS.GetPostDTOs(null);

            return Ok(list);
        }
        [HttpGet("postsPreview/{count:int}"), AllowAnonymous]
        public async Task<ActionResult<PostDTO>> GetPosts(uint count)
        {
            var list = await _postS.GetPostDTOs(count);

            return Ok(list);
        }
        [HttpGet("postData/{postID:int}"), AllowAnonymous]
        public async Task<ActionResult<PostDTO>> GetPostData(int postID)
        {
            var res = await _statisticsS.AddView(postID);

            var postData = await _postS.GetMainData(postID);

            if (postData == null && !res) return NotFound();


            return Ok(postData);
        }
        [HttpPut("createPost")]
        public async Task<ActionResult<PostDTO>> CreatePost(PostCreateDTO post)
        {
            
                var result = await _postS.Create(post);

                if (result == null) return NotFound();

                return Ok(post);
        }
        [HttpPost("updatePost")]
        public async Task<ActionResult<PostDTO>> UpdatePost(PostDTO post)
        {
            var resAdminOrModerator = await UserController.IsUserAdmin(User, _userS);

            if (resAdminOrModerator)
            {
                var res = await _postS.Update(post);

                if (res == null) return NotFound();

                return Ok(res);
            }


            return Forbid("User must be admin!");
        }

        [HttpDelete("deletePost/{postID:int}")]
        public async Task<ActionResult<PostDTO>> DeletePost(int postID)
        {
            var postData = await _postS.GetMainData(postID);


            var resAdminOrModerator = await UserController.IsUserAdmin(User, _userS);

            if (resAdminOrModerator)
            {
                await _postS.Delete(postID);
                return Ok(postData);
            }


            return Forbid("User must be admin!");
        }

        [HttpGet("searchPost/{query}"), AllowAnonymous]
        public async Task<ActionResult<PostDTO>> SearchPost(string query)
        {
            var list = await _postS.Search(query);

            return Ok(list);
        }
    }
}
