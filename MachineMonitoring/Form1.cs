using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachineMonitoring
{
    public partial class Form1 : Form
    {
        List<Machine> machines;

        public Form1()
        {
            InitializeComponent();
            machines = new List<Machine>();
            machines.Add(new Machine("MACH-A3", 122, 77));
            machines.Add(new Machine("MACH-BB", 143, 69));
        }

        private void btAddMachine_Click(object sender, EventArgs e)
        {
            try
            {
                string Name = tbMachineName.Text;
                bool nameExist = NameExist(Name);
                if (nameExist == false)
                {
                    int Temp = Convert.ToInt16(tbTemp.Text);
                    int Pressure = Convert.ToInt16(tbPressure);
                    Machine newMachine = new Machine(Name, Temp, Pressure);
                    machines.Add(newMachine);
                }
                else MessageBox.Show("Machine named " + Name + " already exist.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool NameExist(string _name)
        {
            List<string> MachineNames = new List<string>();
            foreach (Machine m in machines)
            {
                MachineNames.Add(m.Name);
            }
            if (MachineNames.Contains(_name))
                return true;
            else return false;
        }
        private Machine GetMachine(string _name)
        {
            Machine m = null;
            m = machines.SingleOrDefault(s => s.Name == _name);
            return m;
        }
        private void btShowAll_Click(object sender, EventArgs e)
        {
            lbInfo.Items.Clear();
            foreach (Machine m in machines)
            {
                lbInfo.Items.Add(m.ToString());
            }
        }

        private void btSimulate_Click(object sender, EventArgs e)
        {
            try
            {
                string Name = tbMachineName.Text;
                bool nameExist = NameExist(Name);
                if (nameExist == true)
                {
                    int Temp = Convert.ToInt16(tbTemp.Text);
                    int Pressure = Convert.ToInt16(tbPressure.Text);
                    Machine m = GetMachine(Name);
                    m.OutOfRange();
                    m.SetPressure(Pressure);
                    m.SetTemperature(Temp);
                }
                else MessageBox.Show("No machine matches the name");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btLogging_Click(object sender, EventArgs e)
        {
            if (btLogging.Text == "start logging")
            {
                foreach (Machine m in machines)
                {
                    // m.OutOfRange();
                    m.CriticalTemp += new OutOfRangeHandler(TooHighTemp);
                    m.CriticalTemp += new OutOfRangeHandler(TooLowTemp);
                    m.CriticalPressure += new OutOfRangeHandler(TooHighPressure);
                    m.CriticalPressure += new OutOfRangeHandler(TooLowPressure);
                }
                btLogging.Text = "stop logging";
            }
            else
            {
                foreach (Machine m in machines)
                {
                    m.CriticalTemp -= new OutOfRangeHandler(TooHighTemp);
                    m.CriticalTemp -= new OutOfRangeHandler(TooLowTemp);
                    m.CriticalPressure -= new OutOfRangeHandler(TooHighPressure);
                    m.CriticalPressure -= new OutOfRangeHandler(TooLowPressure);
                }
                btLogging.Text = "start logging";
            }
        }

        private string s1 = "temperature is too high";
        private string s2 = "temperature is too low";
        private string s3 = "pressure is too high";
        private string s4 = "pressure is too low";
        private void TooHighTemp(Machine m)
        {
            lbWarnings.Items.Add("Machine " + m.Name + " PROBLEM: " + s1);
        }
        private void TooLowTemp(Machine m)
        {
            lbWarnings.Items.Add("Machine " + m.Name + " PROBLEM: " + s2);
        }
        private void TooHighPressure(Machine m)
        {
            lbWarnings.Items.Add("Machine " + m.Name + " PROBLEM: " + s3);
        }
        private void TooLowPressure(Machine m)
        {
            lbWarnings.Items.Add("Machine " + m.Name + " PROBLEM: " + s4);
        }


    }
}
