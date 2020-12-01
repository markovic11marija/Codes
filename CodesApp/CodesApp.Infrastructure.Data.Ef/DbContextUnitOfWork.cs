using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodesApp.Infrastructure.Data.Ef
{
  public  class DbContextUnitOfWork : IUnitOfWork
    {
        private readonly CodesContext _context;
        private bool _cancelSaving;

        public DbContextUnitOfWork(CodesContext context)
        {
            _context = context; ;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_cancelSaving)
            {
                return 0;
            }

            int numberOfChanges = await _context.SaveChangesAsync(cancellationToken);
            return numberOfChanges;
        }

        public int SaveChanges()
        {
            if (_cancelSaving)
            {
                return 0;
            }


            int numberOfChanges = _context.SaveChanges();
            return numberOfChanges;
        }

        public void CancelSaving()
        {
            _cancelSaving = true;
        }

     
    }
}
