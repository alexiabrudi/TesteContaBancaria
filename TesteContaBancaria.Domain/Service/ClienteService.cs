using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TesteContaBancaria.ApiModel;
using TesteContaBancaria.Domain.Entidade;
using TesteContaBancaria.Domain.Interface;
using TesteContaBancaria.Domain.Util;

namespace TesteContaBancaria.Domain.Service
{
    public class ClienteService : IClienteService
    {
        public Result<ClienteApiModel> CadastrarCliente(ClienteApiModel clientes)
        {
            List<Cliente> listaClientes = ClientesUtil.ListaCliente;

            int ultimoIdConta = 0;

            if (listaClientes.Any())
                ultimoIdConta = listaClientes.Max(x => x.ContaBancaria.IdConta);

            Cliente cliente = new Cliente(clientes.Nome, clientes.Email, clientes.Senha, new ContaBancaria(ultimoIdConta + 1));

            if (cliente.IsValid is false)
                return Result<ClienteApiModel>.Error(cliente.Notifications);

            if (listaClientes.Any(x => x.Email == clientes.Email))
                return Result<ClienteApiModel>.Error(new List<Notification> { new Notification(nameof(CadastrarCliente), Constantes.MENSAGEM_EMAIL_CADASTRADO) });

            listaClientes.Add(cliente);

            return Result<ClienteApiModel>.Ok(clientes);


        }
        public Result<ClienteApiModel> ObterCliente(string email, string senha)
        {
            if (string.IsNullOrEmpty(email))
                return Result<ClienteApiModel>.Error(new Notification(nameof(ObterCliente), Constantes.MENSAGEM_EMAIL_NECESSARIO));
            if (string.IsNullOrEmpty(senha))
                return Result<ClienteApiModel>.Error(new Notification(nameof(ObterCliente), Constantes.MENSAGEM_SENHA_NECESSARIA));

            var cliente = ClientesUtil.FiltrarLista(email, senha);

            if (cliente == null)
                return null;

            return Result<ClienteApiModel>.Ok(new ClienteApiModel()
            {
                Email = cliente.Email,
                Nome = cliente.Nome,
                Senha = cliente.Senha
            });
        }
        public Result ExcluirCliente(string email)
        {
            if (string.IsNullOrEmpty(email))
                return Result.Error(new Notification(nameof(ExcluirCliente), Constantes.MENSAGEM_EMAIL_NECESSARIO_EXCLUSAO));
            List<Cliente> listaClientes = ClientesUtil.ListaCliente;
            listaClientes.RemoveAll(x => x.Email == email);
            return Result.Ok();
        }

        
    }
}
