<p align="center">
  <a href="#">
    <img alt="FlySQL" height="128" src="./.github/resources/logo.png">
    <h1 align="center">FlySQL</h1>
  </a>
</p>

<p align="center">
   <a aria-label="stable-release-version" href="https://www.nuget.org/packages/Fly.SQL/1.0.3" target="_blank">
    <img alt="FlySQL Stable Release" src="https://img.shields.io/nuget/v/Fly.SQL.svg?style=flat-square&cache=3600&label=Stable&labelColor=000000&color=0ea5e9" />
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
    <PackageReference Include="Fly.SQL" Version="1.0.3" />
```

### Packet CLI

```
    paket add Fly.SQL
```

### Script & Interactive

```
    #r "nuget: Fly.SQL, 1.0.3"
```

### Cake

```
    // Cake Addin
    #addin nuget:?package=Fly.SQL&version=1.0.3

    // Cake Tool
    #tool nuget:?package=Fly.SQL&version=1.0.3
```

## Package Layout

> SQL is the base class to use for the methods to be added

- ### **[`SQL`](#set-up-file)**
- [`Connect(string connection-string)`](#connection-example)
- [`Query(string statement)`](#query-example)
- [`Read()`](#reader-example)
- [`Study()`](#read-example)

> These are condensed versions of MySqlDataReader's parsing methods for incoming values (GetInt32, GetString, ...)

- [`Guid(int)`]
- [`String(int)`](#full-read-example)
- [`Int(int)`](#full-read-example)
- `Long(int)`
- `Double(int)`
- `Float(int)`
- `Decimal(int)`
- `Date(int)`
- `Bool(int)`

> Same as above, but for possible null values. Condensed versions of MySqlDataReader null checks for incoming values (IsDBNull).

- `NGuid(int)`
  - Returns null if null
- `NString(int)`
  - Returns `""` if null
- `NStrung(int)`
  - Returns null if null
- `NInt(int)`
  - Returns 0 if null
- `NLong(int)`
  - Returns 0 if null
- `NDouble(int)`
  - Returns 0 if null
- `NFloat(int)`
  - Returns 0 if null
- `NDecimal(int)`
  - Returns 0.0m if null
- `NDate(int)`
  - Returns DateTime.MinValue if null
- `NBool(int)`
  - Returns false if null

> [The Add method is used to add parameters to the SQL query. It accepts any input types the same as AddWithValue() would accept.](#add-example)

- [`Add(string param, object value)`](#full-add-example)

> The Close method simply closes the connection to the database. The Finish method is used to prepare and execute other queries.

- [`Close()`](#close-example)
- [`Finish()`](#full-add-example)

### **[`Quick Queries`](#quick-query-examples)**

- [`Query(string statement, string param, object value)`](#quick-query-example)
- [`Select(string table)`](#select-example)
- [`Select(string table, string target)`](#select-single-example)
- [`Select(string table, string target, object value)`](#select-single-value-example)
- [`Insert(string table, params string[] columns)`](#insert-example)
- [`OneInsert(string table, params object[] values)`](#one-insert-example)
- [`Update(string table, string target, params string[] columns)`](#update-example)
- [`OneUpdate(string table, string target, object targetValue, params object[] values)`](#one-update-example)
- [`Delete(string table, string target)`](#delete-example)
- [`Delete(string table, string target, object value)`](#delete-single-example)
- [`Mad(params object[] values)`](#mad-example)
- [`Bulk(params object[] values)`](#insert-example)

### **Full Examples**

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

#### As of v1.0.1

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

<h3 id="quick-query-examples" style="font-weight: bold;">Quick Query Examples</h3>

> Quick queries are used to simplify basic sql statements even further and are used in the following examples.

<h3 id="quick-query-example">Query Example</h3>

```csharp
    using FlySQL;

    public class ReadUsers : SQL
    {
      public List<User> Get(int id)
      {
        Connect("connection-string");

        // users and courses are just sample entities
        Query(@"SELECT * FROM courses WHERE courseId IN (SELECT courseId FROM course_students WHERE userId=@userId)", "userId", id);
        // The Query(stm, param, value) method is used for if you have a more complex query with only one parameter
        // The parameter is added within the method

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

        Close();
        return users;
      }
    }
```

<h3 id="select-example">Select Example</h3>

```csharp
    using FlySQL;

    public class ReadUsers : SQL
    {
      public List<User> Get()
      {
        Connect("connection-string");

        // User is just a sample entity
        Select("users");
        //SELECT * FROM users
        // similar to Query(); but without the need to write the SQL statement, just put the table name if you want to select all columns

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

        Close();
        return users;
      }
    }
