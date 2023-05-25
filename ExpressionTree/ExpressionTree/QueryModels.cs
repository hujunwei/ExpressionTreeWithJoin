/// <summary>
/// The query models
/// </summary>

public class Query
{
    public string Name { get; set; }
    public List<Property> Properties { get; set; }
}

public class Property
{
    public string Name { get; set; }
    public string Type { get; set; }
    public List<Constraint> Constraints { get; set; }
}

public class Constraint
{
    public string Operator { get; set; }
    public object Value { get; set; }
    public string Unit { get; set; }
}