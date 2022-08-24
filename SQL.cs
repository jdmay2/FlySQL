namespace FlySQL;
public abstract class SQL
{
    private MySqlConnection con { get; set; }
    private MySqlCommand cmd { get; set; }
    private MySqlDataReader rdr { get; set; }
    public void Connect(string connection) { con = new MySqlConnection(connection); }
    public void Query(string stm)
    {
        if (con.State == ConnectionState.Closed) con.Open();
        cmd = new MySqlCommand(stm, con);
    }
    public void Query(string stm, string param, object value)
    {
        if (con.State == ConnectionState.Closed) con.Open();
        cmd = new MySqlCommand(stm, con);
        Add($"@{param}", value);
    }
    public void Select(string table) { Query(@$"SELECT * FROM {table}"); }
    public void Select(string table, string target) { Query(@$"SELECT * FROM {table} WHERE {target} = @{target}"); }
    public void Select(string table, string target, object value)
    {
        Query(@$"SELECT * FROM {table} WHERE {target} = @{target}");
        Add($"@{target}", value);
    }
    public void Insert(string table, params string[] columns) { Query(@$"INSERT INTO {table} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", columns.Select(x => $"@{x}"))})"); }
    public void OneInsert(string table, params object[] values)
    {
        if (values.Length % 2 != 0) throw new Exception("SQL.OneInsert() values must be in pairs. Please see documentation.");
        else
        {
            IDictionary<object, object> dict = new Dictionary<object, object>();
            for (int i = 0; i < values.Length; i += 2) dict.Add(values[i], values[i + 1]);

            Query(@$"INSERT INTO {table} ({string.Join(", ", dict.Keys)}) VALUES ({string.Join(", ", dict.Keys.Select(x => $"@{x}"))})");

            for (int i = 0; i < dict.Keys.Count; i++) Add($"@{dict.Keys.ElementAt(i)}", dict.Values.ElementAt(i));
            Finish();
        }
    }
    public void Update(string table, string target, params string[] columns) { Query(@$"UPDATE {table} SET {string.Join(", ", columns.Select(x => $"{x} = @{x}"))} WHERE {target} = @{target}"); }
    public void OneUpdate(string table, string target, object targetValue, params object[] values)
    {
        if (values.Length % 2 != 0) throw new Exception("SQL.OneUpdate() values must be in pairs. Please see documentation.");
        else
        {
            IDictionary<object, object> dict = new Dictionary<object, object>();
            for (int i = 0; i < values.Length; i += 2) dict.Add(values[i], values[i + 1]);

            Query(@$"UPDATE {table} SET {string.Join(", ", dict.Keys.Select(x => $"{x} = @{x}"))} WHERE {target} = @{target}");
            Add($"@{target}", targetValue);

            for (int i = 0; i < dict.Keys.Count; i++) Add($"@{dict.Keys.ElementAt(i)}", dict.Values.ElementAt(i));
            Finish();
        }
    }
    public void Delete(string table, string target) { Query(@$"DELETE FROM {table} WHERE {target} = @{target}"); }
    public void Delete(string table, string target, object value)
    {
        Query(@$"DELETE FROM {table} WHERE {target} = @{target}");
        Add($"@{target}", value);
        Finish();
    }
    public void Mad(params object[] values)
    {
        if (values.Length % 2 != 0) throw new Exception("SQL.Mad() values must be in pairs. Please see documentation.");
        else for (int i = 0; i < values.Length; i += 2) cmd.Parameters.AddWithValue(values[i].ToString(), values[i + 1]);
    }
    public void Bulk(params object[] values)
    {
        if (values.Length % 2 != 0) throw new Exception("SQL.Bulk() values must be in pairs. Please see documentation.");
        else
        {
            for (int i = 0; i < values.Length; i += 2) cmd.Parameters.AddWithValue(values[i].ToString(), values[i + 1]);
            Finish();
        }
    }
    public void Add(string name, object value) { cmd.Parameters.AddWithValue(name, value); }
    public void Read()
    {
        cmd.Prepare();
        rdr = cmd.ExecuteReader();
    }
    public bool Study() { return rdr.Read(); }
    public Guid? NGuid(int i) { return rdr.IsDBNull(i) ? null : rdr.GetGuid(i); }
    public string? NStrung(int i) { return rdr.IsDBNull(i) ? null : rdr.GetString(i); }
    public string NString(int i) { return rdr.IsDBNull(i) ? "" : rdr.GetString(i); }
    public int NInt(int i) { return rdr.IsDBNull(i) ? 0 : rdr.GetInt32(i); }
    public long NLong(int i) { return rdr.IsDBNull(i) ? 0 : rdr.GetInt64(i); }
    public double NDouble(int i) { return rdr.IsDBNull(i) ? 0 : rdr.GetDouble(i); }
    public float NFloat(int i) { return rdr.IsDBNull(i) ? 0 : rdr.GetFloat(i); }
    public decimal NDecimal(int i) { return rdr.IsDBNull(i) ? 0.0m : rdr.GetDecimal(i); }
    public DateTime NDate(int i) { return rdr.IsDBNull(i) ? DateTime.MinValue : rdr.GetDateTime(i); }
    public bool NBool(int i) { return rdr.IsDBNull(i) ? false : rdr.GetBoolean(i); }
    public Guid Guid(int i) { return rdr.GetGuid(i); }
    public string String(int i) { return rdr.GetString(i); }
    public int Int(int i) { return rdr.GetInt32(i); }
    public long Long(int i) { return rdr.GetInt64(i); }
    public double Double(int i) { return rdr.GetDouble(i); }
    public float Float(int i) { return rdr.GetFloat(i); }
    public decimal Decimal(int i) { return rdr.GetDecimal(i); }
    public DateTime Date(int i) { return rdr.GetDateTime(i); }
    public bool Bool(int i) { return rdr.GetBoolean(i); }
    public void Finish()
    {
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        Close();
    }
    public void Close() { con.Close(); }
}