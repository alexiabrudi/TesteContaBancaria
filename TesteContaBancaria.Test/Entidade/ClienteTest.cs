using TesteContaBancaria.Domain.Entidade;
using TesteContaBancaria.Domain.Util;
using Xunit;

namespace TesteContaBancaria.Test.Entidade
{
    public class ClienteTest
    {
        [Fact]
        public void CriarCliente_Sucesso()
        {
            var cliente = new Cliente("Alexandre", "alexiabrudi@gmail.com", "abc123", new ContaBancaria(1));
            Assert.True(cliente.IsValid);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CriarCliente_NomeInvalido_Erro(string nome)
        {
            var cliente = new Cliente(nome, "alexiabrudi@gmail.com", "abc123", new ContaBancaria(1));
            Assert.False(cliente.IsValid);
            Assert.Contains(cliente.Notifications, x => x.Message == Constantes.MENSAGEM_NOME_INVALIDO);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("abcdhshs")]
        public void CriarCliente_EmailInvalido_Erro(string email)
        {
            var cliente = new Cliente("Alexandre", email, "abc123", new ContaBancaria(1));
            Assert.False(cliente.IsValid);
            Assert.Contains(cliente.Notifications, x => x.Message == Constantes.MENSAGEM_EMAIL_INVALIDO);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CriarCliente_SenhaInvalida_Erro(string senha)
        {
            var cliente = new Cliente("Alexandre", "alexiabrudi@gmail.com", senha, new ContaBancaria(1));
            Assert.False(cliente.IsValid);
            Assert.Contains(cliente.Notifications, x => x.Message == Constantes.MENSAGEM_SENHA_INVALIDA);
        }
    }
}
