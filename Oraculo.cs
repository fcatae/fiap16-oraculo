using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Redis;

namespace ConsoleOraculo
{
    class Oraculo
    {
        const string canal = "Perguntas";
        private readonly ISubscriber _pub;
        private readonly IDatabase _db;

        public Oraculo(string connectionString)
        {
            var redis = ConnectionMultiplexer.Connect(connectionString);
            this._pub = redis.GetSubscriber();
            this._db = redis.GetDatabase();
        }

        public string Pergunta(string id, string pergunta)
        {
            string mensagem = $"{id}: {pergunta}";
            _pub.Publish(canal, mensagem);

            return mensagem;
        }

        public Resposta[] ObterRespostas(string id)
        {
            return _db.HashGetAll(id)
                .Select(v => new Resposta
                {
                    Equipe = v.Name,
                    Texto = v.Value
                })
                .ToArray();
        }
    }
}
