using FoodCornerApi.Database.Models.Common;

namespace FoodCornerApi.Database.Models
{
    public class SubNavbar : BaseEntity, IAuditable
    {
        public string Name { get; set; }
        public string ToURL { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public int NavbarId { get; set; }
        public Navbar Navbar { get; set; }

    }
}
