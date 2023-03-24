using FluentValidation;

namespace CMH.MobileHomeTracker.Infrastructure.Validation
{
    public class HomeValidator : AbstractValidator<Dto.Home>
    {
        public HomeValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(100)
                .NoInvalidCharacters();
        }
    }
}
