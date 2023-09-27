using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestProject1;

public class ChildClassWithJson : ChildClass
{
    public JObject JsonObject { get; init; }
    // public ComplexType Stuff { get; init; }

    // public static readonly ComplexType Default = new ();

    internal ChildClassWithJson(string id, string name, JObject jsonObject) : base(id, name)
    {
        JsonObject = jsonObject;
        // Stuff = complexType ?? Default;
    }

    protected bool Equals(ChildClassWithJson other)
    {
        return JToken.DeepEquals(this.JsonObject, other.JsonObject); //JsonObject.Equals(other.JsonObject); // J ////  //
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ChildClassWithJson)obj);
    }
    
    public override int GetHashCode()
    {
        return JsonObject.GetHashCode();
    }

    public class ComplexType
    {
        
    }
}