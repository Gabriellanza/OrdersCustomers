using Microsoft.AspNetCore.Mvc;
using OrdersCustomers.Domain.Interfaces;

namespace OrdersCustomers.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ApiBaseController
{
    private readonly IClienteService _clienteService;

    public ClienteController(IServiceProvider serviceProvider, IClienteService clienteService) : base(serviceProvider)
    {
        _clienteService = clienteService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var ret = await _clienteService.Listar();

        return Ok(ret);
    }

    

}