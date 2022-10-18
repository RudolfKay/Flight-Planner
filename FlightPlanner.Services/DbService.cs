using Microsoft.EntityFrameworkCore;
using FlightPlanner.Core.Services;
using System.Collections.Generic;
using FlightPlanner.Core.Models;
using FlightPlanner.Data;
using System.Linq;

namespace FlightPlanner.Services
{
    public class DbService : IDbService
    {
        protected IFlightPlannerDbContext _context;

        public DbService(IFlightPlannerDbContext context)
        {
            _context = context;
        }

        public ServiceResult Create<T>(T entity) where T : Entity
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return new ServiceResult(true).SetEntity(entity);
        }

        public ServiceResult Delete<T>(T entity) where T : Entity
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

            return new ServiceResult(true);
        }

        public ServiceResult Update<T>(T entity) where T : Entity
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            
            return new ServiceResult(true).SetEntity(entity);
        }

        public List<T> GetAll<T>() where T : Entity
        {
            return _context.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
