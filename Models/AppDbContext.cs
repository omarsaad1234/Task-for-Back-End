using Microsoft.EntityFrameworkCore;

namespace Task_for_Back_End.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Cashier> Cashier {  get; set; }
        public DbSet<City> Cities {  get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails {  get; set; }
        public DbSet<InvoiceHeader> InvoiceHeader {  get; set; }

    }
}
