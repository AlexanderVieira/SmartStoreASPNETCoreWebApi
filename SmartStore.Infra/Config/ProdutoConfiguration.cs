﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStore.Domain.Entities;

namespace SmartStore.Infra.Config
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.TagRFID)
                .IsRequired()
                .HasMaxLength(12);

            builder
                .Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(400);

            builder
                .Property(p => p.Preco)
                .HasColumnType("decimal(10,2)")
                .IsRequired();            

        }
    }
}
