import socket
import sys
import threading

class Cliente (threading.Thread):
	def __init__(self, host):
		threading.Thread.__init__(self)
		self.threadID = 'Cliente'
		self.name     = 'Cliente'
		self.sock     = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
		self.ip       = host
		self.port     = 12345

	def run(self):
		server_address = (self.ip, self.port)
		print('Conectandose a ' + self.ip + ' en el puerto ' + self.ip)
		self.sock.connect(server_address)

		print('Se ha logrado conectarse a ' + self.ip)

		try:
			#Se envia una cadena de texto
			message = 'Saludos desde ' + socket.gethostbyname(socket.gethostname())
			self.sock.sendall(message.encode())

			#Se crean variables para leer la respuesta y se muestra por pantalla
			amount_received = 0
			amount_expected = len(message)
		    
			while amount_received < amount_expected:
				data = self.sock.recv(512)
				amount_received += len(data)
				print('El servidor dice ' +  str(data))

		finally:
			self.sock.close()
