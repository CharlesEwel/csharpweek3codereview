using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon.Objects
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private int _price;

    public Stylist(string name, int price, int id = 0)
    {
      _name = name;
      _price = price;
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
    public int GetPrice()
    {
      return _price;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }
    public void SetPrice(int newPrice)
    {
      _price = newPrice;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if(!(otherStylist is Stylist)) return false;
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool nameEquality = this.GetName() == newStylist.GetName();
        bool priceEquality = this.GetPrice() == newStylist.GetPrice();
        return(nameEquality && priceEquality);
      }
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        int stylistPrice = rdr.GetInt32(2);
        Stylist newStylist = new Stylist(stylistName, stylistPrice, stylistId);
        allStylists.Add(newStylist);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStylists;
    }

    public List<Client> GetClients()
    {
      List<Client> allClientsMatchingStylist = new List<Client>{};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();


      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @stylistId;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = this.GetId().ToString();

      cmd.Parameters.Add(stylistIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        string clientHairColor = rdr.GetString(2);
        int clientStylistId = rdr.GetInt32(3);
        Client newClient = new Client(clientName, clientHairColor, clientStylistId, clientId);
        allClientsMatchingStylist.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allClientsMatchingStylist;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name, price) OUTPUT INSERTED.id VALUES (@stylistName, @stylistPrice);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@stylistName";
      nameParameter.Value = this.GetName();

      SqlParameter priceParameter = new SqlParameter();
      priceParameter.ParameterName = "@stylistPrice";
      priceParameter.Value = this.GetPrice();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(priceParameter);


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
    public static Stylist Find(int stylistId)
    {

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @stylistId;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = stylistId.ToString();

      cmd.Parameters.Add(stylistIdParameter);

      int foundStylistId = 0;
      string foundStylistName = null;
      int foundStylistPrice = 0;
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        foundStylistId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
        foundStylistPrice = rdr.GetInt32(2);
      }
      Stylist newStylist = new Stylist(foundStylistName, foundStylistPrice, foundStylistId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return newStylist;
    }
    public void Delete()
    {
      List<Client> clientsToDelete = this.GetClients();
      foreach (Client item in clientsToDelete)
      {
        item.Delete();
      }
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @StylistId;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();

      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
