using Flunt.Notifications;
using Flunt.Validations;
using TesteContaBancaria.Domain.Util;

namespace TesteContaBancaria.Domain.Entidade
{
    public class Cliente : Notifiable<Notification>
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public ContaBancaria ContaBancaria { get; private set; }

        public Cliente(string nome, string email, string senha, ContaBancaria contaBancaria)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            ContaBancaria = contaBancaria;

            AddNotifications(new Contract<Cliente>()
                .Requires()
                .IsEmail(email, nameof(email), Constantes.MENSAGEM_EMAIL_INVALIDO)
                .IsNotNullOrEmpty(nome, nameof(nome), Constantes.MENSAGEM_NOME_INVALIDO)
                .IsNotNullOrEmpty(senha, nameof(senha), Constantes.MENSAGEM_SENHA_INVALIDA)
            );
        }
    }
}
