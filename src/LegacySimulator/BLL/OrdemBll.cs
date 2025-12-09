using LegacySimulator.DAL;
using LegacySimulator.INFO;

namespace LegacySimulator.BLL;

public class OrdemBll
{
    private readonly OrdemDal _dal = new();

    public void ProcessarOrdens()
    {
        var ordens = _dal.LerOrdens();

        foreach (var ordem in ordens)
        {
            if (ordem.Valor <= 0)
                Console.WriteLine($"Ordem {ordem.Id} inválida: valor <= 0");
            else
                Console.WriteLine($"Processando ordem {ordem.Id} de {ordem.NomeCliente}, valor {ordem.Valor}");
        }

        _dal.SalvarOrdens(ordens);
    }

    public List<OrdemInfo> ObterOrdems() => _dal.Listar();

}