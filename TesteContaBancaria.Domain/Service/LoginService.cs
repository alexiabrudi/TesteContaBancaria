using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using TesteContaBancaria.Domain.ApiModel;
using TesteContaBancaria.Domain.Interface;
using TesteContaBancaria.Domain.Util;

namespace TesteContaBancaria.Domain.Service
{
    public class LoginService : ILoginService
    {
        public Result<LoginApiModel> Logar(string email, string senha)
        {
            if (string.IsNullOrEmpty(email))
                return Result<LoginApiModel>.Error(new Notification(nameof(Logar), Constantes.MENSAGEM_EMAIL_NECESSARIO));
            if (string.IsNullOrEmpty(senha))
                return Result<LoginApiModel>.Error(new Notification(nameof(Logar), Constantes.MENSAGEM_SENHA_NECESSARIA));

            var cliente = ClientesUtil.FiltrarLista(email, senha);

            if (cliente == null)
                return Result<LoginApiModel>.Error(new Notification(nameof(Logar), Constantes.MENSAGEM_CLIENTE_NAO_ENCONTRADO));

            return Result<LoginApiModel>.Ok(new LoginApiModel()
            {
                Email = cliente.Email,
                Senha = cliente.Senha
            });
        }
    }
}
