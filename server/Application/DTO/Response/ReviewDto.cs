namespace Application.DTO.Response
{
    using Domain.Models;

    public class ReviewDto
    {
        public ReviewDto(Review review, User usr)
            : this(review, usr.UserName, usr.Avatar)
        {
        }

        public ReviewDto(Review review, string userName, string avatar)
        {
            this.UserName = userName;
            this.Avatar = avatar;
            this.Mark = review.Mark;
            this.Description = review.Description;
        }

        public ReviewDto()
        {
        }

        public string UserName { get; set; }

        public string Avatar { get; set; }

        public double Mark { get; set; }

        public string Description { get; set; }
    }
}
