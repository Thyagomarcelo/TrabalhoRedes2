using ApiLimiteAgua.Model;

namespace ApiLimiteAgua.Interfaces
{
    public interface IApiLimiteAguaService
    {
        string GetLimiteAguaDia();
        Task PostInfoLimiteAgua(DadosLimiteAguaModel dados);
        string GetLimiteAguaSemana();

    }
}