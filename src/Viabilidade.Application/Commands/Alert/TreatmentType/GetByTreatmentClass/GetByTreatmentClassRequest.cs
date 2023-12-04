using MediatR;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.TreatmentType.GetByTreatmentClass
{
    public class GetByTreatmentClassRequest : IRequest<IEnumerable<TreatmentTypeModel>>
    {
        public int TreatmentClassId { get; private set; }

        public GetByTreatmentClassRequest(int treatmentClassId)
        {
            TreatmentClassId = treatmentClassId;
        }

    }
}