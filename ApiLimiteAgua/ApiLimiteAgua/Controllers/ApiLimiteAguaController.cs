using ApiLimiteAgua.Interfaces;
using ApiLimiteAgua.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiLimiteAgua.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApiLimiteAguaController : ControllerBase
    {

        private readonly IApiLimiteAguaService _ApiLimiteAguaService;

        public ApiLimiteAguaController(IApiLimiteAguaService ApiLimiteAguaService)
        {
            _ApiLimiteAguaService = ApiLimiteAguaService;
        }

        [HttpPost(Name = "PostInfoLimiteAgua")]
        public JsonResult PostInfoLimiteAgua(DadosLimiteAguaModel dados)
        {

            _ApiLimiteAguaService.PostInfoLimiteAgua(dados);

            return new JsonResult(new
            {
                erroMsg = "",
                Data = HttpStatusCode.OK
            });
        }

        [HttpGet(Name = "GetLimiteAguaDia")]
        public JsonResult GetLimiteAguaDia()
        {
            JsonResult teste = new JsonResult(new
            {
                Erro = "",
                Data = _ApiLimiteAguaService.GetLimiteAguaDia()
            });

            return teste;
        }

        [HttpGet(Name = "GetLimiteAguaSemana")]
        public JsonResult GetLimiteAguaSemana()
        {
            JsonResult teste = new JsonResult(new
            {
                Erro = "",
                Data = _ApiLimiteAguaService.GetLimiteAguaSemana()
            });

            return teste;
        }
    }
}
