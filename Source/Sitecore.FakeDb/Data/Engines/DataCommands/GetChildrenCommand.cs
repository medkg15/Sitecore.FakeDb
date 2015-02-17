﻿namespace Sitecore.FakeDb.Data.Engines.DataCommands
{
  using System.Linq;
  using Sitecore.Collections;
  using Sitecore.Diagnostics;

  public class GetChildrenCommand : Sitecore.Data.Engines.DataCommands.GetChildrenCommand, IDataEngineCommand
  {
    private DataEngineCommand innerCommand = DataEngineCommand.NotInitialized;

    public virtual void Initialize(DataEngineCommand command)
    {
      Assert.ArgumentNotNull(command, "command");

      this.innerCommand = command;
    }

    protected override Sitecore.Data.Engines.DataCommands.GetChildrenCommand CreateInstance()
    {
      return this.innerCommand.CreateInstance<Sitecore.Data.Engines.DataCommands.GetChildrenCommand, GetChildrenCommand>();
    }

    protected override ItemList DoExecute()
    {
      var item = this.innerCommand.DataStorage.GetFakeItem(this.Item.ID);
      var itemList = new ItemList();

      if (item == null)
      {
        return itemList;
      }

      var children = item.Children.Select(child => this.innerCommand.DataStorage.GetSitecoreItem(child.ID, this.Item.Language));
      itemList.AddRange(children);

      return itemList;
    }
  }
}