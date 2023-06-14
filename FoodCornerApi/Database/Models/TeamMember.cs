using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class TeamMember : BaseEntity, IAuditable
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? InistagramUrl { get; set; }
        public string? LinkEdinUrl { get; set; }
        public string? FaceBookUrl { get; set; }

        public string MemberImage { get; set; }
        public string MemberİmageInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
