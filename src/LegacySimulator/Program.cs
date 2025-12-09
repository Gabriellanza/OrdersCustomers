using LegacySimulator.BLL;

namespace LegacySimulator;

public class Program
{
    private static void Main()
    {
        Console.WriteLine("Fluxo legado iniciado");

        var ordemBll = new OrdemBll();
        ordemBll.ProcessarOrdens();

        Console.WriteLine("Fluxo legado com CSV concluído!");
        Console.ReadLine();
    }
}