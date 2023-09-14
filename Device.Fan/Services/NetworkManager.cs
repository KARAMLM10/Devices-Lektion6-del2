using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Device.Fan.Services;

public class NetworkManager
{
    public virtual async Task<string> CkeckConnectivityAsync(string ipaddress = "8.8.8.8")
    {
        var isConnected = await SendPingAsync(ipaddress);
        return isConnected ? "Connected" : "Disconnected";

    }

    private async Task<bool> SendPingAsync(string ipaddress)
    {
        try
        {
            using var ping = new Ping();
            var replay = await ping.SendPingAsync(ipaddress, 1000 , new byte[32], new());
            return replay.Status == IPStatus.Success;

        }
        catch { return false;}
    }
}
