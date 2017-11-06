using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentValidation;
using GameBoy.Net.Devices;
using Retro.Net.Api.RealTime.Models;

namespace Retro.Net.Api.Validation
{
    public class GameBoySocketMessageValidator : AbstractValidator<GameBoySocketMessage>
    {
        private static readonly Regex InvalidCharsRegex = new Regex(@"[^\w\s-]");

        public GameBoySocketMessageValidator(ICollection<string> existingDisplayNames)
        {
            RuleFor(x => x.Button).Must(x => Enum.IsDefined(typeof(JoyPadButton), x.GetValueOrDefault())).WithMessage("{PropertyValue} is not a valid {PropertyName}").When(x => x.Button.HasValue);

            RuleFor(x => x.SetDisplayName)
                .MaximumLength(20)
                .MinimumLength(2)
                .Must(x => !InvalidCharsRegex.IsMatch(x))
                .WithMessage("{PropertyName} can only contain letters, numbers, hyphens and underscores.")
                .Must(x => !existingDisplayNames.Any(n => n.Equals(x, StringComparison.InvariantCultureIgnoreCase)))
                .WithMessage("{PropertyName} {PropertyValue} is already in use.")
                .WithName("Display name")
                .When(x => !string.IsNullOrEmpty(x.SetDisplayName));
        }
    }
}
