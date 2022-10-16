using FlightPlanner.Core.Interfaces;

namespace FlightPlanner.Core.Models
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
