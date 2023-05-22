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
            return product.productId;
        }

        public int Edit(int shoppingProductId, ShoppingProduct updatedProduct)
        {
            //funkcja edytująca produkt z listy zakupów
            var existingProduct = _context.ShoppingProducts.Find(shoppingProductId);
            if (existingProduct != null)
            {
                existingProduct.name = updatedProduct.name;
                existingProduct.quantity = updatedProduct.quantity;

                _context.SaveChanges();
                return existingProduct.productId;
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
                return product.productId;
            }
            return -1;
        }
    }
}