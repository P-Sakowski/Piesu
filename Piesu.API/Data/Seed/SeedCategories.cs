using Microsoft.EntityFrameworkCore;
using Piesu.API.Models;
using System.Collections;
using System.Collections.Generic;

namespace Piesu.API.Data.Seed
{
    public static class SeedCategories
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var services = new Category
            {
                Id = 1,
                Name = "Usługi",
            };

            var walking = new Subcategory
            {
                Id = 1,
                Name = "Wyprowadzanie",
                CategoryId = 1
            };
            var groomer = new Subcategory
            {
                Id = 2,
                Name = "Psi fryzjer",
                CategoryId = 1
            };
            var vet = new Subcategory
            {
                Id = 3,
                Name = "Weterynarz",
                CategoryId = 1
            };
            var care = new Subcategory
            {
                Id = 4,
                Name = "Opieka tymczasowa",
                CategoryId = 1

            };

            /////////////////////////////////////////////////

            var accessories = new Category
            {
                Id = 2,
                Name = "Akcesoria",
            };

            var toys = new Subcategory
            {
                Id = 5,
                Name = "Zabawki",
                CategoryId = 2
            };
            var kennels = new Subcategory
            {
                Id = 6,
                Name = "Budy",
                CategoryId = 2
            };
            var clothes = new Subcategory
            {
                Id = 7,
                Name = "Ubrania",
                CategoryId = 2
            };
            var lanyards = new Subcategory
            {
                Id = 8,
                Name = "Smycze",
                CategoryId = 2
            };
            var bedding = new Subcategory
            {
                Id = 9,
                Name = "Legowiska",
                CategoryId = 2
            };

            /////////////////////////////////////////////////

            var dogs = new Category
            {
                Id = 3,
                Name = "Psy",
            };

            var purchase = new Subcategory
            {
                Id = 10,
                Name = "Kupno",
                CategoryId = 3
            };
            var adoption = new Subcategory
            {
                Id = 11,
                Name = "Adopcja",
                CategoryId = 3
            };

            modelBuilder.Entity<Subcategory>()
            .HasData(
                walking,
                groomer,
                vet,
                care,
                toys,
                bedding,
                kennels,
                clothes,
                lanyards,
                purchase,
                adoption);

            modelBuilder.Entity<Category>()
            .HasData(
                services,
                accessories,
                dogs);
        }
    }
}
