namespace LegacySimulator.INFO;

public class OrdemInfo
{
    public int Id { get; set; }

    public string NomeCliente { get; set; }

    public decimal Valor { get; set; }

    public OrdemInfo(int id, string nomeCliente, decimal valor)
    {
        Id = id;
        NomeCliente = nomeCliente;
        Valor = valor;
    }

    
}