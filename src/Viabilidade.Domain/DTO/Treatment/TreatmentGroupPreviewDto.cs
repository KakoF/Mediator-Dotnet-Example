using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.DTO.Treatment
{
    public class TreatmentGroupPreviewDto
    {
        public TreatmentModel Treatment { get; set; }
        public IEnumerable<TreatmentCalc> ClassCalc { get; set; }
        public int TreatmentsQuantity { get; set; }
        public EntityRuleModel EntityRule { get; set; }
        public ParameterModel Parameter { get; set; }
        public AlertModel Alert { get; set; }
    }
    public class TreatmentCalc
    {
        public int TreatmentClassId { get; set; }
        public string TreatmentClass { get; set; }
        public decimal Percentage { get; set; }
        public int Quantity { get; set; }
        public TreatmentCalc()
        {
                
        }
        public TreatmentCalc(int treatmentClassId, string treatmentClass, decimal valor, int quantity)
        {
            TreatmentClassId = treatmentClassId;
            TreatmentClass = treatmentClass;
            Percentage = decimal.Round(valor, 2);
            Quantity = quantity;
        }

        public IEnumerable<TreatmentCalc> CalcularClasses(IEnumerable<TreatmentByEntityRuleGroupDto> list)
        {
            if (list == null)
                return null;

            var calculo = new List<TreatmentCalc>();
            foreach (var detail in list.DistinctBy(x => x.TreatmentClassId))
            {
                calculo.Add(new TreatmentCalc(
                    list.Where(x => x.TreatmentClassId == detail.TreatmentClassId).Select(x => x.TreatmentClassId).FirstOrDefault(),
                    list.Where(x => x.TreatmentClassId == detail.TreatmentClassId).Select(x => x.TreatmentClass).FirstOrDefault(),
                    (decimal)(list.Count(x => x.TreatmentClassId == detail.TreatmentClassId) / (decimal)list.Count() * 100),
                    list.Where(x => x.TreatmentClassId == detail.TreatmentClassId).Select(x => x.TreatmentClassId).Count()));

            }
            return calculo;
        }

    }
}
