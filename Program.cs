using System;
using System.Threading.Tasks;

namespace ConsoleOraculo
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostname = (args.Length > 0) ? args[0] : "localhost";

            Console.WriteLine("Conectando em " + hostname);

            var oraculo = new Oraculo(hostname);

            for(int i=1; ;i++)
            {
                string id = $"P{i}";
                string pergunta = "Qual a capital do Brasil?";
                string mensagem = oraculo.Pergunta(id, pergunta);

                Console.WriteLine(mensagem);
                Task.Delay(5000).Wait();

                var respostas = oraculo.ObterRespostas(id);
                foreach(var resp in respostas)
                {
                    Console.WriteLine($"{resp.Equipe}: {resp.Texto}");
                }

                Console.WriteLine();
            }
        }
    }
}
