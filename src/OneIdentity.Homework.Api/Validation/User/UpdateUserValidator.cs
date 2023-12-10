using FluentValidation;
using OneIdentity.Homework.Repository.Models.User;

namespace OneIdentity.Homework.Api.Validation.User;

public partial class UpdateUserValidator : AbstractValidator<UpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.PhoneNumber).Matches(Regexes.PhoneRegex).When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        RuleFor(x => x.Nickname).MaximumLength(100);
        RuleFor(x => x.BirthDate).NotEmpty();
    }
}
