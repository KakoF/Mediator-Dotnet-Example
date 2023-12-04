using Viabilidade.Domain.Models.Base;

namespace Viabilidade.Domain.Models.Alert
{
    public class AttachmentModel : BaseModel
    {
        public int? TreatmentId { get; set; }
        public virtual TreatmentModel Treatment { get; set; }
        public string PathFile { get; set; }
        public DateTime? UploadDate { get; set; }
        public Guid? UserId { get; set; }
        public bool? Active { get; set; }
        public string FileName { get; set; }
        
    }
}
