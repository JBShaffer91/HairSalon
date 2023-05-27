using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public void Dispose()
    {
      Stylist.ClearAll(); // Clearing all stylists after each test
    }

    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hair_salon_test;";
      // Setting connection string to test database
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Stylist()
    {
      // Arrange, Act
      Stylist firstStylist = new Stylist("John", "Haircut");
      Stylist secondStylist = new Stylist("John", "Haircut");

      // Assert
      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void Save_SavesToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("John", "Haircut");

      //Act
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectStylistFromDatabase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("John", "Haircut");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.Id);

      //Assert
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void Edit_UpdatesStylistInDatabase_String()
    {
      //Arrange
      string firstDescription = "John";
      Stylist testStylist = new Stylist(firstDescription, "Haircut");
      testStylist.Save();
      string secondDescription = "Mike";

      //Act
      testStylist.Edit(secondDescription, "Haircut");
      string result = Stylist.Find(testStylist.Id).Name;

      //Assert
      Assert.AreEqual(secondDescription, result);
    }

    [TestMethod]
    public void Delete_DeletesStylistAssociationsFromDatabase_StylistList()
    {
      //Arrange
      Client testClient = new Client("Mike", 1);
      testClient.Save();
      string testName = "John";
      string testSpecialty = "Haircut";
      Stylist testStylist = new Stylist(testName, testSpecialty);
      testStylist.Save();

      //Act
      testStylist.Delete();
      List<Stylist> resultClientStylists = testClient.GetStylists();
      List<Stylist> testClientStylists = new List<Stylist> {};

      //Assert
      CollectionAssert.AreEqual(testClientStylists, resultClientStylists);
    }
  }
}
