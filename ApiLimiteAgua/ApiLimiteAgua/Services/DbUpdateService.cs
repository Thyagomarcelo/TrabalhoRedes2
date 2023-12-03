// Servicos/DbUpdateService.cs
using ApiLimiteAgua.Interfaces;
using ApiLimiteAgua.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

public class DbUpdateService: IDbUpdateService
{
    private readonly IHubContext<DbUpdateHub> _hubContext;

    public DbUpdateService(IHubContext<DbUpdateHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotificarAtualizacaoBancoDeDados(DadosLimiteAguaModel dados)
    {
        // Lógica para detectar alterações no banco de dados

        // Notificar clientes sobre a atualização
        await _hubContext.Clients.All.SendAsync("ReceiveDbUpdate", dados);
    }
}