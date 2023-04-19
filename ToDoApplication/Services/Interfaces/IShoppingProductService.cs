using ToDoApplication.Models;

namespace ToDoApplication.Services.Interfaces
{
    public interface IShoppingProductService
    {
        int Edit(int shoppingProductId, ShoppingProduct body);
        int Save(ShoppingProduct body);
        int Delete(int productId);
    }
}
