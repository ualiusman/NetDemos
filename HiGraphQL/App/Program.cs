// See https://aka.ms/new-console-template for more information
using GraphQL;
using GraphQL.Types;

Console.WriteLine("Hello, World!");

var schema = Schema.For(@"
   type Jedi {
      name: String,
      side: String
  }

  type Query {
      hello: String,
      jedis: [Jedi],
      jedi(id: ID): Jedi
  }
", _ =>
{
_.Types.Include<Query>();
});

var root = new {Hello = "hello world!!"};
var writer =  new GraphQL.SystemTextJson.DocumentWriter();
var json = await  schema.ExecuteAsync(writer,x =>{
    x.Query = "{ hello }";
    x.Root = root;
});

Console.WriteLine(json);

public class Jedi{
    public int Id{get;set;}
    public string Name {get;set;} = string.Empty;
    public string Side {get;set;} = string.Empty;
}
public static class StarWarsDB{
 public static IEnumerable<Jedi> GetJedis() 
{
    return new List<Jedi>() {
        new Jedi(){ Id= 1, Name ="Luke", Side="Light"},
        new Jedi(){ Id = 2, Name ="Yoda", Side="Light"},
        new Jedi(){ Id = 3,  Name ="Darth Vader", Side="Dark"}
    };
}
}

public class Query 
{
    [GraphQLMetadata("jedis")]
    public IEnumerable<Jedi> GetJedis() 
    {
        return StarWarsDB.GetJedis();
    }

    [GraphQLMetadata("hello")]
    public string GetHello() 
    {
        return "Hello Query class";
    }

    [GraphQLMetadata("jedi")]
    public Jedi GetJedi(int id)
    {
        return StarWarsDB.GetJedis().SingleOrDefault(j => j.Id == id);
    }
}