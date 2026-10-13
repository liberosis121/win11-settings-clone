namespace SettingsClone.Models
{
    // A snapshot passed from Home to System through Frame.Navigate.
    public sealed class DeviceInfo
    {
        public DeviceInfo(string name, string model, bool bluetoothOn)
        {
            Name = name;
            Model = model;
            BluetoothOn = bluetoothOn;
        }

        public string Name { get; }
        public string Model { get; }
        public bool BluetoothOn { get; }
    }
}
