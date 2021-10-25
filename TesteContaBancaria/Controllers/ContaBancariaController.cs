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
    [Route("contas-bancarias")]
    public class ContaBancariaController : ControllerBase
    {
        private readonly IContaBancariaService _contaBancariaService;
        private readonly ILoginService _loginService;


        public ContaBancariaController(IContaBancariaService contaBancariaService, ILoginService loginService)
        {
            _contaBancariaService = contaBancariaService;
            _loginService = loginService;
        }


        /// <summary>
        /// Rota para obter dados da conta.
        /// É necessário realizar o login antes.
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

                if (usuarioLogado is null)
                    return BadRequest("Necessário realizar o login.");

                Result<ContaBancariaApiModel> retorno = _contaBancariaService.Obter(usuarioLogado.Email, usuarioLogado.Senha);

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
        /// Rota para depositar na conta.
        /// É necessário realizar o login antes.
        /// Não é possível depositar valor negativo
        /// </summary>
        /// <param name="valor"></param>
        [HttpPost]
        [Route("depositar")]
        [ProducesResponseType(typeof(ContaBancariaApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status400BadRequest)]
        public IActionResult Depositar(double valor)
        {
            try
            {
                LoginApiModel usuarioLogado = JsonConvert.DeserializeObject<LoginApiModel>(HttpContext.Session.GetString("SessionUser"));

                if (usuarioLogado is null)
                    return BadRequest("Necessário realizar o login.");

                Result<ContaBancariaApiModel> retorno = _contaBancariaService.Depositar(usuarioLogado.Email, usuarioLogado.Senha, valor);

                if (retorno.IsValid is false)
                    return BadRequest(retorno.Notifications);
                return Created("", retorno.Object);
            }
            catch (Exception e)
            {
                throw new Exception("Necessário realizar o login.");
            }
        }

        /// <summary>
        /// Rota para sacar da conta.
        /// É necessário realizar o login antes.
        /// Não é possível sacar menos do que possui na conta
        /// </summary>
        /// <param name="valor"></param>
        [HttpPost]
        [Route("sacar")]
        [ProducesResponseType(typeof(ContaBancariaApiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status400BadRequest)]
        public IActionResult Sacar(double valor)
        {
            try
            {
                LoginApiModel usuarioLogado = JsonConvert.DeserializeObject<LoginApiModel>(HttpContext.Session.GetString("SessionUser"));

                if (usuarioLogado is null)
                    return BadRequest("Necessário realizar o login.");

                Result<ContaBancariaApiModel> retorno = _contaBancariaService.Sacar(usuarioLogado.Email, usuarioLogado.Senha, valor);

                if (retorno.IsValid is false)
                    return BadRequest(retorno.Notifications);
                return Created("", retorno.Object);
            }

            catch (Exception e)
            {
                throw new Exception("Necessário realizar o login.");
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
