namespace Application.DTO.Response.Account
{
    using Domain.Models;

    public class UserInfo
    {
        public UserInfo(User user, string role)
        {
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.SecondName = user.SecondName;
            this.Role = role;
            this.PhoneNumber = user.PhoneNumber;
            this.Email = user.Email;
            this.GoogleMail = user.GoogleMail;
            this.Avatar = user.Avatar;
        }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string GoogleMail { get; set; }

        public string PhoneNumber { get; set; }

        public string Avatar { get; set; }
    }
}