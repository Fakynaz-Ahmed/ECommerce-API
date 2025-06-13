using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using E_Commerce.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbcontext ,
                             RoleManager<IdentityRole> _roleManager , 
                             UserManager<ApplicationUser> _userManager , 
                             StoreIdentityDbContext _identityDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations =await _dbcontext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                {
                   await _dbcontext.Database.MigrateAsync();
                }

                if (!_dbcontext.ProductBrands.Any())
                {
                    var path = @"P:\.NET course_Route\Asp.NET Core Web_ API\E-Commerce.Web\Persistence\Data\DataSeed\brands.json";
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"❌ stoooooooooooooooooooooop File not found: {path}");
                    }
                    var ProductBrandData = File.OpenRead(path);
                    var ProductBrands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                         await _dbcontext.ProductBrands.AddRangeAsync(ProductBrands);
                }
               
                if (!_dbcontext.ProductTypes.Any())
                {
                    var path = @"P:\.NET course_Route\Asp.NET Core Web_ API\E-Commerce.Web\Persistence\Data\DataSeed\types.json";
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"❌ File not found: {path}");
                    }
                    var ProductTypeData = File.OpenRead(path);
                    //var ProductTypeData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var ProductTypes =await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductTypes is not null && ProductTypes.Any())
                        await _dbcontext.ProductTypes.AddRangeAsync(ProductTypes);
                }

                if (!_dbcontext.Products.Any())
                {
                    var path = @"P:\.NET course_Route\Asp.NET Core Web_ API\E-Commerce.Web\Persistence\Data\DataSeed\products.json";
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"❌ File not found: {path}");
                    }
                    var ProductData = File.OpenRead(path);
                    var Products =await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products is not null && Products.Any())
                       await _dbcontext.Products.AddRangeAsync(Products);
                }

                //DeliveryMethod
                if (!_dbcontext.Set<DeliveryMethod>().Any())
                {
                    var path = @"P:\.NET course_Route\Asp.NET Core Web_ API\E-Commerce.Web\Persistence\Data\DataSeed\delivery.json";
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"❌ File not found: {path}");
                    }
                    var deliveryData = File.OpenRead(path);
                    var deliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(deliveryData);
                    if (deliveryMethods is not null && deliveryMethods.Any())
                        await _dbcontext.Set<DeliveryMethod>().AddRangeAsync(deliveryMethods);
                }

                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //to Do
                Console.WriteLine($"❌ Data seeding failed: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"❌ Data seeding failed: {ex.InnerException.Message}");

                }

            };

        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                   await _roleManager.CreateAsync(new IdentityRole("Admin"));
                   await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                         Email = "Mohamed@gmail.com",
                         DisplayName ="Mohamed Tarek",
                         PhoneNumber = "0123456789",
                         UserName="MohamedTarek"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Mohamed",
                        PhoneNumber = "0123456789",
                        UserName = "SalmaMohamed"
                    };

                   await _userManager.CreateAsync(User01 , "P@ssw0rd");
                   await _userManager.CreateAsync(User02 , "P@ssw0rd");

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }

                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex) { }
        }
    }
}
