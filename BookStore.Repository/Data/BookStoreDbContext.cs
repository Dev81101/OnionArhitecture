using Microsoft.EntityFrameworkCore;
using Bookstore.Domain.Entities;

namespace BookStore.Repository.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        // DbSet for each entity
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints here
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(s => s.Items)
                .WithOne(c => c.ShoppingCart)
                .HasForeignKey(c => c.ShoppingCartId)  // ShoppingCartId is the foreign key in CartItem
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Order-ShoppingCart relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShoppingCart)  // Order has one ShoppingCart
                .WithMany()  // A ShoppingCart can have many orders
                .HasForeignKey(o => o.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
