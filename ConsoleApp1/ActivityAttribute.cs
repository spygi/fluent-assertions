namespace ConsoleApp1
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ActivityAttribute : Attribute
    {
        public string Name { get; }

        public ActivityAttribute(string name) => Name = name;
    }
}
