namespace Application.DTO.Request.Review
{
    using Domain.Models;

    public class ReviewUpdateRequestDto : ReviewRequestDto
    {
        public override Review ToModel()
        {
            return new Review()
            {
                ProductId = this.ProductId,
                Mark = this.Mark,
                Description = this.Description,
                Product = null,
                User = null,
            };
        }
    }
}
