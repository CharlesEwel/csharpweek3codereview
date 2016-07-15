using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using HairSalon.Objects;


namespace HairSalon.Tests
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SavesCorrectObjectToDatabase()
    {
      //Arrange
      Stylist newStylist = new Stylist("Tracy", 17);

      //Act
      newStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];

      //Assert
      Assert.Equal(newStylist, savedStylist);
    }
    [Fact]
    public void Test_Find_ReturnsASpecificStylistObject()
    {
      //Arrange
      Stylist newStylist = new Stylist("Tracy", 17);
      newStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(newStylist.GetId());

      //Assert
      Assert.Equal(newStylist, foundStylist);
    }
    [Fact]
    public void Test_GetClients_FindsClientsByStylistId()
    {
      //Arrange
      Stylist newStylist = new Stylist("Tracy");
      newStylist.Save();

      Client firstClient = new Client("Jasper", newStylist.GetId());

      firstClient.Save();
      Client secondClient = new Client("Wendy", 2);
      secondClient.Save();
      List<Client> expectedResult = new List<Client> {firstClient};
      //Act
      List<Client> result = newStylist.GetClients();
      //Assert
      Assert.Equal(expectedResult, result);
    }
    // [Fact]
    // public void Test_Delete_DeletesStylistAndClientsByStylistId()
    // {
    //   //Arrange
    //   Stylist firstStylist = new Stylist("Tracy");
    //   firstStylist.Save();
    //   Stylist secondStylist = new Stylist("Chad");
    //   secondStylist.Save();
    //
    //   Client firstClient = new Client("Jasper", firstStylist.GetId());
    //   firstClient.Save();
    //   Client secondClient = new Client("Wendy", secondStylist.GetId());
    //   secondClient.Save();
    //
    //   List<Stylist> expectedStylist = new List<Stylist>{firstStylist};
    //   List<Client> expectedResult = new List<Client> {firstClient};
    //   //Act
    //   secondStylist.Delete();
    //
    //   List<Stylist> resultingStylist = Stylist.GetAll();
    //   List<Client> result = Client.GetAll();
    //   //Assert
    //   Assert.Equal(expectedStylist, resultingStylist);
    //   Assert.Equal(expectedResult, result);
    // }

    public void Dispose()
    {
      Stylist.DeleteAll();
      // Client.DeleteAll();
    }

  }
}
