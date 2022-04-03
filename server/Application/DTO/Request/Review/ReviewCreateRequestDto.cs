namespace Application.DTO.Request.Review
{
    using Domain.Models;

    public class ReviewCreateRequestDto : ReviewRequestDto
    {
        public override Review ToModel()
        {
            return new Review()
            {
                ProductId = this.ProductId,
                Description = this.Description,
                Mark = this.Mark,
                Product = null,
                User = null,
            };
        }
    }
}
