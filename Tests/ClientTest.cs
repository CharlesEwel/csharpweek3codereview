using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using HairSalon.Objects;


namespace HairSalon.Tests
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equals_ReturnsTrueIfContentAndRestaurantAreIdentical()
    {
      //Arrange
      Client firstClient = new Client("Wendy", "blonde", 3);
      Client secondClient = new Client("Wendy", "blonde", 3);

      //Assert
      Assert.Equal(firstClient, secondClient);
    }
    [Fact]
    public void Test_Save_SavesCorrectObjectToDatabase()
    {
      //Arrange
      Client newClient = new Client("Wendy", "blonde", 3);

      //Act
      newClient.Save();
      Client savedClient = Client.GetAll()[0];

      //Assert
      Assert.Equal(newClient, savedClient);
    }
    [Fact]
    public void Test_Find_ReturnsASpecificClientObject()
    {
      //Arrange
      Client newClient = new Client("Wendy", "blonde", 3);
      newClient.Save();

      //Act
      Client foundClient = Client.Find(newClient.GetId());

      //Assert
      Assert.Equal(newClient, foundClient);
    }
    [Fact]
    public void Test_DeleteOne_DeletesASpecificClientObject()
    {
      //Arrange
      Client firstClient = new Client("Wendy", "blonde", 3);
      firstClient.Save();
      Client secondClient = new Client("Jasper", "brunette", 2);
      secondClient.Save();

      //Act
      secondClient.Delete();
      List<Client> expectedClient = new List<Client> {firstClient};
      List<Client> testClient= Client.GetAll();

      //Assert
      Assert.Equal(expectedClient, testClient);
    }
    [Fact]
    public void Test_SetHairColorAndStylistId_AdjustsDatabaseCorrectly()
    {
      // Arrange
      Client firstClient = new Client("Wendy", "blonde", 3);
      firstClient.Save();

      //Act
      firstClient.SetHairColor("brunette");
      firstClient.SetStylistId(2);
      Client resultClient = Client.Find(firstClient.GetId());

      //Assert
      Assert.Equal("brunette", resultClient.GetHairColor());
      Assert.Equal(2, resultClient.GetStylistId());
    }
    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
