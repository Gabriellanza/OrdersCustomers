using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersCustomers.Application.DTOs.Cliente;
using OrdersCustomers.Application.Interfaces;
using OrdersCustomers.Application.Mappers;

namespace OrdersCustomers.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ClienteController : ApiBaseController
{
    private readonly IClienteService _clienteService;

    public ClienteController(IServiceProvider serviceProvider, IClienteService clienteService) : base(serviceProvider)
    {
        _clienteService = clienteService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var ret = await _clienteService.ObterPorId(id);

        return Response(ret.ToApiResponse());
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodos()
    {
        var ret = await _clienteService.ListarTodos();

        return ret is not null ? Response(ret) : NoContent();
    }


    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ClienteCreateDto request)
    {
        var ret = await _clienteService.Criar(request);

        return ret?.Id is not null ? CreateResponse(ret) : Response(null);
    }

    [HttpPut]
    public async Task<IActionResult> Alterar([FromBody] ClienteAlterDto request)
    {
        var ret = await _clienteService.Alterar(request);

        return Response(ret);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Inativar(Guid id)
    {
        var ret = await _clienteService.Inativar(id);

        return Response(ret);
    }

}