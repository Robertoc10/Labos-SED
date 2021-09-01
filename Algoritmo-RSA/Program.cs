using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo_RSA
{
	class Program
	{

		private static RSAParameters llavePrivada;
		private static RSAParameters llavePublica;

		static void Main(string[] args)
		{    
			string mensaje = "";
			Console.WriteLine("Ingrese el mensaje a encriptar");
			mensaje = Console.ReadLine();
			crearLlaves();
			byte[] encriptado = Encriptar(Encoding.UTF8.GetBytes(mensaje));//asignamos el formato UTF8 para la codificaion de la frase
			byte[] desencriptado = Desencriptar(encriptado);
			Console.WriteLine("Mensaje encriptado:\n" + BitConverter.ToString(encriptado).Replace("-", ""));
			Console.WriteLine("Mensaje desencriptado:\n" + Encoding.UTF8.GetString(desencriptado));
			Console.WriteLine("Presione cualquier tecla para terminar");
			Console.ReadKey();
		}


		static byte[] Encriptar(byte[] mensaje)
		{
			byte[] encriptado;
			using (var rsa = new RSACryptoServiceProvider(2048))//creacion de un objeto RSA y asignandole tamaño de las llaves
			{
				rsa.ImportParameters(llavePublica);
				encriptado = rsa.Encrypt(mensaje, true);
			}
			return encriptado;
		}


		static byte[] Desencriptar(byte[] mensaje)
		{
			byte[] desencriptado;
			using (var rsa = new RSACryptoServiceProvider(2048))
			{
				rsa.ImportParameters(llavePrivada);
				desencriptado = rsa.Decrypt(mensaje, true);
			}
			return desencriptado;
		}

		static void crearLlaves()
		{
			using (var rsa = new RSACryptoServiceProvider(2048))
			{
				llavePrivada = rsa.ExportParameters(true);
				llavePublica = rsa.ExportParameters(false);
			}
		}



	}
}
