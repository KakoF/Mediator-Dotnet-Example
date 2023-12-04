using FluentValidation;
using Viabilidade.Domain.Interfaces.Services.Host;

namespace Viabilidade.Application.Commands.Alert.Alert.UpdateResponsible.Validators
{
    public class UpdateResponsibleValidator : AbstractValidator<UpdateResponsibleRequest>
    {
        private readonly IHostService _hostService;

        public UpdateResponsibleValidator(IHostService hostService)
        {
            _hostService = hostService;
            RuleFor(a => a.UserId)
                .MustAsync(async (value, c) => await UserExistAsync(value)).WithMessage("Usuário não encontrado");
        }

        private async Task<bool> UserExistAsync(Guid? userId)
        {
            if (userId == null)
                return true;

            var user = await _hostService.GetUserInfoAsync((Guid)userId);
            if (user == null)
                return false;

            return true;
        }
    }
}
