using Microsoft.EntityFrameworkCore;

namespace ImageVwr.Models
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options)
        {
        }

        public DbSet<ImageModel> Images { get; set; }
        public DbSet<WordModel> Words { get; set; }
    }
}
