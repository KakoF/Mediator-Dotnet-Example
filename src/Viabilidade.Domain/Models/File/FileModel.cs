using Microsoft.AspNetCore.Http;
using Viabilidade.Domain.Models.Enums;

namespace Viabilidade.Domain.Models.File
{
    public class FileModel
    {
        public IEnumerable<IFormFile> Files { get; set; }
        public string UsuarioId { get; set; }
        public string ObjetoId { get; set; }
        public eFileType Tipo { get; set; }
    }
}
