using LogCenter.Domain.Entities;
using LogCenter.Infra.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogCenter.Infra.Repositories
{
    public class BaseRepository<T> where T : EntityBase
    {
        protected readonly DatabaseContext Database;

        public BaseRepository(DatabaseContext context)
        {
            Database = context;
        }

        public IQueryable<T> Get()
        {
            return Database.Set<T>().AsQueryable();
        }

        public async virtual Task<T> GetById(int id)
        {
            return await Database.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Save(T objectToSave)
        {
            objectToSave.CreationDate = DateTime.Now;
            Database.Set<T>().Add(objectToSave);
        }

        public void Update(T objectToUpdate)
        {
            Database.Set<T>().Update(objectToUpdate);
        }

        public void Upsert(T objectToUpsert)
        {
            if (objectToUpsert.Id != 0)
                Update(objectToUpsert);
            else
                Save(objectToUpsert);
        }

        public void Delete(T objectToDelete)
        {
            Database.Set<T>().Remove(objectToDelete);
        }

        public void Commit()
        {
            Database.SaveChanges();
        }

        public Task CommitAsync()
        {
            return Database.SaveChangesAsync();
        }
    }
}
