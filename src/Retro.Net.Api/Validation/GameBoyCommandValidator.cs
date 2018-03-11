using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentValidation;
using Retro.Net.Api.RealTime.Messages.Command;

namespace Retro.Net.Api.Validation
{
    public class GameBoyCommandValidator : AbstractValidator<GameBoyCommand>
    {
        public GameBoyCommandValidator(ICollection<string> existingDisplayNames)
        {
            RuleFor(x => x.PressButton)
                .SetValidator(new RequestGameBoyJoyPadButtonPressValidator())
                .When(x => x.ValueCase == GameBoyCommand.ValueOneofCase.PressButton);

            RuleFor(x => x.SetState)
                .SetValidator(new SetGameBoyClientStateValidator(existingDisplayNames))
                .When(x => x.ValueCase == GameBoyCommand.ValueOneofCase.SetState);
        }

        private class RequestGameBoyJoyPadButtonPressValidator : AbstractValidator<RequestGameBoyJoyPadButtonPress>
        {
            public RequestGameBoyJoyPadButtonPressValidator()
            {
                RuleFor(x => x.Button)
                    .IsInEnum()
                    .NotEqual(GameBoyJoyPadButton.None)
                    .WithMessage("{PropertyValue} is not a valid {PropertyName}");
            }
        }

        private class SetGameBoyClientStateValidator : AbstractValidator<SetGameBoyClientState>
        {
            private static readonly Regex InvalidCharsRegex = new Regex(@"[^\w\s-]");

            public SetGameBoyClientStateValidator(ICollection<string> existingDisplayNames)
            {
                RuleFor(x => x.DisplayName)
                    .MaximumLength(20)
                    .MinimumLength(2)
                    .Must(x => !InvalidCharsRegex.IsMatch(x))
                    .WithMessage("{PropertyName} can only contain letters, numbers, hyphens and underscores.")
                    .Must(x => !existingDisplayNames.Any(n => n.Equals(x, StringComparison.InvariantCultureIgnoreCase)))
                    .WithMessage("{PropertyName} {PropertyValue} is already in use.");
            }
        }
    }
}