```

<h3 id="select-single-example">Select Single Example</h3>

```csharp
    using FlySQL;

    public class ReadUser : SQL
    {
      public User Get(int id)
      {
        Connect("connection-string");

        // User is just a sample entity
        Select("users", "userId");
        //SELECT * FROM users WHERE userId=@userId
        /* similar to Query(); but without the need to write the SQL statement, just put the table name and the target column name if you want to select a single value */
        /* Note: this command only accepts one target column name. The column name is optional, and if not specified, the entire table will be returned */

        Add("@userId", Id);
        /* parameters add like normal, but make sure that they are same name as the target column name */

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

<h3 id="select-single-value-example">Select Single With Value Example</h3>

```csharp
    using FlySQL;

    public class ReadUser : SQL
    {
      public User Get(int id)
      {
        Connect("connection-string");

        // User is just a sample entity
        Select("users", "userId", id);
        //SELECT * FROM users WHERE userId=@userId
        // Just like Select(table, target) but the target value added and no need to use Add()

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

<h3 id="insert-example">Insert Example</h3>

```csharp
    using FlySQL;

    public class AddUser : SQL
    {
      public void Add(User user)
      {
        Connect("connection-string");
        // Assume id is an integer and auto-incrementing
        // User is just a sample entity
        Insert("users", "username", "name");
        // table name, column name, column name, ...
        //INSERT INTO users (username, name) VALUES (@username, @name)
        /* The Insert command will take the table name as the first parameter, and any parameters after that are the column names */

        /* Add parameters like normal, but be sure to use the same name as the column name */
        Add("@username", user.Username);
        Add("@name", user.Name);

        Finish(); // prepares statement and executes, only if using Add()

        /* You can also add multiple values at once with the Bulk() command */
        Bulk("username", user.Username, "name", user.Name);
        // Bulk("column name", value, column name, value, ...)
        /* This command will throw an error in the request if the number of parameters is not even, but there is not a limit to the number of parameters you can add */
        /* This method can be used after any other method that accepts adding parameters mentioned above */
        /* If you use Bulk(), you cannot use Add() or Bulk() again after its use, and no other commands are to be used after as well, including Finish(), as the Bulk() command assumes all values are added and immediately executes */
        /* You can use Add() before Bulk() if you want to add multiple values at once after having added one at a time with Add() */
      }
    }
```

<h3 id="one-insert-example">OneInsert Example</h3>

```csharp
    using FlySQL;

    public class AddUser : SQL
    {
      public void Add(User user)
      {
        Connect("connection-string");
        // Assume id is an integer and auto-incrementing
        // User is just a sample entity
        OneInsert("users", "username", user.Username, "name", user.Name);
        // table name, param pairs
        //INSERT INTO users (username, name) VALUES (@username, @name)
        //Will auto run Finish() after OneInsert is called, assuming that all values have been added in the OneInsert() method
      }
    }
```

<h3 id="update-example">Update Example</h3>

```csharp
    using FlySQL;

    public class EditUser : SQL
    {
      public void Edit(User user)
      {
        Connect("connection-string");
        // User is just a sample entity
        Update("users", "userId", "username", "name");
        // table name, target name, column name, column name, ...
        // UPDATE users SET username=@username, name=@name WHERE userId=@userId

        // Add parameters
        Add("@username", user.Username);
        Add("@name", user.Name);
        Add("@userId", user.Id);

        Finish(); // prepare statement and execute

        // Or Bulk() with auto execute
        Bulk("userId", user.Id, "username", user.Username, "name", user.Name);
      }
    }
```

<h3 id="one-update-example">OneUpdate Example</h3>

```csharp
    using FlySQL;

    public class EditUser : SQL
    {
      public void Edit(User user)
      {
        Connect("connection-string");
        // User is just a sample entity
        OneUpdate("users", "userId", user.Id,"username", user.Username, "name", user.Name);
        // table name, target name, target value, param pairs ...
        // UPDATE users SET username=@username, name=@name WHERE userId=@userId
        // Will auto run Finish() after OneUpdate is called, assuming that all values have been added in the OneUpdate() method
      }
    }
```

<h3 id="delete-example">Delete Example</h3>

```csharp
    using FlySQL;

    public class DeleteUser : SQL
    {
      public void Delete(int id)
      {
        Connect("connection-string");
        // User is just a sample entity
        Delete("users", "userId");
        // table name, target name
        // DELETE FROM users WHERE userId=@userId

        Add("@userId", id); // Add Parameter
        Finish(); // prepare statement and execute
      }
    }
```

<h3 id="delete-single-example">Delete With Value Example</h3>

```csharp
    using FlySQL;

    public class DeleteUser : SQL
    {
      public void Delete(int id)
      {
        Connect("connection-string");
        // User is just a sample entity
        Delete("users", "userId", id);
        // table name, target name, target value
        // DELETE FROM users WHERE userId=@userId
        // Will auto run Finish() after Delete() is called
      }
    }
```

<h3 id="mad-example">Mad Example</h3>

```csharp
    using FlySQL;

    // Does not have to just be edit, this is just an example to show how to use the method

    public class EditUser : SQL
    {
      public void Edit(User user)
      {
        Connect("connection-string");
        // User is just a sample entity
        Update("users", "userId", "username", "name");
        // table name, target name, column name, column name, ...
        // UPDATE users SET username=@username, name=@name WHERE userId=@userId

        // Add parameters
        Mad("@username", user.Username, "@name", user.Name, "@userId", user.Id);
        // Similar to Bulk() but does not auto Finish()
        // Add() can be used before and after Mad()
        // Mad() will throw an error if the number of parameters is not even, but there is not a limit to the number of parameters you can add

        Finish(); // prepare statement and execute
      }
    }
```

## License

The FlySQL source code is made available under the [MIT license](https://github.com/jdmay2/FlySQL/blob/main/LICENSE).
