using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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
        private readonly ILoginService _loginService;
        public ClienteController(IClienteService clienteService, ILoginService loginService)
        {
            _clienteService = clienteService;
            _loginService = loginService;
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
            try
            {
                LoginApiModel usuarioLogado = JsonConvert.DeserializeObject<LoginApiModel>(HttpContext.Session.GetString("SessionUser"));

                if(usuarioLogado is null)
                    return BadRequest("Necessário realizar o login.");

                Result<ClienteApiModel> retorno = _clienteService.ObterCliente(usuarioLogado.Email, usuarioLogado.Senha);

                if (retorno.IsValid is false)
                    return BadRequest(retorno.Notifications);

                if (retorno.Object is null)
                    return NoContent();

                return Ok(retorno.Object);
            }
            catch (Exception e)
            {
                throw new Exception("Necessário realizar o login.");
            }
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
            try
            {
                Result<ClienteApiModel> retorno = _clienteService.CadastrarCliente(cliente);

                if (retorno.IsValid is false)
                    return BadRequest(retorno.Notifications);

                return Created("", retorno.Object);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
            try
            {
                Result retorno = _clienteService.ExcluirCliente(email);

                if (retorno.IsValid is false)
                    return BadRequest(retorno.Notifications);
                return NoContent();
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Rota para logar utilizando email e senha, para ter acesso as informações da conta.
        /// É necessário realizar o cadastro antes.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        [HttpGet]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status400BadRequest)]
        public IActionResult Login(string email, string senha)
        {
            try
            {
                Result<LoginApiModel> usuarioLogado = _loginService.Logar(email, senha);

                if (usuarioLogado.IsValid is false)
                    return BadRequest(usuarioLogado.Notifications);

                HttpContext.Session.SetString("SessionEmail", email);
                HttpContext.Session.SetString("SessionSenha", senha);
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(usuarioLogado.Object));

                return Ok($"Usuario {usuarioLogado.Object.Email} Logado com sucesso!");
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
