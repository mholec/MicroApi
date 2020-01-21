using System.Threading.Tasks;
using MicroApi.Models;
using Microsoft.AspNetCore.Http;
using WebApiContrib.Core.Results;

namespace MicroApi.Handlers
{
    public class UserHandler : HandlerBase
    {
        public UserHandler(IHttpContextAccessor httpContextAccessor) : base((httpContextAccessor))
        {
        }

        public async Task GetUser()
        {
            var info = new
            {
                HttpContext.User.Identity.Name,
                HttpContext.User.Identity.IsAuthenticated
            };

            await HttpContext.Ok(info);
        }

        public async Task CreateUser()
        {
            AppUser user = await FromBody<AppUser>();

            if (!ModelState.IsValid)
            {
                await HttpContext.BadRequest(ModelState);
                return;
            }

            await HttpContext.Ok(user);
        }
    }
}