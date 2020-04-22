using FluentValidation;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Outbound;

namespace KudoCode.LogicLayer.Domain.Logic.CompanyRediSs.Handlers.Search
{
	public class SearchRediSValidationHandler
		: AbstractValidator<SearchRediSRequest>
	{
		public SearchRediSValidationHandler()
		{
			RuleFor(x => x).NotEmpty().NotNull();
			RuleFor(x => x.SearchText).NotEmpty().NotNull();
			RuleFor(x => x.IndexName).NotEmpty().NotNull();
		}
	}
}


