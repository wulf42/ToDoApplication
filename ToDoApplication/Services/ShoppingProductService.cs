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
    
        public int Edit(int shoppingProductId, ShoppingProduct body)
        {
            //funkcja edytująca produkt z listy zakupów
            var shoppingProduct = _context.ShoppingProducts.Find(shoppingProductId);

            shoppingProduct.name = body.name;
            shoppingProduct.quantity = body.quantity;

            _context.SaveChanges();
            return shoppingProduct.productId;
        }
    }
}
