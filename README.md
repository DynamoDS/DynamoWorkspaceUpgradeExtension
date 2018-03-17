# Dynamo Workspace Upgrade Extension
A dynamo view extension which automates converting workspaces to dynamo 2.0 format.

![image](https://github.com/mjkkirschner/DynamoWorkspaceUpgradeExtension/blob/master/images/extensionImage.png)

## status:
:warning: WIP 

## build:

* Built in visual studio 2015.
* Make sure to enable restore nuget references on build.

## install:

Copy `bin/Debug/DynamoXMLtoJsonMigrator.dll` to your `<dynamo install directory>` alonside the other core dlls.
Copy `src/migration_ViewExtensionDefinition.xml` to your `<dynamo install directory>/ViewExtensions/`

## using it:
* run only on dynamo 2.x
* if the extension is loaded it will add a menu item to the view menu.
* currently will not recurse into sub folders of the selected directory.
* will append jsonMigrated to any files it migrates.
* conversion to json requires that the nodes in the graph are loaded at the time the graph is opened.
