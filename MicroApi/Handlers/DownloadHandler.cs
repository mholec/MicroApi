using System.Threading.Tasks;
using MicroApi.Services;
using Microsoft.AspNetCore.Http;
using WebApiContrib.Core.Results;

namespace MicroApi.Handlers
{
    public class DownloadHandler : HandlerBase
    {
        private readonly DownloadService downloadService;

        public DownloadHandler(DownloadService downloadService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.downloadService = downloadService;
        }
        
        public async Task GetFromUrl()
        {
            string url = HttpContext.Request.Query["url"];
            string content = await downloadService.LoadFromUrl(url);

            await HttpContext.Ok(new {content});
        }
    }
}