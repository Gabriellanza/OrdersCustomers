namespace OrdersCustomers.Domain.Entities.Comum;

public class Notification
{
    private Notification()
    {
        Id = Guid.NewGuid();
        Date = DateTime.Now;
    }

    public Notification(string key, string value, NotificationType type) : this()
    {
        Key = key;
        Value = value;
        Type = type;
    }

    public Guid Id { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }

    public DateTime Date { get; set; }

    public NotificationType Type { get; set; }

    public override string ToString()
    {
        var strNotification = "";

        strNotification += "Data: " + Date.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine;
        strNotification += "Key: " + Key + Environment.NewLine;
        strNotification += "Message: " + Value + Environment.NewLine;
        strNotification += "".PadLeft(40, '=') + Environment.NewLine;

        return strNotification;
    }
}