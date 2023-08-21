using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripServiceProject
{
    public interface ITripRepository
    {
        List<Trip> GetTripsByUserName(string username);
    }

    public class TripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public List<Trip> GetActiveUserTripsByUserName(string username)
        {
            List<Trip> trips = new List<Trip>();

            List<Trip> allTrips = _tripRepository.GetTripsByUserName(username);

            foreach (Trip trip in allTrips)
            {
                if (trip.TripDate > DateTime.Now && trip.IsCancelled)
                {
                    trips.Add(trip);
                }
            }

            return trips;
        }
    }

    public class Trip
    {
        public string Destination { get; set; }
        public DateTime TripDate { get; set; }
        public bool IsCancelled { get; set; }
    }
}
