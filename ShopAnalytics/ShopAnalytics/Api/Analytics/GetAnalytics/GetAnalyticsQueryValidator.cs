using FluentValidation;

namespace ShopAnalytics.Api.Analytics.GetAnalytics;

public class GetAnalyticsQueryValidator : AbstractValidator<GetAnalyticsQuery>
{
    public GetAnalyticsQueryValidator()
    {
        RuleFor(x => x.ShopId).NotEmpty().WithMessage("ShopId cannot be empty.");
        RuleFor(x => x.FromDate).NotEmpty().WithMessage("FromDate cannot be empty.");
        RuleFor(x => x.ToDate).NotEmpty().WithMessage("ToDate cannot be empty.");
        RuleFor(x => x.FromDate).LessThanOrEqualTo(x => x.ToDate).WithMessage("FromDate cannot be greater than ToDate.");
    }
    
}