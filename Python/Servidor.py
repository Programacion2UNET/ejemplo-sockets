import socket
import sys
import threading

# Clase utilizada para ser ejecutada en hilos
class Servidor (threading.Thread):
	def __init__(self):
		threading.Thread.__init__(self)
		self.threadID = 'Server' 	#Identificador del hilo
		self.name     = 'Server' 	#Nombre del hilo
		self.sock     = socket.socket(socket.AF_INET, socket.SOCK_STREAM) #Socket de comunicacion
		self.port	  = 12345
		

	def run(self):
		self.sock.bind(('', self.port))
		self.sock.listen(1)

		while (True):
			print("Esperando al cliente en el puerto " + str(self.port) + "...")

			try:
				#Se acepta la conexión
				connection, client_address = self.sock.accept()

				print("Se conectó un cliente desde " + self.sock.getsockname()[0])

				while True:
					data = connection.recv(512)
					print(data)

					#Envía una cadena de texto al cliente para mostrar intercambio de información
					message = "Gracias por conectarte a " + socket.gethostbyname(socket.gethostname()) + "\nHasta luego."
					connection.sendall(message.encode())
			finally:
				#Se cierra la conexión
				connection.close()		
