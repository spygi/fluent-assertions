using FluentAssertions;
using Newtonsoft.Json.Linq;

namespace TestProject1
{
    internal class MainClass
    {
        public string Id { get; init; }

        public List<BaseClass> ListOfStuff { get; init; }

        public MainClass(string id, List<BaseClass> listOfStuff)
        {
            Id = id;
            ListOfStuff = listOfStuff;
        }
    }

    internal class BaseClass
    {
        public string Id { get; init; }

        public BaseClass(string id) => Id = id;
    }

    internal class ChildClass : BaseClass
    {
        public string Name { get; init; }

        public JObject SomeObject { get; init; }

        public ChildClass(string id, string name, JObject someObject) : base(id)
        {
            Name = name;
            SomeObject = someObject;
        }

        // 4th ASSERT
        // ========================

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

    internal class AnotherChildClass : BaseClass
    {
        public string Description { get; init; }

        public string Type { get; init; }

        public AnotherChildClass(string id, string description, string type) : base(id)
        {
            Description = description;
            Type = type;
        }

        // 4th ASSERT
        // ========================

        //public override bool Equals(object? obj)
        //{
        //    if (obj == null || GetType() != obj.GetType())
        //    {
        //        return false;
        //    }
        //    var other = (obj as AnotherChildClass)!;
        //    return Description.Equals(other.Description)
        //        && Type.Equals(other.Type);
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(Description, Type);
        //}
    }

    internal class SomeObjectClass
    {
        public string Name { get; init; }

        public SomeObjectClass(string name) { Name = name; }
    }

    public class UnitTest1
    {
        public UnitTest1()
        {
            // Global configuration to achieve the same result as with 5th ASSERT just with
            // a simple "subject.Should().BeEquivalentTo(expected);"
            //AssertionOptions.AssertEquivalencyUsing(options => options
            //    .RespectingRuntimeTypes()
            //    .Using<JObject>(o => o.Subject.Should().BeEquivalentTo(o.Expectation))
            //    .WhenTypeIs<JObject>());
        }

        [Fact]
        public void Test()
        {
            List<BaseClass> expectedList = new() {
                new ChildClass("cid1", "cname1", JObject.FromObject(new SomeObjectClass("o1"))),
                new ChildClass("cid2", "cname2", JObject.FromObject(new SomeObjectClass("o2"))),
                new AnotherChildClass("cid3", "cdescription3", "ctype3")
            };
            var expected = new MainClass("id1", expectedList);

            List<BaseClass> subjectList = new() {
                new ChildClass("cid1", "cname1", JObject.FromObject(new SomeObjectClass("o1"))),
                new ChildClass("cid2s", "cname2s", JObject.FromObject(new SomeObjectClass("o2"))),
                new AnotherChildClass("cid3", "cdescription4", "ctype3")
            };
            var subject = new MainClass("id1", subjectList);

            // 1st ASSERT
            // ========================
            // Works if there is a difference in a property that exists in the BaseClass.
            // Won't work if any other property in ChildClass or AnotherChildClass is different.
            // Will work the same as 5th ASSERT with global configuration.
            subject.Should().BeEquivalentTo(expected);

            // 2nd ASSERT
            // ========================
            // Works if there is a difference in any property that is not a JObject.
            // Won't work if any JObject is different.
            //subject.Should().BeEquivalentTo(expected, options => options.RespectingRuntimeTypes());

            // 3rd ASSERT
            // ========================
            // For JObject we can use FluentAssertions.Json, as it provides a new .Should() extension method specific for json objects.
            // But then we would need to check every item in the lists ourselves.
            // We could use this in combination with 2nd ASSERT, but we would only get errors in JObjects if we succeed in 2nd ASSERT.
            //((ChildClass)subjectList.First()).SomeObject.Should().BeEquivalentTo(((ChildClass)expectedList.First()).SomeObject);

            // 4th ASSERT
            // ========================
            // If we don't do more than this, it fails even when comparing identical objects.
            // Works if we overwrite the Equals method in the classes that inherit from BaseClass, and we compare the JObject in that Equals with DeepEquals.
            // Even if it works, we cannot get precise error messages on where exactly the different is.
            //subject.Should().BeEquivalentTo(expected, options => options
            //    .RespectingRuntimeTypes()
            //    .ComparingByValue<BaseClass>());

            // 5th ASSERT
            // ========================
            // This works!!!!
            //subject.Should().BeEquivalentTo(expected, options => options
            //    .RespectingRuntimeTypes()
            //    .Using<JObject>(o => o.Subject.Should().BeEquivalentTo(o.Expectation))
            //    .WhenTypeIs<JObject>());
        }
    }
}