using System;

namespace AirlinePlanificator.Utilities
{
    public static class FlightCalculator
    {
        public static double FlightDistance(double fromLat, double fromLon, double toLat, double toLon)
        {
            //The usual PI/180 constant
            double DEG_TO_RAD = 0.017453292519943295769236907684886;
            //Earth's quatratic mean radius for WGS-84
            double EARTH_RADIUS_IN_METERS = 6372797.560856;

            double latitudeArc = (fromLat - toLat) * DEG_TO_RAD;
            double longitudeArc = (fromLon - toLon) * DEG_TO_RAD;
            double latitudeH = Math.Sin(latitudeArc * 0.5);
            latitudeH *= latitudeH;
            double lontitudeH = Math.Sin(longitudeArc * 0.5);
            lontitudeH *= lontitudeH;
            double tmp = Math.Cos(fromLat * DEG_TO_RAD) * Math.Cos(toLat * DEG_TO_RAD);
            tmp = 2.0 * Math.Asin(Math.Sqrt(latitudeH + tmp * lontitudeH));

            return EARTH_RADIUS_IN_METERS * tmp / 1000.0;
        }

        /// <summary>
        /// calculate the flight time depending on the distance
        /// </summary>
        /// <param name="distance">Distance of the flight (going only in Km)</param>
        /// <param name="planeViewModel">Plane indicator</param>
        /// <returns>datetime of the flight duration</returns>
        public static double FlightTime(double distance, ViewModels.PlaneViewModel plane)
        {
            if (distance == 0 || plane.Speed == 0)
                return 0;

            double time = distance / plane.Speed * 2.0;
            int hour = ((int)time) + 2;
            double minute = Math.Ceiling((time % 1) * 60.0 / 15.0) * 15.0 / 60.0;

            return hour + minute;
        }
    }
}
