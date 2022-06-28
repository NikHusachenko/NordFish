using NordFish.Database.Enumes;

namespace NordFish.Database.Entities
{
    public class UserEntity
    {
        public long Id { get; set; }        
        public string Login { get; set; }
        public string Password { get; set; }       
        public string Email { get; set; }
        public UserTypes Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public List<ProductEntity> Products { get; set; }

        public UserEntity()
        {
            Products = new List<ProductEntity>();
        }
    }
}
