using System;
using System.Net.Sockets;
using Nancy.Hosting.Self;

namespace SimpleInventory
{
	public class Startup
	{
		public static void Main()
		{
			var url = new Uri("http://localhost:7000");
			using (var nancyHost = new NancyHost(url))
			{
				try
				{
					nancyHost.Start();
				}
				catch (SocketException e)
				{
					Console.WriteLine("Failed starting web service {0}: {1}", e.SocketErrorCode, e.Message);
					return;
				}
				catch (Exception ex)
				{
					Console.WriteLine("Failed starting service: {0}", ex);
				}

				Console.WriteLine("SimpleInventory listening at {0}", url);
				Console.WriteLine("Hit enter to exit");
				Console.ReadLine();
			}
		}
	}
}
