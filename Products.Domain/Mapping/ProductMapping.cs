using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Mapping
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            ToTable("Produto");

            HasKey(p => p.Id);

            Property(p => p.Id)
                    .HasColumnName("Id")
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                    .IsRequired();

            Property(p => p.Name)
                    .HasColumnName("Nome")
                    .IsRequired();

            Property(p => p.Price)
                   .HasPrecision(16,2)
                   .HasColumnName("Preco")
                   .IsRequired();

            HasRequired(p => p.Category)
                .WithMany(p => p.Products);
        }
    }
}
