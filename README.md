<p align="center">
  <a href="#">
    <img alt="FlySQL" height="128" src="./.github/resources/logo.png">
    <h1 align="center">FlySQL</h1>
  </a>
</p>

<p align="center">
   <a aria-label="stable-release-version" href="https://www.nuget.org/packages/Fly.SQL/1.0.1" target="_blank">
    <img alt="FlySQL Stable Release" src="https://img.shields.io/nuget/v/Fly.SQL.svg?style=flat-square&label=Stable&labelColor=000000&color=0ea5e9" />
  </a>
  <a aria-label="FlySQL is free to use" href="https://github.com/jdmay2/FlySQL/blob/main/LICENSE" target="_blank">
    <img alt="License: MIT" src="https://img.shields.io/badge/License-MIT-success.svg?style=flat-square&color=33CC12" target="_blank" />
  </a>
  <a aria-label="FlySQL downloads" href="https://nugettrends.com/packages?months=12&ids=Fly.SQL" target="_blank">
    <img alt="Downloads" src="https://img.shields.io/nuget/dt/Fly.SQL.svg?style=flat-square&cache=3600&labelColor=gray&color=33CC12&label=Downloads&logo=nuget" />
  </a>
</p>

## Description
A simple ORM for the MySql.Data NuGet package to use in any projects allowing for an easier and cleaner experience with basic MySQL operations.  

> This package currently only supports .NET 6   
   
> You do not need to add the MySql.Data package as it is already a dependency contained within this package.

## Install Package
### NuGet Package Manager
```
    Install-Package Fly.SQL
```
### .NET CLI
```
    dotnet add package Fly.SQL
```
### Package Reference
```csharp
    <PackageReference Include="Fly.SQL" Version="1.0.1" />
```
### Packet CLI
```
    paket add Fly.SQL
```
### Script & Interactive
```
    #r "nuget: Fly.SQL, 1.0.1"
```
### Cake
```
    // Cake Addin
    #addin nuget:?package=Fly.SQL&version=1.0.1
    
    // Cake Tool
    #tool nuget:?package=Fly.SQL&version=1.0.1
```

## Package Layout
> SQL is the base class to use for the methods to be added  
> 
[`SQL`](#set-up-file)  
[`Connect(string connection-string)`](#connection-example)  
[`Query(string connection-string)`](#query-example)  
[`Add(string param, value)`](#add-example)    
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

[Full Read All Example](#full-read-all-example)   
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

#### v1.0.1

```csharp
    public User Get(int id)
    {
      Connect($"server={server};port={port};database={database};user={username};password={password}");
      //Connect(string connection-string);
      ...
    }
```
#### v1.0.0

```csharp
    public User Get(int id)
    {
      Connection = $"server={server};port={port};database={database};user={username};password={password}";
      Connect();
      ...
    }
```
<h3 id="query-example">Setting the Query</h3>

```csharp
    public User Get(int id)
    {
      //Connect(cs);
      
      Query(@"SELECT * FROM users WHERE id=@userId");
      //Query(string sql-query);
    }
```
<h3 id="add-example">Adding the Parameter</h3>

```csharp
    public User Get(int id)
    {
      //Connect(cs);
      //Query(statement);
      
      Add("@userId", id);
      //Add(param, value); Currently accepts int, string, bool, datetime
    }
```
<h3 id="reader-example">Initiating the Reader</h3>

```csharp
    public User Get(int id)
    {
      //Connect(cs);
      //Query(statement);
      //Add(param, value);
      
      Read();
    }
```
<h3 id="read-example">Using the Reader</h3>

```csharp
    public User Get(int id)
    {
      //Connect(cs);
      //Query(statement);
      //Add(param, value);
      //Read();
      
      // User is just a sample entity
      Study();
      return new User()
      {
        Id = Int(0),
        Username = String(1),
        Name = String(2),
      };
    }
```
<h3 id="close-example">Closing the Connection</h3>

```csharp
    public User Get(int id)
    {
      //Connect(cs);
      //Query(statement);
      //Add(param, value);
      //Read();
      
      // User is just a sample entity
      //Study();
      //return new User()
      //{
      //  ...
      //};
      
      Close();
    }
```
<h3 id="full-read-all-example">Full Read All Example</h3>

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
<h3 id="full-read-example">Full Read Example</h3>

```csharp
    using FlySQL;
    
    public class ReadUser : SQL
    {
      public User Get(int userId)
      {
        Connect($"server={server};port={port};database={database};user={username};password={password}");
        Query(@"SELECT * FROM users WHERE id=@userId");
        Add("@userId", userId);
        Read();
        
        // User is just a sample entity
        Study();
        return new User()
        {
          Id = Int(0),
          Username = String(1),
          Name = String(2),
        };
        
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
