using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ExpressionTree
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // Seed Test Data
            new TestData().Seed();

            // Read the query
            var json = File.ReadAllText("query.json");
            var query = JsonConvert.DeserializeObject<Query>(json);

            // Build IQueryable from query.json
            using var context = new MyDbContext();
            IQueryable<Customer> queryable = QueryGeneratorEngine.BuildQuery<Customer>(context, query);
            List<Customer> data = queryable.ToList();

            Console.WriteLine(queryable.ToQueryString());
            Console.WriteLine("Done");
        }
    }
}
