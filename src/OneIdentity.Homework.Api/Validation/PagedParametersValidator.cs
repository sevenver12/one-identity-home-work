using FluentValidation;
using OneIdentity.Homework.Api.Parameters;

namespace OneIdentity.Homework.Api.Validation;

public partial class PagedParametersValidator : AbstractValidator<PagedParameters>
{
    public PagedParametersValidator()
    {
        RuleFor(x => x.PageSize).LessThan(100);
    }
}
