using FluentValidation;
using OneIdentity.Homework.Repository.Models.User;

namespace OneIdentity.Homework.Api.Validation.User;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street).MinimumLength(1).MaximumLength(100);
        RuleFor(x => x.Suite).MinimumLength(1).MaximumLength(100);
        RuleFor(x => x.ZipCode).MinimumLength(1).MaximumLength(100);
        RuleFor(x => x.City).MinimumLength(1).MaximumLength(100);
    }
}
