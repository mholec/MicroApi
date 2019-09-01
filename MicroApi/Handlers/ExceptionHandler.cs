using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MicroApi.Handlers
{
    public class ExceptionHandler : HandlerBase
    {
        public ExceptionHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
        
        public async Task ProcessException(Exception exception)
        {
            await InternalServerError(exception.Message);
        }
    }
}