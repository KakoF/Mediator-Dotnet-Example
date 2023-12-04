using MediatR;
using Microsoft.AspNetCore.Http;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Treatment.Create
{
    public class CreateTreatmentRequest : IRequest<TreatmentModel>
    {
        public IEnumerable<int> AlertIds { get; set; }
        public int TreatmentClassId { get; set; }
        public int TreatmentTypeId { get; set; }
        public string Description { get; set; }
        public bool? Disable { get; set; }
        public bool? Mute { get; set; }
        public int? MuteDays { get; set; }
        public IEnumerable<IFormFile> Attachments { get; set; }
    }

}
