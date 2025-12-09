using System.Globalization;
using LegacySimulator.INFO;

namespace LegacySimulator.DAL;

public class OrdemDal
{
    private readonly string _caminhoEntrada;
    private readonly string _caminhoSaida;

    public OrdemDal()
    {
        var pastaDados = Path.Combine(AppContext.BaseDirectory, "Dados");
        Directory.CreateDirectory(pastaDados);

        _caminhoEntrada = Path.Combine(pastaDados, "ordens_entrada.csv");
        _caminhoSaida = Path.Combine(pastaDados, "ordens_saida.csv");
    }

    public List<OrdemInfo> LerOrdens()
    {
        var ordens = new List<OrdemInfo>();

        if (!File.Exists(_caminhoEntrada))
            return ordens;

        var linhas = File.ReadAllLines(_caminhoEntrada);

        foreach (var linha in linhas)
        {
            var colunas = linha.Split(',');
            ordens.Add(new OrdemInfo(
                int.Parse(colunas[0]),
                colunas[1],
                decimal.Parse(colunas[2], CultureInfo.InvariantCulture)
            ));
        }

        return ordens;
    }

    public void SalvarOrdens(List<OrdemInfo> ordens)
    {
        Directory.CreateDirectory("Dados");

        using var writer = new StreamWriter(_caminhoSaida);
        writer.WriteLine("Id,NomeCliente,Valor");

        foreach (var o in ordens)
        {
            writer.WriteLine($"{o.Id},{o.NomeCliente},{o.Valor.ToString(CultureInfo.InvariantCulture)}");
        }
    }

    private static readonly List<OrdemInfo> Ordem = new();

    public void Salvar(OrdemInfo ordem) => Ordem.Add(ordem);

    public List<OrdemInfo> Listar() => Ordem;
}