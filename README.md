# Console App Calling Asp.Net Core WebAPI
A sample .net core console app that calling a asp.net core wep api.

**Model (`User.cs`)**

```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

**Web api controller (`UserController.cs`)**

```csharp
[Route("api/[controller]")]
public class UserController : Controller
{
    // GET: api/values
    [HttpGet]
    public List<User> Get()
    {
        return new  List<User>{
            new User {
                Id = 1,
                Name = "user 1" },
            new User {
                Id = 2,
                Name = "user 2" },
            new User {
                Id = 3,
                Name = "user 3" },
            new User {
                Id = 4,
                Name = "user 4" }
        };
    }
    ...
```

**Console app (`Program.cs`)**

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        HttpClient client = new HttpClient();

        client.BaseAddress = new Uri("http://localhost:54741/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var users = GetUsers(client);

        foreach (var item in users.Result)
        {
            Console.WriteLine(item.Id + " " + item.Name);
        }

        Console.ReadLine();
    }

    static async Task<List<User>> GetUsers(HttpClient client)
    {
        var users = new List<User>();

        using (client)
        {
            HttpResponseMessage response = await client.GetAsync("api/user");
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadAsAsync<List<User>>();
            }
        }

        return users;
    }
}
```

We need to change package.json in console app like following.

**Console app package.json file (`project.json`)**

```csharp
...
"dependencies": {
    "Microsoft.NETCore.App": "1.1.1",
    "RestAPISample.Domain": "1.0.0-*",

    // user code: 
    // following libraries required to call web api
    "Microsoft.AspNet.WebApi.Client": "5.2.3",
    "System.Runtime.Serialization.Xml": "4.1.1" // to read response message as a generic type => ReadAsAsync<T>
},

"frameworks": {
    "netcoreapp1.0": {
      "imports": [
        "dnxcore50",

        // user code:
        // to fix build errors
        "portable-net451+win8"
      ]
    }
},
...
```

And also I added more lines to package.json file to build app on win7-x64 OS, like following. If your OS is not win7, you don't need to add this configuration.

**Console app and web api package.json file (`project.json`)**

```csharp
...
// user code:
// to fix build errors for win7
"runtimes": {
    "win7-x64": {}
}
...
```
