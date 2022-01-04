namespace Application.DTO.Request.Account
{
    using System.ComponentModel.DataAnnotations;

    public class GoogleLoginDto
    {
        [Required]
        [MaxLength(2000)]
        public string Token { get; set; }
    }
}
