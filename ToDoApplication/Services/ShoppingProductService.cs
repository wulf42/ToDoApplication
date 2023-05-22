using ToDoApplication.Context;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication.Services
{
    public class ShoppingProductService : IShoppingProductService
    {
        private readonly ToDoApplicationDbContext _context;

        public ShoppingProductService(ToDoApplicationDbContext context)
        {
            _context = context;
        }

        public int Save(ShoppingProduct product)
        {
            _context.ShoppingProducts.Add(product);
            _context.SaveChanges();
            return product.ProductId;
        }

        public int Edit(int shoppingProductId, ShoppingProduct updatedProduct)
        {
            //funkcja edytująca produkt z listy zakupów
            var existingProduct = _context.ShoppingProducts.Find(shoppingProductId);
            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Quantity = updatedProduct.Quantity;

                _context.SaveChanges();
                return existingProduct.ProductId;
            }

            return -1;
        }

        public int Delete(int productId)
        {
            var product = _context.ShoppingProducts.Find(productId);

            //remove shopping product
            if (product != null)
            {
                _context.ShoppingProducts.Remove(product);
                _context.SaveChanges();
                return product.ProductId;
            }
            return -1;
        }
    }
}