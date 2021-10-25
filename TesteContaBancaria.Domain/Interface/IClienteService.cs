using TesteContaBancaria.Domain.ApiModel;

namespace TesteContaBancaria.Domain.Interface
{
    public interface IClienteService
    {
        Result<ClienteApiModel> CadastrarCliente(ClienteApiModel clientes);
        Result ExcluirCliente(string email);
        Result<ClienteApiModel> ObterCliente(string email, string senha);
    }
}
