using KudoCode.LogicLayer.Dtos.CompanyRediSs;
using FluentValidation;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Inbound;

namespace KudoCode.LogicLayer.Domain.Logic.CompanyRediSs.Handlers.Save
{
	public class SaveCompanyRediSValidationHandler : AbstractValidator<SaveCompanyRediSRequest>
	{
		public SaveCompanyRediSValidationHandler()
		{
			RuleFor(x => x).NotEmpty().NotNull();
		}
	}
}


