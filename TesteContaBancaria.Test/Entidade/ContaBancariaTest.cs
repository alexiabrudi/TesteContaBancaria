using TesteContaBancaria.Domain.Entidade;
using TesteContaBancaria.Domain.Util;
using Xunit;

namespace TesteContaBancaria.Test.Entidade
{
    public class ContaBancariaTest
    {
        [Fact]
        public void CriarContaBancaria_Valida()
        {
            var contaBancaria = new ContaBancaria(1);
            Assert.True(contaBancaria.IsValid);
        }
        [Fact]
        public void CriarContaBancaria_IdInvalido_Erro()
        {
            var contaBancaria = new ContaBancaria(-1);
            Assert.True(contaBancaria.IsValid is false);
            Assert.Contains(contaBancaria.Notifications, x => x.Message == Constantes.MENSAGEM_ID_CONTA_MAIOR_ZERO);
        }
        [Fact]
        public void CriarContaBancaria_DepositarValor_Sucesso()
        {
            var contaBancaria = new ContaBancaria(1);

            contaBancaria.Depositar(50);

            Assert.True(contaBancaria.Saldo == 50);
        }
        [Fact]
        public void CriarCOntaBancaria_SacarValor_Sucesso()
        {
            var contaBancaria = new ContaBancaria(1);

            contaBancaria.Depositar(50);
            Assert.True(contaBancaria.Saldo == 50);

            contaBancaria.Sacar(40);
            Assert.True(contaBancaria.Saldo == 10);
        }

    }
}
