using System;
using System.IO;
using TesteContaBancaria.Domain.ApiModel;
using TesteContaBancaria.Domain.Service;
using TesteContaBancaria.Domain.Util;
using Xunit;

namespace TesteContaBancaria.Test.Service
{
    public class ClienteServiceTest
    {
        [Fact]
        public void CadastrarCliente_Sucesso()
        {
            var clienteModel = new ClienteApiModel()
            {
                Nome = "Teste1",
                Email = "teste1@gmail.com",
                Senha = "abc123"

            };
            var cliente = new ClienteService();

            var retorno = cliente.CadastrarCliente(clienteModel);

            Assert.True(retorno.IsValid);
            Assert.True(retorno.Object.Email == clienteModel.Email);
            Assert.True(retorno.Object.Senha == clienteModel.Senha);
            Assert.True(retorno.Object.Nome == clienteModel.Nome);
            retorno.Clear();
        }
        [Fact]
        public void CadastrarCliente_UsuarioCadastrado_Erro()
        {
            var clienteModel = new ClienteApiModel()
            {
                Nome = "Teste2",
                Email = "teste2@gmail.com",
                Senha = "abc123"

            };
            var cliente = new ClienteService();

            var retorno = cliente.CadastrarCliente(clienteModel);

            Assert.True(retorno.IsValid);
            Assert.True(retorno.Object.Email == clienteModel.Email);

            var clienteModelRepetido = new ClienteApiModel()
            {
                Nome = "Teste2",
                Email = "teste2@gmail.com",
                Senha = "abc123"
            };

            var retorno2 = cliente.CadastrarCliente(clienteModelRepetido);
            Assert.False(retorno2.IsValid);
            Assert.Contains(retorno2.Notifications, x => x.Message == Constantes.MENSAGEM_EMAIL_CADASTRADO);
            retorno.Clear();
            retorno2.Clear();
        }

        [Fact]
        public void ExcluirCliente_Sucesso()
        {
            var clienteModel = new ClienteApiModel()
            {
                Nome = "Teste2",
                Email = "teste2@gmail.com",
                Senha = "abc123"

            };
            var cliente = new ClienteService();

            var retorno = cliente.CadastrarCliente(clienteModel);
            Assert.True(retorno.IsValid);
            Assert.True(retorno.Object.Email == clienteModel.Email);

            cliente.ExcluirCliente(clienteModel.Email);

            var retorno2 = cliente.ObterCliente(clienteModel.Nome, clienteModel.Senha);
            Assert.Null(retorno2);
            retorno.Clear();
            retorno2.Clear();
        }
        [Fact]
        public void ExcluirCliente_SemEmail_Erro()
        {
            var cliente = new ClienteService();

            var retorno = cliente.ExcluirCliente("");

            Assert.Contains(retorno.Notifications, x => x.Message == Constantes.MENSAGEM_EMAIL_NECESSARIO_EXCLUSAO);
            retorno.Clear();
        }
        [Fact]
        public void ObterCliente_Sucesso()
        {
            var clienteModel = new ClienteApiModel()
            {
                Nome = "Teste2",
                Email = "teste2@gmail.com",
                Senha = "abc123"

            };
            var cliente = new ClienteService();

            var retorno = cliente.CadastrarCliente(clienteModel);
            Assert.True(retorno.IsValid);
            Assert.True(retorno.Object.Email == clienteModel.Email);

            var retorno2 = cliente.ObterCliente(clienteModel.Email, clienteModel.Senha);
            Assert.True(retorno2.Object.Email == clienteModel.Email);
            retorno.Clear();
            retorno2.Clear();
        }
        [Fact]
        public void ObterCliente_Sucesso_SemRetorno()
        {
            var clienteModel = new ClienteApiModel()
            {
                Nome = "Teste2",
                Email = "teste2@gmail.com",
                Senha = "abc123"

            };
            var cliente = new ClienteService();


            var retorno = cliente.ObterCliente(clienteModel.Email, clienteModel.Senha);
            Assert.Null(retorno);
            retorno.Clear();
        }
    }
}
