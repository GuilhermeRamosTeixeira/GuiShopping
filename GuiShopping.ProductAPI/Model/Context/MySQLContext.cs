using Microsoft.EntityFrameworkCore;

namespace GuiShopping.ProductAPI.Model.Context
{
    public class MySQLContext:DbContext
    {
        public MySQLContext(){}

        public MySQLContext(DbContextOptions<MySQLContext> options) :base(options) {}

        DbSet<Product> products { get; set; }
    }
}
