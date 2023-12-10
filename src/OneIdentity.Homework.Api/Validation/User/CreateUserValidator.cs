using FluentValidation;
using OneIdentity.Homework.Repository.Models.User;

namespace OneIdentity.Homework.Api.Validation.User;

public partial class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.UserName).NotEmpty().MinimumLength(5).MaximumLength(100);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(5).MaximumLength(100);
        RuleFor(x => x.PhoneNumber).Matches(Regexes.PhoneRegex).When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        RuleFor(x => x.Nickname).MaximumLength(100);
        RuleFor(x => x.Nickname).MinimumLength(5).When(x => !string.IsNullOrEmpty(x.Nickname));
        RuleFor(x => x.BirthDate).NotEmpty();
    }
}
