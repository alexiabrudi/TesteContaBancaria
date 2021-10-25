using TesteContaBancaria.Domain.ApiModel;
using TesteContaBancaria.Domain.Service;
using TesteContaBancaria.Domain.Util;
using Xunit;

namespace TesteContaBancaria.Test.Service
{
    public class ContaBancariaServiceTest
    {
        [Fact]
        public void DepositarContaBancaria_Sucesso()
        {
            var clienteModel = new ClienteApiModel()
            {
                Nome = "Teste1",
                Email = "teste1@gmail.com",
                Senha = "abc123"

            };
            var cliente = new ClienteService();

            var retorno = cliente.CadastrarCliente(clienteModel);

            var contaBancaria = new ContaBancariaService();

            var retornoContaBancaria = contaBancaria.Depositar(clienteModel.Email, clienteModel.Senha, 50);

            Assert.True(retornoContaBancaria.IsValid);
            Assert.True(retornoContaBancaria.Object.Saldo == "R$ 50,00");
            retorno.Clear();
            retornoContaBancaria.Clear();
        }
        [Fact]
        public void SacarContaBancaria_Sucesso()
        {
            var clienteModel = new ClienteApiModel()
            {
                Nome = "Teste1",
                Email = "teste1@gmail.com",
                Senha = "abc123"

            };
            var cliente = new ClienteService();

            var retorno = cliente.CadastrarCliente(clienteModel);

            var contaBancaria = new ContaBancariaService();
            var retornoContaBancaria = contaBancaria.Depositar(clienteModel.Email, clienteModel.Senha, 50);
            Assert.True(retornoContaBancaria.IsValid);
            Assert.True(retornoContaBancaria.Object.Saldo == "R$ 50,00");
            var retornoContaBancaria2 = contaBancaria.Sacar(clienteModel.Email, clienteModel.Senha, 40);
            Assert.True(retornoContaBancaria2.Object.Saldo == "R$ 10,00");
            retorno.Clear();
            retornoContaBancaria.Clear();
        }

        [Fact]
        public void SacarContaBancaria_SaldoInsuficiente_Erro()
        {
            var clienteModel = new ClienteApiModel()
            {
                Nome = "Teste1",
                Email = "teste1@gmail.com",
                Senha = "abc123"

            };
            var cliente = new ClienteService();

            var retorno = cliente.CadastrarCliente(clienteModel);

            var contaBancaria = new ContaBancariaService();
            var retornoContaBancaria = contaBancaria.Depositar(clienteModel.Email, clienteModel.Senha, 50);
            Assert.True(retornoContaBancaria.IsValid);
            Assert.True(retornoContaBancaria.Object.Saldo == "R$ 50,00");
            var retornoContaBancaria2 = contaBancaria.Sacar(clienteModel.Email, clienteModel.Senha, 60);
            Assert.Contains(retornoContaBancaria2.Notifications, x => x.Message == Constantes.MENSAGEM_SALDO_INFUSICIENTE);
            retorno.Clear();
            retornoContaBancaria.Clear();
        }
    }
}
