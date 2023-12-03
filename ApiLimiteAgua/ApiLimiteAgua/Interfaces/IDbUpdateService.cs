using ApiLimiteAgua.Model;

namespace ApiLimiteAgua.Interfaces
{
    public interface IDbUpdateService
    {
        public Task NotificarAtualizacaoBancoDeDados(DadosLimiteAguaModel dados);
    }
}