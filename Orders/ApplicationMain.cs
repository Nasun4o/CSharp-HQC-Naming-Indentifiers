using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace StoreCalculator
{
    using StoreCalculator.Models;

    public class ApplicationMain
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var dataMapper = new DataMapper();
            var productsCategory = dataMapper.GetAllCategories();
            var product = dataMapper.GetAllProducts();
            var mostOrderedProduct = dataMapper.GetAllOrders();

            // Names of the 5 most expensive products
            NameOfTopFiveExpensiveProducts(product);

            Console.WriteLine(new string('-', 10));

            // Number of products in each category
            NumberOfProductsInCategory(productsCategory, product);

            Console.WriteLine(new string('-', 10));

            // The 5 top products (by order quantity)
            TopFiveByOrderQuantity(product, mostOrderedProduct);

            Console.WriteLine(new string('-', 10));

            // The most profitable category
            MostProfitableCategory(productsCategory, product, mostOrderedProduct);
        }

        private static void MostProfitableCategory(IEnumerable<Category> productsCategory, IEnumerable<Product> product,IEnumerable<Order> mostOrderedProduct)
        {
            var mostProfitCategory = mostOrderedProduct
                .GroupBy(o => o.ProductId)
                .Select(g => new
                {
                    catId = product
                   .First(p => p.Id == g.Key)
                   .CatId,
                    price = product
                        .First(p => p.Id == g.Key)
                        .UnitPrice,
                    quantity = g
                        .Sum(p => p.Quantity)
                })
                .GroupBy(gg => gg.catId)
                .Select(grp => new
                {
                    categoryName = productsCategory
                    .First(c => c.Id == grp.Key)
                    .Name,
                    totalQuantity = grp
                        .Sum(g => g.quantity * g.price)
                })
                .OrderByDescending(g => g.totalQuantity)
                .First();

            Console.WriteLine("{0}: {1}", mostProfitCategory.categoryName, mostProfitCategory.totalQuantity);
        }

        private static void TopFiveByOrderQuantity(IEnumerable<Product> product, IEnumerable<Order> mostOrderedProduct)
        {
            var topFiveOrderedProducts = mostOrderedProduct
                .GroupBy(o => o.ProductId)
                .Select(grp => new
                {
                    Product = product
                   .First(p => p.Id == grp.Key)
                   .Name,
                    Quantities = grp
                   .Sum(grpgrp => grpgrp.Quantity)
                })
                .OrderByDescending(q => q.Quantities)
                .Take(5);

            foreach (var item in topFiveOrderedProducts)
            {
                Console.WriteLine("{0}: {1}", item.Product, item.Quantities);
            }
        }

        private static void NumberOfProductsInCategory(IEnumerable<Category> productsCategory, IEnumerable<Product> product)
        {
            var numberOfProductsInCategory = product
                .GroupBy(p => p.CatId)
                .Select(grp => new
                {
                    Category = productsCategory
                   .First(c => c.Id == grp.Key)
                   .Name,
                    Count = grp
                   .Count()
                })
                .ToList();

            foreach (var item in numberOfProductsInCategory)
            {
                Console.WriteLine("{0}: {1}", item.Category, item.Count);
            }
        }

        private static void NameOfTopFiveExpensiveProducts(IEnumerable<Product> product)
        {
            var topFiveExpensiveProducts = product
                .OrderByDescending(p => p.UnitPrice)
                .Take(5)
                .Select(p => p.Name);

            Console.WriteLine(string.Join(Environment.NewLine, topFiveExpensiveProducts));
        }
    }
}
