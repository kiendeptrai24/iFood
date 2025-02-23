using iFood.Data.Enum;
using iFood.Models;

namespace iFood.Interfaces;

public interface ICartRepository
{
    Task<IEnumerable<Cart>> GetAll();
    Task<Cart> GetByIdAsync(int id);
    Task<IEnumerable<Cart>> GetSliceAsync(int offset, int size);
    Task<IEnumerable<Cart>> GetCartsByCategoryAndSliceAsync(ProductCategory category, int offset, int size);
    Task<int> GetCountAsync();
    Task<int> GetCountByCategoryAsync(ProductCategory category);
    Task<Cart> GetByIdAsyncNoTracking(int id);
    bool Add(Cart cart);
    bool Update(Cart cart);
    bool Delete(Cart cart);
    bool Save();
}
