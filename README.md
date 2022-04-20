# FlySQL  
### ‼️This package only supports .NET 6‼️
A simple ORM for the MySql.Data NuGet package to use in any projects allowing for an easier and cleaner experience with basic MySQL operations.  

> Note: You do not need to add the MySql.Data package as it is already a dependency contained within this package.

## Package Layout
> SQL is the base class to use for the methods to be added  
> 
[`SQL`](#set-up-file)  
[`Connect(string connection-string)`](#connection-example)  
[`Query(string connection-string)`](#query-example)  
[`Read()`](#reader-example)  
[`Study()`](#read-example)  

> condensed versions of MySqlDataReader Get parse methods for incoming values (GetInt32, GetString, ...) 

[`String(int)`](#full-read-example)  
[`Int(int)`](#full-read-example)  
`Double(int)`   
`Float(int)`  
`Date(int)`   
`Bool(int)`

> Same as above, but for possible null values. Condensed versions of MySqlDataReader null checks for incoming values (IsDBNull). If null, will return null for strings, 0 for ints, 0 for doubles, 0 for floats, DateTime.MinValue for DateTime's, and false for bools.

`NString(int)`  
`NInt(int)`   
`NDouble(int)`  
`NFloat(int)`   
`NDate(int)`  
`NBool(int)`  

[`Close()`](#close-example)  
[`Add(string param, string value)` (+3 Overloads)](#full-add-example)  
[`Finish()`](#full-add-example)  

[Full Read Example](#full-read-example)   
[Full Add Example](#full-add-example)   
[Full Edit Example](#full-edit-example)   
[Full Delete Example](#full-delete-example)   

## To use the Package
> We will use User as the class for the examples
<h3 id="set-up-file">Setting up the File</h3>

Add the using statement and SQL base class to setup the file for use of the package.
```csharp
    using FlySQL;
    
    public class Example : SQL
```
<h3 id="connection-example">Setting the Connection String</h3>

```csharp
    public List<User> Get()
    {
      Connect($"server={server};port={port};database={database};user={username};password={password}");
      //Connect(string connection-string);
      ...
    }
```
<h3 id="query-example">Setting the Query</h3>

```csharp
    public List<User> Get()
    {
      //Connect(cs);
      
      Query(@"SELECT * FROM users");
      //Query(string sql-query);
    }
```
<h3 id="reader-example">Initiating the Reader</h3>

```csharp
    public List<User> Get()
    {
      //Connect(cs);
      //Query(statement);
      
      Read();
    }
```
<h3 id="read-example">Using the Reader</h3>

```csharp
    public List<User> Get()
    {
      //Connect(cs);
      //Query(statement);
      //Read();
      
      // User is just a sample entity
      List<User> users = new List<User>();
      while (Study())
      {
        users.Add(new User()
          {
            Id = Int(0),
            Username = String(1),
            Name = String(2),
          });
      }
      return users;
    }
```
<h3 id="close-example">Closing the Connection</h3>

```csharp
    public List<User> Get()
    {
      //Connect(cs);
      //Query(statement);
      //Read();
      
      // User is just a sample entity
      //List<User> users = new List<User>();
      //while (Study())
      //{
      //  ...
      //}
      //return users;
      
      Close();
    }
```
<h3 id="full-read-example">Full Read Example</h3>

```csharp
    using FlySQL;
    
    public class ReadUsers : SQL
    {
      public List<User> Get()
      {
        Connect($"server={server};port={port};database={database};user={username};password={password}");
        Query(@"SELECT * FROM users");
        Read();
        
        // User is just a sample entity
        List<User> users = new List<User>();
        while (Study())
        {
          users.Add(new User()
            {
              Id = Int(0),
              Username = String(1),
              Name = String(2),
            });
        }
        return users;
        
        Close();
      }
    }
```
<h3 id="full-add-example">Full Add Example</h3>

```csharp
    using FlySQL;
    
    public class AddUser : SQL
    {
      public void Add(User user)
      {
        Connect($"server={server};port={port};database={database};user={username};password={password}");
        // Assume id is an integer and auto-incrementing
        // User is just a sample entity
        Query(@"INSERT INTO users (username, name) VALUES (@username, @name)");
        
        // Add parameters
        Add("@username", user.Username);
        Add("@name", user.Name);

        Finish(); // prepare statement and execute
      }
    }
```
<h3 id="full-edit-example">Full Edit Example</h3>

```csharp
    using FlySQL;
    
    public class EditUser : SQL
    {
      public void Edit(User user)
      {
        Connect($"server={server};port={port};database={database};user={username};password={password}");
        // User is just a sample entity
        Query($@"UPDATE users SET username=@username, name=@name WHERE id=@userId");

        // Add parameters
        Add("@username", user.Username);
        Add("@name", user.Name);
        Add("@userId", user.Id);

        Finish(); // prepare statement and execute
      }
    }
```
<h3 id="full-delete-example">Full Delete Example</h3>

```csharp
    using FlySQL;
    
    public class DeleteUser : SQL
    {
      public void Delete(int id)
      {
        Connect($"server={server};port={port};database={database};user={username};password={password}");
        // User is just a sample entity
        Query(@"DELETE FROM users WHERE userId=@userId");
        Add("@userId", id); // Add Parameter
        Finish(); // prepare statement and execute
      }
    }
```
## License
The FlySQL source code is made available under the MIT license.
