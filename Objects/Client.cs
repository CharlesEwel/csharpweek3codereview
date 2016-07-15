using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon.Objects
{
  public class Client
  {
    private int _id;
    private string _name;
    private string _hairColor;
    private int _stylistId;

    public Client(string name, string hairColor, int stylistId, int id = 0)
    {
      _name = name;
      _hairColor = hairColor;
      _stylistId = stylistId;
      _id = id;
    }

    public int GetId()
    {
        return _id;
    }
    public string GetName()
    {
        return _name;
    }
    public string GetHairColor()
    {
        return _hairColor;
    }
    public int GetStylistId()
    {
        return _stylistId;
    }

    public void SetName(string newName)
    {
      _name = newName;
      // SqlConnection conn = DB.Connection();
      // SqlDataReader rdr = null;
      // conn.Open();
      //
      // SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @clientName where id = @id;", conn);
      //
      // SqlParameter nameParameter = new SqlParameter();
      // nameParameter.ParameterName = "@clientName";
      // nameParameter.Value = newName;
      //
      // SqlParameter idParameter = new SqlParameter();
      // idParameter.ParameterName = "@id";
      // idParameter.Value = this.GetId();
      //
      // cmd.Parameters.Add(nameParameter);
      // cmd.Parameters.Add(idParameter);
      // rdr = cmd.ExecuteReader();
      //
      // if (rdr != null)
      // {
      //   rdr.Close();
      // }
      // if (conn != null)
      // {
      //   conn.Close();
      // }
    }
    public void SetHairColor(string newHairColor)
    {
      _hairColor = newHairColor;
      // SqlConnection conn = DB.Connection();
      // SqlDataReader rdr = null;
      // conn.Open();
      //
      // SqlCommand cmd = new SqlCommand("UPDATE clients SET user_id = @userId where id = @id;", conn);
      //
      // SqlParameter userIdParameter = new SqlParameter();
      // userIdParameter.ParameterName = "@userId";
      // userIdParameter.Value = newUserId.ToString();
      //
      // SqlParameter idParameter = new SqlParameter();
      // idParameter.ParameterName = "@id";
      // idParameter.Value = this.GetId();
      //
      // cmd.Parameters.Add(userIdParameter);
      // cmd.Parameters.Add(idParameter);
      // rdr = cmd.ExecuteReader();
      //
      // if (rdr != null)
      // {
      //   rdr.Close();
      // }
      // if (conn != null)
      // {
      //   conn.Close();
      // }
    }
    public void SetStylistId(int newStylistId)
    {
      _stylistId = newStylistId;
      // SqlConnection conn = DB.Connection();
      // SqlDataReader rdr = null;
      // conn.Open();
      //
      // SqlCommand cmd = new SqlCommand("UPDATE clients SET stylist_id = @stylistId where id = @id;", conn);
      //
      // SqlParameter stylistIdParameter = new SqlParameter();
      // stylistIdParameter.ParameterName = "@stylistId";
      // stylistIdParameter.Value = newStylistId.ToString();
      //
      // SqlParameter idParameter = new SqlParameter();
      // idParameter.ParameterName = "@id";
      // idParameter.Value = this.GetId();
      //
      // cmd.Parameters.Add(stylistIdParameter);
      // cmd.Parameters.Add(idParameter);
      // rdr = cmd.ExecuteReader();
      //
      // if (rdr != null)
      // {
      //   rdr.Close();
      // }
      // if (conn != null)
      // {
      //   conn.Close();
      // }
    }

    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client)) return false;
      else
      {
        Client newClient = (Client) otherClient;
        bool nameEquality = this.GetName() == newClient.GetName();
        bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
        bool hairColorEquality = this.GetHairColor() == newClient.GetHairColor();
        return(nameEquality && stylistEquality && hairColorEquality);
      }
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        string clientHairColor = rdr.GetString(2);
        int clientStylistId = rdr.GetInt32(3);
        Client newClient = new Client(clientName, clientHairColor, clientStylistId, clientId);
        allClients.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allClients;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, hair_color, stylist_id) OUTPUT INSERTED.id VALUES (@clientName, @clientHairColor, @clientStylistId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@clientName";
      nameParameter.Value = this.GetName();

      SqlParameter hairColorParameter = new SqlParameter();
      hairColorParameter.ParameterName = "@clientHairColor";
      hairColorParameter.Value = this.GetHairColor();

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@clientStylistId";
      stylistIdParameter.Value = this.GetStylistId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(hairColorParameter);
      cmd.Parameters.Add(stylistIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

    }
    public static Client Find(int clientId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @clientId;", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@clientId";
      clientIdParameter.Value = clientId.ToString();

      cmd.Parameters.Add(clientIdParameter);

      int foundClientId = 0;
      string foundClientName = null;
      string foundClientHairColor = null;
      int foundClientStylistId = 0;
      int clientUserId = 0;
      int clientRating = 0;
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(0);
        foundClientName = rdr.GetString(1);
        foundClientHairColor = rdr.GetString(2);
        foundClientStylistId = rdr.GetInt32(3);
      }
      Client newClient = new Client(foundClientName, foundClientHairColor, foundClientStylistId, foundClientId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return newClient;
    }
    // public void Delete()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);
    //
    //   SqlParameter clientIdParameter = new SqlParameter();
    //   clientIdParameter.ParameterName = "@ClientId";
    //   clientIdParameter.Value = this.GetId();
    //
    //   cmd.Parameters.Add(clientIdParameter);
    //   cmd.ExecuteNonQuery();
    //
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
