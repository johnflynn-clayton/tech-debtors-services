using FluentValidation;
using System.Text.RegularExpressions;

namespace CMH.MobileHomeTracker.Infrastructure.Validation
{
    public static class CustomValidators
    {
        private static readonly Regex _invalidCharacters = new Regex("[;<>\\[\\]{}&\\$%]", RegexOptions.Compiled);

        /// <summary>
        /// Validation will fail if the string contains one of the following characters: ; &lt; &gt; [ ] { } &amp; $ %
        /// </summary>
        public static IRuleBuilderOptions<T, string> NoInvalidCharacters<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(value => !_invalidCharacters.IsMatch(value))
                .WithMessage("'{PropertyName}' contains an invalid character.");
        }
    }
}
