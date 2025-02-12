namespace Ability_Drive_API.Service
{
    public class CalculateRideCostClass
    {
        public static decimal CalculateRideCost(string pickupLocation, string destination) // Changed to public
        {
            // Base fare in EGP
            decimal baseFare = 10.00M;

            // Cost per kilometer in EGP (example: 3 EGP per km)
            decimal costPerKm = 3.00M;

            // Optional: Add a time-based rate in EGP per minute (example: 0.50 EGP/min)
            decimal costPerMinute = 0.50M;

            // Simulate distance calculation (replace with real distance logic)
            double distanceInKm = CalculateDistanceInKm(pickupLocation, destination);

            // Simulate time estimation (replace with real time calculation logic)
            double timeInMinutes = CalculateEstimatedTimeInMinutes(distanceInKm);

            // Calculate cost based on distance and time
            decimal distanceCost = (decimal)distanceInKm * costPerKm;
            decimal timeCost = (decimal)timeInMinutes * costPerMinute;

            // Calculate total cost
            decimal totalCost = baseFare + distanceCost + timeCost;

            // Optional: Add a peak hours surcharge (20% extra, for example)
            if (IsPeakHour())
            {
                totalCost += totalCost * 0.20M; // 20% surcharge
            }

            return Math.Round(totalCost, 2); // Round to 2 decimal places for currency
        }

        private static double CalculateDistanceInKm(string pickupLocation, string destination)
        {
            // Placeholder logic: Use string length difference to simulate distance
            // Replace with real geolocation-based calculation
            int randomDistance = new Random().Next(5, 20); // Random distance in km between 5 and 20
            return randomDistance;
        }

        private static double CalculateEstimatedTimeInMinutes(double distanceInKm)
        {
            double averageSpeedKmPerHour = 40.0; // Average speed in km/h in Egyptian cities
            return (distanceInKm / averageSpeedKmPerHour) * 60; // Convert hours to minutes
        }

        private static bool IsPeakHour()
        {
            var currentHour = DateTime.Now.Hour;
            return (currentHour >= 7 && currentHour <= 9) || (currentHour >= 17 && currentHour <= 19);
        }
    }
}
