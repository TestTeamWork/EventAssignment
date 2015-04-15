using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineMonitoring
{
    public delegate void OutOfRangeHandler(Machine sender);
    public class Machine
    {
        public event OutOfRangeHandler CriticalTemp;
        public event OutOfRangeHandler CriticalPressure;

        protected virtual void OnCriticalTemp()
        {
            if (CriticalTemp != null) CriticalTemp(this);
        }

        protected virtual void OnCriticalPressure()
        {
            if (CriticalPressure != null) CriticalPressure(this);
        }

        public void OutOfRange()
        {
            if (this.temperature > 120 || this.temperature < 80) OnCriticalTemp();
            if (this.pressure > 80 || this.pressure < 60) OnCriticalPressure();
        }

        private int temperature;
        private int pressure;
        public String Name { get; set; }

        public Machine(String name, int _temperature, int _pressure)
        {
            this.Name = name;
            this.pressure = _pressure;
            this.temperature = _temperature;
        }

        public void SetTemperature(int newTemperature)
        {
            this.temperature = newTemperature;
        }
        public void SetPressure(int newPressure)
        {
            this.pressure = newPressure;
        }
        public override string ToString()
        {
            return "Name " + this.Name
                + "\tCurrent temp " + this.temperature
                + "\tCurrent pressure " + this.pressure;
        }
    }
}
