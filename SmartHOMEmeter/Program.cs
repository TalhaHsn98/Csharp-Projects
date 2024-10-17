using System;
using System.Collections.Generic;
using System.Timers;

namespace SmartHomeSimulator
{
    // Base class for all smart devices
    abstract class SmartDevice
    {
        public string DeviceName { get; set; }
        public bool IsOn { get; set; }
        public double EnergyUsage { get; set; }  // Energy used per hour in watts

        public SmartDevice(string name, double energyUsage)
        {
            DeviceName = name;
            IsOn = false;
            EnergyUsage = energyUsage;
        }

        public virtual void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"{DeviceName} is turned on.");
        }

        public virtual void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"{DeviceName} is turned off.");
        }

        public abstract void ShowStatus();
    }

    // Derived class for Smart Light
    class SmartLight : SmartDevice
    {
        public SmartLight(string name) : base(name, 10) { } // Energy usage 10W per hour

        public override void ShowStatus()
        {
            Console.WriteLine($"Light {DeviceName} is {(IsOn ? "On" : "Off")}. Energy usage: {EnergyUsage}W");
        }
    }

    // Derived class for Smart Thermostat
    class SmartThermostat : SmartDevice
    {
        public double Temperature { get; set; }

        public SmartThermostat(string name, double initialTemp) : base(name, 50) // Energy usage 50W per hour
        {
            Temperature = initialTemp;
        }

        public void SetTemperature(double temperature)
        {
            Temperature = temperature;
            Console.WriteLine($"{DeviceName} temperature set to {Temperature}°C.");
        }

        public override void ShowStatus()
        {
            Console.WriteLine($"Thermostat {DeviceName} is {(IsOn ? "On" : "Off")}, Temp: {Temperature}°C, Energy usage: {EnergyUsage}W");
        }
    }

    // Derived class for Smart Door Lock
    class SmartDoorLock : SmartDevice
    {
        public bool IsLocked { get; set; }

        public SmartDoorLock(string name) : base(name, 5) // Energy usage 5W per hour
        {
            IsLocked = true;
        }

        public void LockDoor()
        {
            IsLocked = true;
            Console.WriteLine($"{DeviceName} is locked.");
        }

        public void UnlockDoor()
        {
            IsLocked = false;
            Console.WriteLine($"{DeviceName} is unlocked.");
        }

        public override void ShowStatus()
        {
            Console.WriteLine($"Door {DeviceName} is {(IsLocked ? "Locked" : "Unlocked")}. Energy usage: {EnergyUsage}W");
        }
    }

    class Program
    {
        static List<SmartDevice> devices = new List<SmartDevice>();

        static void Main(string[] args)
        {
            // Create devices
            devices.Add(new SmartLight("Living Room Light"));
            devices.Add(new SmartThermostat("Main Thermostat", 22.5));
            devices.Add(new SmartDoorLock("Front Door"));

            // Main loop to accept user commands
            string command = string.Empty;
            while (command != "exit")
            {
                Console.WriteLine("\nEnter a command (e.g., 'turn on light', 'set temp', 'lock door', 'show status', 'exit'):");
                command = Console.ReadLine().ToLower();
                ProcessCommand(command);
            }
        }

        static void ProcessCommand(string command)
        {
            string[] parts = command.Split(' ');
            if (parts.Length < 2) return;

            switch (parts[0])
            {
                case "turn":
                    HandleTurnCommand(parts);
                    break;
                case "set":
                    HandleSetCommand(parts);
                    break;
                case "lock":
                case "unlock":
                    HandleLockCommand(parts);
                    break;
                case "show":
                    ShowDeviceStatus();
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }

        static void HandleTurnCommand(string[] parts)
        {
            if (parts.Length < 3) return;

            string action = parts[1];
            string deviceName = parts[2];

            SmartDevice device = devices.Find(d => d.DeviceName.ToLower().Contains(deviceName));

            if (device != null)
            {
                if (action == "on")
                {
                    device.TurnOn();
                }
                else if (action == "off")
                {
                    device.TurnOff();
                }
            }
            else
            {
                Console.WriteLine("Device not found.");
            }
        }

        static void HandleSetCommand(string[] parts)
        {
            if (parts.Length < 3) return;

            string parameter = parts[1];
            if (parameter == "temp" && parts.Length == 4)
            {
                string deviceName = parts[2];
                double temperature;
                if (double.TryParse(parts[3], out temperature))
                {
                    SmartThermostat thermostat = (SmartThermostat)devices.Find(d => d.DeviceName.ToLower().Contains(deviceName) && d is SmartThermostat);
                    if (thermostat != null)
                    {
                        thermostat.SetTemperature(temperature);
                    }
                    else
                    {
                        Console.WriteLine("Thermostat not found.");
                    }
                }
            }
        }

        static void HandleLockCommand(string[] parts)
        {
            string deviceName = parts[1];
            SmartDoorLock door = (SmartDoorLock)devices.Find(d => d.DeviceName.ToLower().Contains(deviceName) && d is SmartDoorLock);
            if (door != null)
            {
                if (parts[0] == "lock")
                {
                    door.LockDoor();
                }
                else if (parts[0] == "unlock")
                {
                    door.UnlockDoor();
                }
            }
            else
            {
                Console.WriteLine("Door not found.");
            }
        }

        static void ShowDeviceStatus()
        {
            foreach (var device in devices)
            {
                device.ShowStatus();
            }
        }
    }
}
