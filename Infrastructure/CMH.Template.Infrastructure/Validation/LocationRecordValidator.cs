using FluentValidation;

namespace CMH.MobileHomeTracker.Infrastructure.Validation
{
    public class LocationRecordValidator : AbstractValidator<Dto.LocationRecord>
    {
        public LocationRecordValidator()
        {
            RuleFor(r => r.Latitude)
                .GreaterThan(0);

            RuleFor(r => r.Longitude)
                .GreaterThan(0);
        }
    }
}
