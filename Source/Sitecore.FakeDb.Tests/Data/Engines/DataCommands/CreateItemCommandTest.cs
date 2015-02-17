﻿namespace Sitecore.FakeDb.Tests.Data.Engines.DataCommands
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.Data.Engines;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb.Data.Engines.DataCommands;
  using Sitecore.FakeDb.Data.Items;
  using Xunit;

  public class CreateItemCommandTest : CommandTestBase
  {
    [Fact]
    public void ShouldCreateInstance()
    {
      // arrange
      var createdCommand = Substitute.For<CreateItemCommand>();
      this.innerCommand.CreateInstance<Sitecore.Data.Engines.DataCommands.CreateItemCommand, CreateItemCommand>().Returns(createdCommand);

      var command = new OpenCreateItemCommand();
      command.Initialize(this.innerCommand);

      // act & assert
      command.CreateInstance().Should().Be(createdCommand);
    }

    [Fact]
    public void ShouldCreateItem()
    {
      // arrange
      var itemId = ID.NewID;
      var templateId = ID.NewID;

      var item = ItemHelper.CreateInstance(this.database);
      var destination = ItemHelper.CreateInstance(this.database);

      this.dataStorage.GetSitecoreItem(itemId).Returns(item);

      var command = new OpenCreateItemCommand() { Engine = new DataEngine(this.database) };
      command.Initialize(itemId, "home", templateId, destination);
      command.Initialize(this.innerCommand);

      // act
      var result = command.DoExecute();

      // assert
      result.Should().Be(item);
      this.dataStorage.Received().Create("home", itemId, templateId, destination);
    }

    private class OpenCreateItemCommand : CreateItemCommand
    {
      public new Sitecore.Data.Engines.DataCommands.CreateItemCommand CreateInstance()
      {
        return base.CreateInstance();
      }

      public new Item DoExecute()
      {
        return base.DoExecute();
      }
    }
  }
}