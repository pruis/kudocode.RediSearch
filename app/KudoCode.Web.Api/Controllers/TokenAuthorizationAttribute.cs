using Microsoft.AspNetCore.Mvc.Filters;

namespace KudoCode.Web.Api.Controllers
{
	public class TokenAuthenticationAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{

		}
	}
}