namespace TestProject1;

public class BaseClass
{
    public string Id { get; init; }

    internal BaseClass(string id) => Id = id;
}

public class MainClass
{
    public BaseClass Base { get; init; }

    public MainClass(BaseClass baseClass)
    {
        Base = baseClass;
    }
}