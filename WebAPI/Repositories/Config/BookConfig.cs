using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Models;

namespace WebAPI.Repositories.Config
{
    //TODO:IEntityTypeConfiguration dan kalıtım alıyoruz. Generic yapıda Book sınıfını alıyoruz.
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //TODO: Migrasyon işlemlerinde tabloda da veri yoksa çekirdek veriler ekleyeceğiz.
            builder.HasData(
                new Book { Id = 1, Title = "Karagöz ve Hacivat", Price = 75 },
                new Book { Id = 2, Title = "Mesnevi", Price = 175 },
                new Book { Id = 3, Title = "Devlet", Price = 375 }                              
            );
        }
    }
}
