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
			byte[] encriptado = Encriptar(Encoding.UTF8.GetBytes(mensaje));
			byte[] desencriptado = Desencriptar(encriptado);
			Console.WriteLine("Mensaje encriptado:\n\t" + BitConverter.ToString(encriptado).Replace("-", "") + "\n");
			Console.WriteLine("Mensaje desencriptado:\n\t" + Encoding.UTF8.GetString(desencriptado));
			Console.WriteLine("Presione cualquier tecla para terminar");
			Console.ReadKey();
		}


		static byte[] Encriptar(byte[] mensaje)
		{
			byte[] encriptado;
			using (var rsa = new RSACryptoServiceProvider(2048))
			{
				rsa.ImportParameters(llavePublica);
				encriptado = rsa.Encrypt(mensaje, true);
			}
			return encriptado;
		}

		static void crearLlaves()
		{
			using (var rsa = new RSACryptoServiceProvider(2048))
			{
				llavePrivada = rsa.ExportParameters(true);
				llavePublica = rsa.ExportParameters(false);
			}
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



	}
}
