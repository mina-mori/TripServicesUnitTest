using Moq;
using System;
using System.Collections.Generic;
using TripServiceProject;
using Xunit;

namespace TripServiceTest
{
    public class TripServiceTests
    {
        [Fact]
        public void GetActiveUserTripsByUserName_ReturnsActiveTrips()
        {
            // Arrange
            string username = "testuser";
            DateTime futureDate = DateTime.Now.AddDays(1);
            DateTime pastDate = DateTime.Now.AddDays(-1);

            var mockTripRepository = new Mock<ITripRepository>();
            mockTripRepository.Setup(repo => repo.GetTripsByUserName(username)).Returns(new List<Trip>
            {
                new Trip { Destination = "Dest1", TripDate = futureDate, IsCancelled = true },
                new Trip { Destination = "Dest2", TripDate = pastDate, IsCancelled = true },
                new Trip { Destination = "Dest3", TripDate = futureDate, IsCancelled = false }
            });

            var tripService = new TripService(mockTripRepository.Object);

            // Act
            List<Trip> activeTrips = tripService.GetActiveUserTripsByUserName(username);

            // Assert
            Assert.NotNull(activeTrips);
            Assert.Equal(1, activeTrips.Count);
            Assert.Equal("Dest1", activeTrips[0].Destination);
        }

    }
}