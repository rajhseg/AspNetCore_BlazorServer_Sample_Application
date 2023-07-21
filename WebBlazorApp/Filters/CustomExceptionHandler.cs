using Microsoft.AspNetCore.Http;

namespace WebBlazorApp.Filters
{
	public class CustomExceptionHandler
	{
		public CustomExceptionHandler()
		{

		}

		public async Task Invoke(HttpContext context)
		{
            var feature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
            var error = feature?.Error;

            if (error != null)
            {
                //  LogError(error);
                throw error;
            }

            await Task.CompletedTask;
        }
	}
}
