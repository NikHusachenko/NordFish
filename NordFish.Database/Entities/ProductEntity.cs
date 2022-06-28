using NordFish.Database.Enumes;

namespace NordFish.Database.Entities
{
    public class ProductEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float Weight { get; set; }
        public ProductTypes Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public long? UserFK { get; set; }
        public UserEntity User { get; set; }
    }
}
