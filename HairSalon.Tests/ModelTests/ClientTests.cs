using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {
    public void Dispose()
    {
      Client.ClearAll(); // Clearing all clients after each test
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hair_salon_test;";
      // Setting connection string to test database
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Client()
    {
      // Arrange, Act
      Client firstClient = new Client("John", 1);
      Client secondClient = new Client("John", 1);

      // Assert
      Assert.AreEqual(firstClient, secondClient);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      //Arrange
      Client testClient = new Client("John", 1);

      //Act
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectClientFromDatabase_Client()
    {
      //Arrange
      Client testClient = new Client("John", 1);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.Id);

      //Assert
      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Edit_UpdatesClientInDatabase_String()
    {
      //Arrange
      string firstDescription = "John";
      Client testClient = new Client(firstDescription, 1);
      testClient.Save();
      string secondDescription = "Mike";

      //Act
      testClient.Edit(secondDescription, 1);
      string result = Client.Find(testClient.Id).Name;

      //Assert
      Assert.AreEqual(secondDescription, result);
    }

    [TestMethod]
    public void Delete_DeletesClientAssociationsFromDatabase_ClientList()
    {
      //Arrange
      Stylist testStylist = new Stylist("John");
      testStylist.Save();
      string testName = "Mike";
      Client testClient = new Client(testName, testStylist.Id);
      testClient.Save();

      //Act
      testClient.Delete();
      List<Client> resultStylistClients = testStylist.GetClients();
      List<Client> testStylistClients = new List<Client> {};

      //Assert
      CollectionAssert.AreEqual(testStylistClients, resultStylistClients);
    }
  }
}
