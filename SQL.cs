namespace FlySQL;
public abstract class SQL
{
    private MySqlConnection con { get; set; }
    private MySqlCommand cmd { get; set; }
    private MySqlDataReader rdr { get; set; }
    public void Connect(string connection)
    {
        con = new MySqlConnection(connection);
    }
    public void Query(string stm)
    {
        con.Open();
        cmd = new MySqlCommand(stm, con);
    }
    public void Select(string table)
    {
        Query(@$"SELECT * FROM {table}");
    }
    public void Select(string table, string target)
    {
        string stm = @$"SELECT * FROM {table} WHERE {target} = @{target}";
        Query(stm);
    }
    public void Insert(string table, params string[] columns)
    {
        string stm = @$"INSERT INTO {table} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", columns.Select(x => $"@{x}"))})";
        Query(stm);
    }
    public void Update(string table, string target, params string[] columns)
    {
        string stm = @$"UPDATE {table} SET {string.Join(", ", columns.Select(x => $"{x} = @{x}"))} WHERE {target} = @{target}";
        Query(stm);
    }
    public void Delete(string table, string target)
    {
        string stm = @$"DELETE FROM {table} WHERE {target} = @{target}";
        Query(stm);
    }
    public void Bulk(params object[] values)
    {
        if (values.Length % 2 != 0)
        {
            throw new Exception("SQL.Mad() values must be in pairs. Please see documentation.");
        }
        else
        {
            for (int i = 0; i < values.Length; i += 2)
            {
                cmd.Parameters.AddWithValue(values[i].ToString(), values[i + 1]);
            }
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
    public void Add(string name, string value)
    {
        cmd.Parameters.AddWithValue(name, value);
    }
    public void Add(string name, int value)
    {
        cmd.Parameters.AddWithValue(name, value);
    }
    public void Add(string name, long value)
    {
        cmd.Parameters.AddWithValue(name, value);
    }
    public void Add(string name, double value)
    {
        cmd.Parameters.AddWithValue(name, value);
    }
    public void Add(string name, float value)
    {
        cmd.Parameters.AddWithValue(name, value);
    }
    public void Add(string name, decimal value)
    {
        cmd.Parameters.AddWithValue(name, value);
    }
    public void Add(string name, DateTime value)
    {
        cmd.Parameters.AddWithValue(name, value);
    }
    public void Add(string name, Boolean value)
    {
        cmd.Parameters.AddWithValue(name, value);
    }
    public void Read()
    {
        cmd.Prepare();
        rdr = cmd.ExecuteReader();
    }
    public bool Study()
    {
        return rdr.Read();
    }
    public string? NString(int i)
    {
        return rdr.IsDBNull(i) ? null : rdr.GetString(i);
    }
    public int NInt(int i)
    {
        return rdr.IsDBNull(i) ? 0 : rdr.GetInt32(i);
    }
    public long NLong(int i)
    {
        return rdr.IsDBNull(i) ? 0 : rdr.GetInt64(i);
    }
    public double NDouble(int i)
    {
        return rdr.IsDBNull(i) ? 0 : rdr.GetDouble(i);
    }
    public float NFloat(int i)
    {
        return rdr.IsDBNull(i) ? 0 : rdr.GetFloat(i);
    }
    public decimal NDecimal(int i)
    {
        return rdr.IsDBNull(i) ? 0.0m : rdr.GetDecimal(i);
    }
    public DateTime NDate(int i)
    {
        return rdr.IsDBNull(i) ? DateTime.MinValue : rdr.GetDateTime(i);
    }
    public bool NBool(int i)
    {
        return rdr.IsDBNull(i) ? false : rdr.GetBoolean(i);
    }
    public string String(int i)
    {
        return rdr.GetString(i);
    }
    public int Int(int i)
    {
        return rdr.GetInt32(i);
    }
    public long Long(int i)
    {
        return rdr.GetInt64(i);
    }
    public double Double(int i)
    {
        return rdr.GetDouble(i);
    }
    public float Float(int i)
    {
        return rdr.GetFloat(i);
    }
    public decimal Decimal(int i)
    {
        return rdr.GetDecimal(i);
    }
    public DateTime Date(int i)
    {
        return rdr.GetDateTime(i);
    }
    public bool Bool(int i)
    {
        return rdr.GetBoolean(i);
    }
    public void Finish()
    {
        cmd.Prepare();
        cmd.ExecuteNonQuery();
    }
    public void Close()
    {
        con.Close();
    }
}