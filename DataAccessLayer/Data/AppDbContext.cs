using DataAccessLayer.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<MembershipModel> Memberships { get; set; }
        public DbSet<CustomerMembershipModel> CustomerMemberships { get; set; }
    }
}
