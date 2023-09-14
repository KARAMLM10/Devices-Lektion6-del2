using Device.Fan.Models.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Device.Fan.Services;

public class DeviceManager
{

    private readonly DeviceConfiguration _config;
    private readonly DeviceClient _client;


    public DeviceManager(DeviceConfiguration config)
    {
        _config = config;
        _client = DeviceClient.CreateFromConnectionString(_config.ConnectionString);

        Task.FromResult(StartAsync());
    }

    public bool AllowSending() => _config.Allowsending;

    private async Task StartAsync()
    {
        await _client.SetMethodDefaultHandlerAsync(DirectMethodDefaultCallback, null);
    }

    private async Task<MethodResponse> DirectMethodDefaultCallback(MethodRequest req , object userContext) 
    {
        var res = new DirectMethodDataResponse { Messege = $"Excuted method : {req.Name} successfuly." };



        switch (req.Name.ToLower())
        {
            case "start":
                _config.Allowsending = true;
                break;

            case "stop":
                _config.Allowsending = false;
                break;

                default:
                    res.Messege = $"Direct Method {req.Name} Not Found"; 
                    return new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res)), 404);           

        }
        
        return new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res)), 200);


    }
}
