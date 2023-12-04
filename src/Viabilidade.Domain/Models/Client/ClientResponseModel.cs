namespace Viabilidade.Domain.Models.Client
{
    public class ClientResponseModel
    {
        public int StatusCode { get; set; }
        public string Data { get; set; }

        public ClientResponseModel(int statusCode, string data)
        {
            StatusCode = statusCode;
            Data = data;
            
        }
    }
}
