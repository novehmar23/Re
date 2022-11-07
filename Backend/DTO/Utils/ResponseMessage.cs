
namespace DTO
{ 
    public class ResponseMessage
    {
        public string responseMessage { get; set; }
        public ResponseMessage(string message)
        {
            responseMessage = message;
        }
    }
}