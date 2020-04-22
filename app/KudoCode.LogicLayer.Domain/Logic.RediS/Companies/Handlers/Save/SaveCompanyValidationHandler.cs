using KudoCode.LogicLayer.Dtos.Companies;
using FluentValidation;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;

namespace KudoCode.LogicLayer.Domain.Logic.Companies.Handlers.Save
{
	public class SaveCompanyValidationHandler : AbstractValidator<SaveCompanyRequest>
	{
		public SaveCompanyValidationHandler()
		{
			RuleFor(x => x).NotEmpty().NotNull();
		}
	}
}


