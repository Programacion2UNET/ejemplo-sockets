#!/usr/bin/python3
from Cliente import Cliente
from Servidor import Servidor

resp = input("¿Desea ser servidor? (s/n): ")

if resp.lower().startswith('s') :
	servidor = Servidor()
	servidor.start()
	servidor.join()
else:
	ip = input("Escriba la IP a conectarse: ")
	cliente = Cliente(ip)
	cliente.start()
	cliente.join()