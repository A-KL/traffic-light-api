namespace TraficLight.DashboardApp
{
    using System.Collections.Generic;
    using Windows.Devices.Gpio;
    using TraficLight.DashboardApp.Domain;

    public interface ITrafficLightService
    {
        bool this[string key] { get; set; }

        LightsStatus ToStatus();
    }

    public class TrafficLightService : ITrafficLightService
    {
        private const int RedPin = 10;
        private const int GreenPin = 10;
        private const int YellowPin = 10;

        public TrafficLightService()
        {
            var gpio = GpioController.GetDefault();

            var red = gpio.OpenPin(RedPin);            
            var green = gpio.OpenPin(GreenPin);
            var yellow = gpio.OpenPin(YellowPin);

            red.SetDriveMode(GpioPinDriveMode.InputPullDown);
            green.SetDriveMode(GpioPinDriveMode.InputPullDown);
            yellow.SetDriveMode(GpioPinDriveMode.InputPullDown);

            this.state = new Dictionary<string, GpioPin>
            {
                { "red", red },
                { "yellow", yellow },
                { "green", green }
            };
        }

        private readonly IDictionary<string, GpioPin> state;

        public bool this[string key]
        {
            get => this.state[key].Read() == GpioPinValue.High;

            set => this.state[key].Write(value ? GpioPinValue.High : GpioPinValue.Low);
        }

        public LightsStatus ToStatus()
        {
            return new LightsStatus
            {
                Red = this.state["red"].Read() == GpioPinValue.High,
                Yellow = this.state["yellow"].Read() == GpioPinValue.High,
                Green = this.state["green"].Read() == GpioPinValue.High
            };
        }

        public void Apply()
        {

        }
    }
}
