using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class TreatmentModel : BaseModel
    {
        public DateTime? Date { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public int? TreatmentClassId { get; set; }
        public virtual TreatmentClassModel TreatmentClass { get; set; }
        public int? TreatmentTypeId { get; set; }
        public virtual TreatmentTypeModel TreatmentType { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public virtual IEnumerable<AttachmentModel> Attachments { get; set; }
        public virtual IEnumerable<AlertModel> Alerts { get; set; }
    }
}
