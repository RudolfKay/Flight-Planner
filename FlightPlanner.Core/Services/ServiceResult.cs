using FlightPlanner.Core.Interfaces;
using System.Collections.Generic;

namespace FlightPlanner.Core.Services
{
    public class ServiceResult
    {
        public ServiceResult(bool success)
        {
            Success = success;
            Errors = new List<string>();
        }

        public ServiceResult SetEntity(IEntity entity)
        {
            Entity = entity;
            return this;
        }

        public ServiceResult AddError(string Error)
        {
            Errors.Add(Error);
            return this;
        }

        public bool Success { get; private set; }

        public IEntity Entity { get; private set; }

        public IList<string> Errors { get; private set; }

        public string FormattedErrors => string.Join(",", Errors);
    }
}
