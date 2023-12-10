using FluentValidation;
using OneIdentity.Homework.Repository.Models.User;

namespace OneIdentity.Homework.Api.Validation.User;

public partial class UpdateUserValidator : AbstractValidator<UpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Phone).Matches(Regexes.PhoneRegex).When(x => !string.IsNullOrEmpty(x.Phone));
        RuleFor(x => x.Name).MinimumLength(5).MaximumLength(100);
        RuleFor(x => x.Website).Matches(Regexes.UrlRegex).When(x => !string.IsNullOrEmpty(x.Website));
        RuleFor(x => x.BirthDate).NotEmpty();
    }
}
