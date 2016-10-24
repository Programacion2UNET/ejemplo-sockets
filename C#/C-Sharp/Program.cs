using System;

namespace SocketEjemplo
{
	class MainClass
	{
		public static void IniciarCliente()
		{
			Console.WriteLine("Escriba la IP a conectarse");
			String ip = Console.ReadLine();

			Cliente cliente = new Cliente(ip);
			cliente.Run();
		}

		public static void Main (string[] args)
		{
			Console.WriteLine("¿Desea ser servidor?");
			String resp = Console.ReadLine();

			if (resp.ToLower().StartsWith("s")) {
				Servidor miServidor = new Servidor();
				miServidor.Run();
			} else {
				IniciarCliente();
			}
		}
	}
}
