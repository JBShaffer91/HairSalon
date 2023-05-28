using Xunit;
using System.Collections.Generic;
using System;
using HairSalon.Models;
using Microsoft.EntityFrameworkCore;

namespace HairSalon.Tests
{
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hair_salon_test;";
    }

    [Fact]
    public void GetAll_ClientsEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Equals_ReturnsTrueForSameName_Client()
    {
      //Arrange, Act
      Client firstClient = new Client("Client1", 1);
      Client secondClient = new Client("Client1", 1);

      //Assert
      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Save_SavesClientToDatabase_ClientList()
    {
      //Arrange
      Client testClient = new Client("Client1", 1);
      testClient.Save();

      //Act
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client> {testClient};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Save_AssignsIdToClient_Id()
    {
      //Arrange
      Client testClient = new Client("Client1", 1);
      testClient.Save();

      //Act
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.Id;
      int testId = testClient.Id;

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Find_FindsClientInDatabase_Client()
    {
      //Arrange
      Client testClient = new Client("Client1", 1);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.Id);

      //Assert
      Assert.Equal(testClient, foundClient);
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
