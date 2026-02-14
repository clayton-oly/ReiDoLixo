using Microsoft.EntityFrameworkCore;
using ReiDoLixo.Api.Models;

namespace ReiDoLixo.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
}