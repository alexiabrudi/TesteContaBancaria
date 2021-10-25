using TesteContaBancaria.Domain.ApiModel;

namespace TesteContaBancaria.Domain.Interface
{
    public interface IContaBancariaService
    {
        Result<ContaBancariaApiModel> Obter(string email, string senha);
        Result<ContaBancariaApiModel> Depositar(string email, string senha, double valor);
        Result<ContaBancariaApiModel> Sacar(string email, string senha, double valor);
    }
}
