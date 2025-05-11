

using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Repositories.Config;

namespace WebAPI.Repositories
{

    //TODO: RepositoryContext class ını DbContext classından kalıtım alacak şekilde oluşturun.
    //TODO: DbContext için Microsoft.EntityFrameworkCore namespace'ini ekleyin.
    //TODO: Bunu Package Manager Console'da yapın.
    public class RepositoryContext : DbContext
    {

        //TODO: DbContextOptions nesnesi verdiğimizde base(options) ile DbContext göndermiş oluyoruz.
        public RepositoryContext(DbContextOptions options) : base(options)
        {
                
        }
        public DbSet<Book> Books { get; set; }

        //TODO: Tip konfigürasyonunu için ilgili kodu ekleyin.
        //TODO: Artık Model oluşturulurken BookConfig sınıfını kullanacağız.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
