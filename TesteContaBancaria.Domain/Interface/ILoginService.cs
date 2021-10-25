using TesteContaBancaria.Domain.ApiModel;

namespace TesteContaBancaria.Domain.Interface
{
    public interface ILoginService
    {
        Result<LoginApiModel> Logar(string email, string senha);
    }
}
