namespace TestProject1;

public class ChildClass: BaseClass
{
    public string Name { get; init; }
    
    public ChildClass(string id, string name) : base(id)
    {
        Name = name;
    }

    // protected bool Equals(ChildClass other)
    // {
    //     return Name == other.Name;
    // }
    //
    // public override bool Equals(object? obj)
    // {
    //     if (ReferenceEquals(null, obj)) return false;
    //     if (ReferenceEquals(this, obj)) return true;
    //     if (obj.GetType() != this.GetType()) return false;
    //     return Equals((ChildClass)obj);
    // }
    //
    // public override int GetHashCode()
    // {
    //     return Name.GetHashCode();
    // }
    
    // =====
    //public override bool Equals(object? obj)
    //{
    //    if (obj == null || GetType() != obj.GetType())
    //    {
    //        return false;
    //    }
    //    var other = (obj as ChildClass)!;
    //    return Name.Equals(other.Name)
    //        && JToken.DeepEquals(SomeObject, other.SomeObject);
    //}

    //public override int GetHashCode()
    //{
    //    return HashCode.Combine(Name, SomeObject);
    //}
}