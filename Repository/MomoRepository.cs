using iFood.Data;
using iFood.Data.Enum;
using iFood.Interfaces;
using iFood.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Interfaces;

public class MomoRepository : IMomoRepository
{
    private readonly ApplicationDBContext _context;
    public MomoRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public bool Add(MomoInfo momoInfo)
    {
        _context.MomoInfos.Add(momoInfo);
        return Save();
    }

    public bool Delete(MomoInfo momoInfo)
    {
        _context.MomoInfos.Remove(momoInfo);
        return Save();
    }

    public Task<IEnumerable<MomoInfo>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<MomoInfo>> GetAllByUserId()
    {
        throw new NotImplementedException();
    }

    public Task<MomoInfo> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<MomoInfo> GetByIdAsyncNoTracking(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MomoInfo>> GetSliceAsync(int offset, int size)
    {
        throw new NotImplementedException();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool Update(MomoInfo momoInfo)
    {
        throw new NotImplementedException();
    }
}
