using Microsoft.EntityFrameworkCore;


namespace TestOgSikkerhedH4.Controllers.Models
{
    public class Dbcontext:DbContext
    {
        public Dbcontext(DbContextOptions option) : base(option) { }

        public DbSet<Login.Login> login { get; set; }

       
    }
}
