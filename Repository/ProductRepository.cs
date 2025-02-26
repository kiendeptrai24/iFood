using iFood.Data;
using iFood.Data.Enum;
using iFood.Interfaces;
using iFood.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Interfaces;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDBContext _context;
    public ProductRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public bool Add(Product product)
    {
        _context.Products.Add(product);
        return Save();
    }

    public void CheckAttackProduct(Product product)
    {
        _context.Attach(product);
    }

    public bool Delete(Product product)
    {
        _context.Products.Remove(product);
        return Save();
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(c => c.ProductID == id);
    }

    public async Task<Product> GetByIdAsyncNoTracking(int id)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(c => c.ProductID == id);
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Products.CountAsync();
    }

    public async Task<int> GetCountByCategoryAsync(ProductCategory category)
    {
        return await _context.Products.CountAsync(c => c.Category == category);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAndSliceAsync(ProductCategory category, int offset, int size)
    {
                return await _context.Products
            .Where(c => c.Category == category)
            .Skip(offset)
            .Take(size)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetSliceAsync(int offset, int size)
    {
        return await _context.Products.Skip(offset).Take(size).ToListAsync();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool Update(Product product)
    {
        _context.Products.Update(product);
        return Save();
    }
}
