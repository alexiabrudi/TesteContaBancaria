using Flunt.Notifications;
using Flunt.Validations;
using System;
using TesteContaBancaria.Domain.Util;

namespace TesteContaBancaria.Domain.Entidade
{
    public class ContaBancaria : Notifiable<Notification>
    {
        public int IdConta { get; private set; }
        public double Saldo { get; private set; }
        public DateTime DataAbertura { get; private set; }

        public ContaBancaria(int idConta)
        {
            IdConta = idConta;
            Saldo = 0.0;
            DataAbertura = DateTime.Now;

            AddNotifications(new Contract<ContaBancaria>()
                .Requires()
                .IsGreaterThan(IdConta, 0, nameof(idConta), Constantes.MENSAGEM_ID_CONTA_MAIOR_ZERO)
                .IsGreaterOrEqualsThan(Saldo, 0, nameof(idConta), Constantes.MENSAGEM_SALDO_MAIOR_ZERO));
        }

        public void Depositar(double valor)
        {
            Saldo += valor;
        }
        public void Sacar(double valor)
        {
            Saldo -= valor;
        }
        public string DataAberturaConta(DateTime dataAbertura)
        {
            return DataAbertura.ToString("d");
        }
    }
}
