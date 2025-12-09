using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersCustomers.Application.DTOs.Ordem;
using OrdersCustomers.Application.Interfaces;
using OrdersCustomers.Application.Mappers;

namespace OrdersCustomers.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrdemController : ApiBaseController
{
    private readonly IOrdemService _ordemService;

    public OrdemController(IServiceProvider serviceProvider, IOrdemService ordemService) : base(serviceProvider)
    {
        _ordemService = ordemService;
    }

    [HttpGet("{numeroOrdem}")]
    public async Task<IActionResult> ObterPorId(string numeroOrdem)
    {
        var ret = await _ordemService.ObterPorNumeroOrdem(numeroOrdem);

        return Response(ret.ToApiResponse());
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodos()
    {
        var ret = await _ordemService.ListarTodas();

        return ret is not null ? Response(ret) : NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] OrdemCreateDto request)
    {
        var ret = await _ordemService.Criar(request);

        return ret?.NumeroOrdem is not null ? CreateResponse(ret) : Response(null);
    }

    [HttpPut("Finalizar/{numeroOrdem}")]
    public async Task<IActionResult> Finalizar(string numeroOrdem)
    {
        var ret = await _ordemService.Finalizar(numeroOrdem);

        return Response(ret);
    }
}