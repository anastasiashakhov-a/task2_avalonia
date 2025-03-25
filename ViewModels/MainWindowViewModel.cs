using AircraftApp.Models;
using ReactiveUI;
using System;
using System.Reactive;

namespace AircraftApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Aircraft? _selectedAircraft;
        private string _flightLog = string.Empty;
        private double _runwayLength = 1500;

        public Aircraft? SelectedAircraft
        {
            get => _selectedAircraft;
            set => this.RaiseAndSetIfChanged(ref _selectedAircraft, value);
        }

        public string FlightLog
        {
            get => _flightLog;
            set => this.RaiseAndSetIfChanged(ref _flightLog, value);
        }

        public double RunwayLength
        {
            get => _runwayLength;
            set => this.RaiseAndSetIfChanged(ref _runwayLength, value);
        }

        public ReactiveCommand<Unit, Unit> CreateAirplaneCommand { get; }
        public ReactiveCommand<Unit, Unit> CreateHelicopterCommand { get; }
        public ReactiveCommand<Unit, Unit> TakeOffCommand { get; }
        public ReactiveCommand<Unit, Unit> LandCommand { get; }

        public MainWindowViewModel()
        {
            CreateAirplaneCommand = ReactiveCommand.Create(CreateAirplane);
            CreateHelicopterCommand = ReactiveCommand.Create(CreateHelicopter);
            TakeOffCommand = ReactiveCommand.Create(TakeOff);
            LandCommand = ReactiveCommand.Create(Land);
        }

        private void CreateAirplane()
        {
            var airplane = new Airplane { RequiredRunwayLength = RunwayLength };
            airplane.FlightEvent += OnFlightEvent;
            SelectedAircraft = airplane;
        }

        private void CreateHelicopter()
        {
            var helicopter = new Helicopter();
            helicopter.FlightEvent += OnFlightEvent;
            SelectedAircraft = helicopter;
        }

        private void TakeOff()
        {
            if (SelectedAircraft != null)
            {
                SelectedAircraft.TakeOff();
            }
        }

        private void Land()
        {
            if (SelectedAircraft != null)
            {
                SelectedAircraft.Land();
            }
        }

        private void OnFlightEvent(object? sender, string message)
        {
            FlightLog += $"{DateTime.Now:T}: {message}\n";
        }
    }
}
