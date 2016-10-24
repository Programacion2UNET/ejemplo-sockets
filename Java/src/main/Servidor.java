/*
 * The MIT License
 *
 * Copyright 2016 skatox.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
package main;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketTimeoutException;

/**
 *
 * @author skatox
 */
public class Servidor extends Thread {

    protected ServerSocket serverSocket;
    protected int port = 12345;

    public Servidor() throws IOException {
        serverSocket = new ServerSocket(this.port);
    }

    @Override
    public void run() {
        while (true) {
            try {
                System.out.println("Esperando al cliente en el puerto " + this.port + "...");
                
                //Se acepta la conexión
                Socket server = serverSocket.accept();

                System.out.println("Se conectó un cliente desde " + server.getRemoteSocketAddress());

                //Recibe porción de datos y lo imprime en pantalla
                DataInputStream in = new DataInputStream(server.getInputStream());
                System.out.println(in.readUTF());

                //Envía una cadena de texto al cliente para mostrar intercambio de información
                DataOutputStream out = new DataOutputStream(server.getOutputStream());
                out.writeUTF("Gracias por conectarte a " + server.getLocalSocketAddress()
                        + "\nHasta luego.");

                //Se cierra la conexión
                server.close();

            } catch (SocketTimeoutException s) {
                System.out.println("Se agotó el tiempo de espera");
                break;
            } catch (IOException e) {
                e.printStackTrace();
                break;
            }
        }
    }

}
