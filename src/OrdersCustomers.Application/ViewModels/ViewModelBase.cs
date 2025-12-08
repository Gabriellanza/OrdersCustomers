namespace OrdersCustomers.Application.ViewModels;

public class ViewModelBase : IComparable
{
    public int CompareTo(object obj)
    {
        return (this == obj) ? 1 : 0;
    }
}