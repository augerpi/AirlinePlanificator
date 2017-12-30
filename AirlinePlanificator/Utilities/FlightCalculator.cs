using System;

namespace AirlinePlanificator.Utilities
{
    public static class FlightCalculator
    {
        public static Double FlightDistance(Double fromLat, Double fromLon, Double toLat, Double toLon)
        {
            //The usual PI/180 constant
            Double DEG_TO_RAD = 0.017453292519943295769236907684886;
            //Earth's quatratic mean radius for WGS-84
            Double EARTH_RADIUS_IN_METERS = 6372797.560856;

            Double latitudeArc = (fromLat - toLat) * DEG_TO_RAD;
            Double longitudeArc = (fromLon - toLon) * DEG_TO_RAD;
            Double latitudeH = Math.Sin(latitudeArc * 0.5);
            latitudeH *= latitudeH;
            Double lontitudeH = Math.Sin(longitudeArc * 0.5);
            lontitudeH *= lontitudeH;
            Double tmp = Math.Cos(fromLat * DEG_TO_RAD) * Math.Cos(toLat * DEG_TO_RAD);
            tmp = 2.0 * Math.Asin(Math.Sqrt(latitudeH + tmp * lontitudeH));

            return EARTH_RADIUS_IN_METERS * tmp / 1000.0;
        }

        /// <summary>
        /// Calculate the flight time depending on the distance
        /// </summary>
        /// <param name="distance">Distance of the flight (going only in Km)</param>
        /// <param name="plane">Plane indicator</param>
        /// <returns>Flight duration in hour</returns>
        public static Double FlightTimeFromDistance(Double distance, ViewModels.PlaneViewModel plane)
        {
            if (Math.Abs(distance) < Double.Epsilon || plane.Speed == 0)
                return 0;

            Double time = distance / plane.Speed * 2.0;
            Int32 hour = ((Int32)time) + 2;
            Double minute = Math.Ceiling((time % 1) * 60.0 / 15.0) * 15.0 / 60.0;

            return hour + minute;
        }

        /// <summary>
        /// Calculate the distance depending on the flight time
        /// </summary>
        /// <param name="flightTime">Flight duration in hour</param>
        /// <param name="plane">Plane indicator</param>
        /// <returns>Distance of the flight (going only in Km)</returns>
        public static Double DistanceFromFlightTime(Double flightTime, ViewModels.PlaneViewModel plane)
        {
            if (Math.Abs(flightTime) < Double.Epsilon || plane.Speed == 0)
                return 0;

            Double airTime = (flightTime - 2) / 2;
            return airTime * plane.Speed;
        }
    }
}
