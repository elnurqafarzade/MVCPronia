﻿using Microsoft.EntityFrameworkCore;
using MVCPronia.Models;
using System.Collections.Generic;


namespace MVCPronia.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
    }
}
