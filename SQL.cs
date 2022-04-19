namespace FlySQL;
public abstract class SQL
{
    private MySqlConnection con { get; set; }
    private MySqlCommand cmd { get; set; }
    private MySqlDataReader rdr { get; set; }
    public string Connection { get; set; }
    public void Connect()
    {
        con = new MySqlConnection(Connection);
    }
    public void Query(string stm)
    {
        con.Open();
        cmd = new MySqlCommand(stm, con);
    }
    public void Add(string name, string value)
    {
        cmd.Parameters.AddWithValue(name, value);
    }
    public void Add(string name, int value)
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
    public double NDouble(int i)
    {
        return rdr.IsDBNull(i) ? 0 : rdr.GetDouble(i);
    }
    public float NFloat(int i)
    {
        return rdr.IsDBNull(i) ? 0 : rdr.GetFloat(i);
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
    public double Double(int i)
    {
        return rdr.GetDouble(i);
    }
    public float Float(int i)
    {
        return rdr.GetFloat(i);
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
