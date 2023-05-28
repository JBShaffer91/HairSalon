using Xunit;
using System.Collections.Generic;
using System;
using HairSalon.Models;
using Microsoft.EntityFrameworkCore;

namespace HairSalon.Tests
{
  public class StylistTests : IDisposable
  {
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hair_salon_test;";
    }

    [Fact]
    public void GetAll_StylistsEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Equals_ReturnsTrueForSameName_Stylist()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Stylist1");
      Stylist secondStylist = new Stylist("Stylist1");

      //Assert
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Stylist1");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist> {testStylist};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Save_AssignsIdToStylist_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("Stylist1");
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.Id;
      int testId = testStylist.Id;

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Find_FindsStylistInDatabase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("Stylist1");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.Id);

      //Assert
      Assert.Equal(testStylist, foundStylist);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
