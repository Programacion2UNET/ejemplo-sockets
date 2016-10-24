using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketEjemplo
{
	public class Cliente
	{
		protected Socket sender;
		protected Int32 port = 12345;
        protected String ip;
		IPEndPoint remoteEP;

		public Cliente (String host)
		{
            IPAddress ipAddress = IPAddress.Parse(host);
            this.ip= host;
            this.remoteEP = new IPEndPoint(ipAddress, this.port);
            this.sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
		}

		public void Run(){
			// Buffer
			byte[] bytes = new byte[1024];

			// Se inicia la conexión
			try {
				try {
                    Console.WriteLine("Conectandose a {0} en el puerto {1}", this.ip, this.port);
                    this.sender.Connect(this.remoteEP);

                    Console.WriteLine("Se ha logrado conectarse a " + this.ip);

                    //Se envia una cadena de texto
                    byte[] msg = Encoding.ASCII.GetBytes("Saludos desde " + ((IPEndPoint)sender.LocalEndPoint).Address.ToString() + "<EOF>");
					sender.Send(msg);

					//Se crea un objeto para leer la respuesta y se muestra por pantalla
					int bytesRec = sender.Receive(bytes);
					Console.WriteLine("El servidor dice {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Se cierra la conexión
					sender.Shutdown(SocketShutdown.Both);
					sender.Close();

				} catch (ArgumentNullException ane) {
					Console.WriteLine("Error de argumentos : {0}",ane.ToString());
				} catch (SocketException se) {
					Console.WriteLine("Excepción del socket : {0}",se.ToString());
				} catch (Exception e) {
					Console.WriteLine("Excepcion inesperada : {0}", e.ToString());
				}

			} catch (Exception e) {
				Console.WriteLine( e.ToString());
			}
		}
	}
}

