using System.Net;
using Serilog;
using VRChatOSCLib;

namespace Phoenix.OBSVRCMuteSync;

public class OSCClient
{
    public static VRChatOSC Client { get; private set; }
    public static ILogger Logger = Log.ForContext<OSCClient>();
    public static bool isMuted = false;
    public static void Initialize(IPAddress ip, int port)
    {
        Client = new VRChatOSC(ip, port);
        Client.OnMessage += OnMessage;
        Client.Listen(IPAddress.Loopback,OSCQServer.LocalServer.OscReceivePort);
        Client.Connect(IPAddress.Loopback, port);
        
        Logger.Information("OSC Client Initialized for send:  {IP}:{Port}", ip, port);
        Task.Delay(-1).GetAwaiter().GetResult();
    }

    private static void OnMessage(object? sender, VRCMessage e)
    {
        string address = e.Address;
        var type = e.Type;
        switch(type)
        {
            case VRCMessage.MessageType.DefaultParameter:
                if (address.Equals("/avatar/parameters/MuteSelf"))
                {
                    isMuted = e.GetValue<bool>();
                    OBSWebsocketClient.OBSWebSocket.InputsRequests.ToggleInputMuteAsync(ConfigManager.Config.OBSMicName);
                }

                break;
        }
    }
}