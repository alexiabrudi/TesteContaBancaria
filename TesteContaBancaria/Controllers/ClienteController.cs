using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteContaBancaria.Domain;
using TesteContaBancaria.Domain.ApiModel;
using TesteContaBancaria.Domain.Interface;

namespace TesteContaBancaria.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        /// <summary>
        /// Rota para obter o cliente logado, retornando nome, email e a senha do usuario
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status400BadRequest)]
        public IActionResult Obter()
        {

            Result<ClienteApiModel> retorno = _clienteService.ObterCliente(usuarioLogado.Email, usuarioLogado.Senha);

            if (retorno.IsValid is false)
                return BadRequest(retorno.Notifications);

            if (retorno.Object is null)
                return NoContent();

            return Ok(retorno.Object);
        }
        /// <summary>
        /// Rota para cadastrar um cliente.
        /// É necessário informar: Nome, email e senha
        /// <param name="cliente"></param>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ClienteApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status400BadRequest)]
        public IActionResult Cadastrar(ClienteApiModel cliente)
        {
            var retorno = _clienteService.CadastrarCliente(cliente);

            if (retorno.IsValid is false)
                return BadRequest(retorno.Notifications);
            return Created("", retorno.Object);
        }

        /// <summary>
        /// Rota para excluir um cliente cadastrado. É necessário informar o email.
        /// </summary>
        /// <param name="email"></param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status400BadRequest)]
        public IActionResult Excluir(string email)
        {
            var retorno = _clienteService.ExcluirCliente(email);
            if (retorno.IsValid is false)
                return BadRequest(retorno.Notifications);
            return NoContent();
        }
    }
}
