using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteContaBancaria.Domain.Util
{
    public static class Constantes
    {
        public static string MENSAGEM_ID_CONTA_MAIOR_ZERO = "O Id da conta deve ser maior que 0";
        public static string MENSAGEM_SALDO_MAIOR_ZERO = "O saldo não pode ser negativo";
        public static string MENSAGEM_NOME_INVALIDO = "Nome inválido";
        public static string MENSAGEM_EMAIL_INVALIDO = "Email inválido";
        public static string MENSAGEM_SENHA_INVALIDA = "Senha inválida";
        public static string MENSAGEM_EMAIL_CADASTRADO = "Email já cadastrado";
        public static string MENSAGEM_EMAIL_NECESSARIO = "Email Necessário";
        public static string MENSAGEM_SENHA_NECESSARIA = "Senha Necessária";
        public static string MENSAGEM_EMAIL_NECESSARIO_EXCLUSAO = "Email necessário para exclusão";
        public static string MENSAGEM_CLIENTE_NAO_ENCONTRADO = "Cliente não encontrado";
        public static string MENSAGEM_VALOR_POSITIVO = "Valor para saque/depósito deve ser maior que 0";
        public static string MENSAGEM_SALDO_INFUSICIENTE = "Saldo Insuficiente";
    }

    public static class Validacao { 

        public static void ValidarEmailESenha(string email, string senha, List<Notification> mensagensErros)
        {
            if (string.IsNullOrEmpty(email))
                mensagensErros.Add(new Notification(nameof(ValidarEmailESenha), Constantes.MENSAGEM_EMAIL_NECESSARIO));
            if (string.IsNullOrEmpty(senha))
                mensagensErros.Add(new Notification(nameof(ValidarEmailESenha), Constantes.MENSAGEM_SENHA_NECESSARIA));
        }
    }

    
}
