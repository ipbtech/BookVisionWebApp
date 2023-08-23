﻿using BookVisionWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookVisionWebApp.DAL.Config
{
    public class BookDbConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Id).HasColumnName("book_id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Author).HasColumnName("author").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Title).HasColumnName("titile").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(500);
            builder.Property(x => x.Price).HasColumnName("price").IsRequired();
        }
    }
}
