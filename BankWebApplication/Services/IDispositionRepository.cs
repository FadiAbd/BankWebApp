using BankWebbApp.Data;
using BankWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWebbApp.Services
{
    public interface IDispositionRepository
    {
        IQueryable<Disposition>GetAllDispositions();
        public void Save();

    }

    public class DispositionRepository : IDispositionRepository 
    {
        protected readonly ApplicationDbContext _dbContext;
        public DispositionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Disposition> GetAllDispositions()
        {
            return _dbContext.Dispositions;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }
}
