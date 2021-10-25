using TesteContaBancaria.Domain.ApiModel;
using TesteContaBancaria.Domain.Service;
using TesteContaBancaria.Domain.Util;
using Xunit;

namespace TesteContaBancaria.Test.Service
{
    public class LoginServiceTest
    {
        [Fact]
        public void LogarSucesso()
        {
            var clienteModel = new ClienteApiModel()
            {
                Nome = "Teste1",
                Email = "teste1@gmail.com",
                Senha = "abc123"

            };
            var cliente = new ClienteService();

            var retorno = cliente.CadastrarCliente(clienteModel);

            var login = new LoginService();

            var retornoLogin = login.Logar(retorno.Object.Email, retorno.Object.Senha);

            Assert.True(retornoLogin.IsValid);
            Assert.True(clienteModel.Email == retornoLogin.Object.Email);
            retorno.Clear();
            retornoLogin.Clear();
        }
        [Fact]
        public void Logar_ClienteNaoCadastrado_Erro()
        {
            var login = new LoginService();

            var retornoLogin = login.Logar("teste@gmail.com", "abc123");

            Assert.False(retornoLogin.IsValid);
            Assert.Contains(retornoLogin.Notifications, x => x.Message == Constantes.MENSAGEM_CLIENTE_NAO_ENCONTRADO);
            retornoLogin.Clear();
        }
    }
}