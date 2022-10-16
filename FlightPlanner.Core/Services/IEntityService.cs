using FlightPlanner.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        ServiceResult Create(T entity);

        ServiceResult Delete(T entity);

        ServiceResult Update(T entity);

        List<T> GetAll();

        T getById(int id);

        IQueryable<T> Query();
    }
}
