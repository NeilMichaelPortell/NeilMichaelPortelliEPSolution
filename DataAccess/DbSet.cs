using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeilMichaelPortelliEPSolution.DataAccess
{
    public class DbSet<T>
    {
        internal void Add(Poll poll)
        {
            throw new NotImplementedException();
        }

        internal void Add(Vote vote)
        {
            throw new NotImplementedException();
        }

        internal async Task AddAsync(Poll poll)
        {
            throw new NotImplementedException();
        }

        internal async Task AddAsync(Vote vote)
        {
            throw new NotImplementedException();
        }

        internal void Find(int id)
        {
            throw new NotImplementedException();
        }

        internal async Task FindAsync(int pollId)
        {
            throw new NotImplementedException();
        }

        internal async Task FirstOrDefaultAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

        internal object Include(Func<object, object> value)
        {
            throw new NotImplementedException();
        }

        internal object Include(string v)
        {
            throw new NotImplementedException();
        }

        internal async Task<List<Poll>> ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}