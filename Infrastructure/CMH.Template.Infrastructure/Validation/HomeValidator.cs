using FluentValidation;

namespace CMH.MobileHomeTracker.Infrastructure.Validation
{
    public class HomeValidator : AbstractValidator<Dto.Home>
    {
        public HomeValidator()
        {
            RuleFor(r => r.Model)
                .NotEmpty()
                .MaximumLength(100)
                .NoInvalidCharacters();
        }
    }
}
