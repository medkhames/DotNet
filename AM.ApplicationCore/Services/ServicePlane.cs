using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Domain;
using System.Collections.Generic;

namespace AM.ApplicationCore.Services
{
    public class ServicePlane : Service<Plane>, IServicePlane
    {
        
        public ServicePlane(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public void DeletePlanes()
        {
            Delete(p => p.ManufactureDate.AddYears(10).Year < DateTime.Now.Year);
        }

        public IList<Flight> GetFlights(int n)
        {
            return GetAll().OrderByDescending(p => p.PlaneId).Take(n).SelectMany(p => p.Flights).OrderBy(f => f.FlightDate).ToList();
        }

        public IList<Passenger> GetPassengers(Plane p)
        {
            return GetById(p.PlaneId).Flights.SelectMany(f => f.Tickets.Select(t => t.Passenger)).ToList();
        }

        public bool IsAvailablePlane(Flight f, int n)
        {
            //int capacity = Get(p => p.Flights.Contains(f) == true).Capacity;
            int capacity = f.Plane.Capacity;
            int nbPassengers = f.Tickets.Count();
            //return ;
            return capacity >= (nbPassengers + n);
        }
    }
}
