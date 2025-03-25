using System;

namespace AircraftApp.Models
{
    public abstract class Aircraft
    {
        public event EventHandler<string>? FlightEvent;
        
        private double _altitude;
        public double Altitude
        {
            get => _altitude;
            protected set
            {
                _altitude = value;
                OnFlightEvent($"Altitude changed to {_altitude} meters");
            }
        }

        protected void OnFlightEvent(string message)
        {
            FlightEvent?.Invoke(this, message);
        }

        public abstract bool TakeOff();
        public abstract void Land();
    }

    public class Airplane : Aircraft
    {
        public double RequiredRunwayLength { get; set; }

        public override bool TakeOff()
        {
            OnFlightEvent("Airplane is starting takeoff...");
            if (RequiredRunwayLength >= 1000)
            {
                Altitude = 10000;
                OnFlightEvent("Airplane successfully took off!");
                return true;
            }
            OnFlightEvent("Runway is too short for takeoff!");
            return false;
        }

        public override void Land()
        {
            OnFlightEvent("Airplane is landing...");
            Altitude = 0;
            OnFlightEvent("Airplane has landed safely.");
        }
    }

    public class Helicopter : Aircraft
    {
        public override bool TakeOff()
        {
            OnFlightEvent("Helicopter is starting takeoff...");
            Altitude = 500;
            OnFlightEvent("Helicopter successfully took off!");
            return true;
        }

        public override void Land()
        {
            OnFlightEvent("Helicopter is landing...");
            Altitude = 0;
            OnFlightEvent("Helicopter has landed safely.");
        }
    }
}