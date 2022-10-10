using FlightPlanner.Core.Services;
using System.Collections.Generic;
using FlightPlanner.Core.Models;
using System.Linq;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        public EntityService(IFlightPlannerDbContext context) : base(context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            Create<T>(entity);
        }

        public void Delete(T entity)
        {
            Delete<T>(entity);
        }

        public void Update(T entity)
        {
            Update<T>(entity);
        }

        public List<T> GetAll(T entity)
        {
            return GetAll<T>();
        }

        public T getById(int id)
        {
            return GetById<T>(id);
        }

        public IQueryable<T> Query()
        {
            return Query<T>();
        }
    }
}
