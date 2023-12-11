using FluentValidation;
using OneIdentity.Homework.Repository.Models.User;

namespace OneIdentity.Homework.Api.Validation.User;

public class CompanyValidator : AbstractValidator<Company>
{
    public CompanyValidator()
    {
        RuleFor(x=> x.Bs).MinimumLength(1).MaximumLength(100);
        RuleFor(x=> x.CatchPhrase).MinimumLength(1).MaximumLength(100);
        RuleFor(x=> x.Name).MinimumLength(1).MaximumLength(100);
    }
}
