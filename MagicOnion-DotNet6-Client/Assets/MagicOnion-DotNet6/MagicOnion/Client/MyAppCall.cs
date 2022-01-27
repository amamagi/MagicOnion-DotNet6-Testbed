using System.Threading.Tasks;
using Grpc.Core;
using MagicOnion.Client;
using MagicOnionDotNet6.Shared;
using UnityEngine;

namespace MagicOnion_DotNet6.MagicOnion.Client
{
    public class MyAppCall : MonoBehaviour
    {
        private void Start()
        {
            CallTest();
        }

        private async Task CallTest()
        {
            var host = "localhost";
            var port = 5001;
            var channel = new Channel(host, port, ChannelCredentials.Insecure);
            var client = MagicOnionClient.Create<IMyFirstService>(channel);
            var result = await client.SumAsync(10, 4);
            Debug.Log($"Result: {result}");
        }
    }
}