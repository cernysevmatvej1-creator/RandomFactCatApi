using Microsoft.EntityFrameworkCore;
using ApiDeepSeek.Models;
using ApiDeepSeek.models;

namespace GroupApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users {  get; set; }
    }
}