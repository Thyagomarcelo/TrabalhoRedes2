using ApiLimiteAgua.Controllers;
using ApiLimiteAgua.Interfaces;
using ApiLimiteAgua.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ApiLimiteAgua.Services
{
    public class ApiLimiteAguaService : IApiLimiteAguaService
    {
        ApiLimiteAguaRepository ConsultasLimiteAguaBD = new ApiLimiteAguaRepository();

        private readonly IDbUpdateService _DbUpdateService;

        public ApiLimiteAguaService(IDbUpdateService DbUpdateService)
        {
            _DbUpdateService = DbUpdateService;
        }

        public string GetLimiteAguaDia()
        {
            List<DadosLimiteAguaModel> dadosLimiteAgua = new List<DadosLimiteAguaModel>();

            dadosLimiteAgua = ConsultasLimiteAguaBD.getDadosBaseDia();

            string dadosLimiteAguaJson = JsonSerializer.Serialize(dadosLimiteAgua);

            return dadosLimiteAguaJson;
        }

        public string GetLimiteAguaSemana()
        {
            List<DadosLimiteAguaModel> dadosLimiteAgua = new List<DadosLimiteAguaModel>();

            dadosLimiteAgua = ConsultasLimiteAguaBD.getDadosBaseSemana();

            string dadosLimiteAguaJson = JsonSerializer.Serialize(dadosLimiteAgua);

            return dadosLimiteAguaJson;
        }

        public async Task PostInfoLimiteAgua(DadosLimiteAguaModel dados)
        {
            ConsultasLimiteAguaBD.IncluirRegistro(dados);

            await _DbUpdateService.NotificarAtualizacaoBancoDeDados(dados);
        }
    }
}
