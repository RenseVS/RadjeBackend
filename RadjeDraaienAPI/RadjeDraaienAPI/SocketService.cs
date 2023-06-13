using System;
using SocketIOClient;
using System.Diagnostics;

namespace RadjeDraaienAPI
{
	public class SocketService
	{
        SocketIO client;
		public SocketService()
		{
            client = new SocketIO("http://localhost:3000");
        }

        public async void SpinWheel(int id) {
            await client.ConnectAsync();
            await client.EmitAsync("send", id.ToString());
        }
	}
}

