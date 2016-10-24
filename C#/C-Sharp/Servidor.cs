using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketEjemplo
{
	public class Servidor
	{
		protected Socket serverSocket;
		protected Int32 port = 12345;

		public Servidor ()
		{
			this.serverSocket = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp );
		}

		public void Run() {
			//Datos del cliente
			string data = null;

			//Buffer
			byte[] bytes = new Byte[1024];

            // listen for incoming connections.
            try {
				serverSocket.Bind(new IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), this.port));
				serverSocket.Listen(10);

				// Start listening for connections.
				while (true) {
					data = null;

					Console.WriteLine("Esperando al cliente en el puerto " + this.port + "...");

					//Se acepta la conexión
					Socket handler = serverSocket.Accept();



					Console.WriteLine("Se conectó un cliente desde " + ((IPEndPoint)handler.RemoteEndPoint).Address.ToString() );

					//Recibe porción de datos y lo imprime en pantalla
					while (true) {
						bytes = new byte[1024];
						int bytesRec = handler.Receive(bytes);
						data += Encoding.ASCII.GetString(bytes,0,bytesRec);

						//Si se recibe el caracter de final se rompe la conexión
						if (data.IndexOf("<EOF>") > -1) { 
							break;
						}
					}
					Console.WriteLine( "{0}", data);

					//Envía una cadena de texto al cliente para mostrar intercambio de información
					byte[] msg = Encoding.ASCII.GetBytes("Gracias por conectarte a " + ((IPEndPoint)handler.LocalEndPoint).Address.ToString() 
						+ "\nHasta luego");

					handler.Send(msg);
					handler.Shutdown(SocketShutdown.Both);

					//Se cierra la conexión
					handler.Close();
				}

			} catch (Exception e) {
				Console.WriteLine(e.ToString());
			}

			Console.Read();
		}
	}
}

