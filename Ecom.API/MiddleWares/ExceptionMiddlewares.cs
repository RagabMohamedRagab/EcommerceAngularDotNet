namespace Ecom.API.MiddleWares
{
    public class ExceptionMiddlewares(RequestDelegate _next)
    {
        public async Task Invoke(HttpContext context) {
            try
            {
               await _next(context);
            }
            catch (Exception ex) { 
            
            }
        
        }
    }
}
