namespace Application.DTO.Response.Account
{
    using System;
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
            this.CartCount = user.CartItems?.Count;
            this.WishListCount = user.WishedItems?.Count;
        }

        public UserInfo(User user)
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.SecondName = user.SecondName;
            this.PhoneNumber = user.PhoneNumber;
            this.Email = user.Email;
            this.GoogleMail = user.GoogleMail;
            this.Avatar = user.Avatar;
            this.CartCount = user.CartItems?.Count;
            this.WishListCount = user.WishedItems?.Count;
            this.Banned = user.Banned;
        }

        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string GoogleMail { get; set; }

        public string PhoneNumber { get; set; }

        public string Avatar { get; set; }

        public int? CartCount { get; set; }

        public int? WishListCount { get; set; }

        public bool Banned { get; set; }
    }
}