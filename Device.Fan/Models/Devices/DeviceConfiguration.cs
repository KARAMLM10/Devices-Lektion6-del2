namespace Device.Fan.Models.Devices;

public class DeviceConfiguration
{
        private readonly string _connectionstring;

    public DeviceConfiguration(string connectionstring)
    {
        _connectionstring = connectionstring;
    }


    public string DeviceID => _connectionstring.Split(";")[1].Split("=")[1]; 
    public string ConnectionString => _connectionstring;
    public bool Allowsending { get; set; } = false;
    public int TelemetryInterval { get; set; } = 10000;

}
