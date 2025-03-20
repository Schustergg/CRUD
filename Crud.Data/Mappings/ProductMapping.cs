using Crud.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal");

            builder.Ignore(p => p.Validation);

            builder.ToTable("Products");
        }
    }
}
