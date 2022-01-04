namespace Application.DTO.Response.Account
{
    using System.Collections.Generic;

    public class MessageResultDto
    {
        public MessageResultDto(string message, List<string> errors)
        {
            Message = message;
            Errors = errors;
        }

        public string Message { get; set; }

        public List<string> Errors { get; set; }
    }
}
