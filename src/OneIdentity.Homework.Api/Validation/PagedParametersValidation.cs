using FluentValidation;
using OneIdentity.Homework.Api.Parameters;

namespace OneIdentity.Homework.Api.Validation;

public partial class PagedParametersValidation : AbstractValidator<PagedParameters>
{
    public PagedParametersValidation()
    {
        RuleFor(x => x.PageSize).LessThan(100);
    }
}
