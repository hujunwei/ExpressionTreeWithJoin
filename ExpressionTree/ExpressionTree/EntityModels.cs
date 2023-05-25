using Microsoft.EntityFrameworkCore;

namespace ExpressionTree;

/// <summary>
/// Entity models
/// </summary>
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
}

/// <summary>
/// DbContext
/// </summary>
public class MyDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=127.0.0.1,1433;;User ID=sa;Password=P@ssw0rd;Initial Catalog=ExpressionTreeTest;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // FK: Order.CustomerId
        modelBuilder.Entity<Order>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId);
    }
}
