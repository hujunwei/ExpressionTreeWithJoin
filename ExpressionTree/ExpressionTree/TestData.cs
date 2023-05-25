namespace ExpressionTree;

/// <summary>
/// Just seed some test data.
/// </summary>
public class TestData
{
    public void Seed()
    {
        using var context = new MyDbContext();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var customers = new List<Customer>
        {
            new() { Name = "Alice" },
            new() { Name = "Bob" },
            new() { Name = "Charlie" }
        };
        context.Customers.AddRange(customers);
        context.SaveChanges();

        var orders = new List<Order>
        {
            new Order { TotalAmount = 100, CustomerId = customers[0].Id },
            new Order { TotalAmount = 200, CustomerId = customers[1].Id },
            new Order { TotalAmount = 300, CustomerId = customers[1].Id },
            new Order { TotalAmount = 400, CustomerId = customers[2].Id },
            new Order { TotalAmount = 500, CustomerId = customers[2].Id }
        };
        context.Orders.AddRange(orders);

        // 保存更改到数据库
        context.SaveChanges();
    }
}