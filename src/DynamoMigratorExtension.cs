﻿using Dynamo.Core;
using Dynamo.Wpf.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace DynamoXMLToJsonMigrator
{
    public class DynamoMigratorExtension : NotificationObject, IViewExtension
    {
        private MenuItem menuItem;

        /// <summary>
        /// An info string about this extension.
        /// </summary>
        public string Info
        {
            get
            {
                return
                "This extension will automate the process of converting XML Dynamo files to JSON based 2.0 files.\n"+
                "Select a source directory which contains Dynamo files and press Migrate.\n"+
                "Optionally, you can also specify a target directory.\n"+
                "Files will be saved to the target directory, or if none is given the source files will be overwritten in the source directory.\n"+
                "Note: For clockwork package, it is recommended to migrate the files in batch's of 75-100 due to large number of nodes in there.";
            }
        }

        public string Name
        {
            get
            {
                return "dynamojsonMigrationExtension";
            }
        }

        private string selectedSourceDirectory = "No source directory selected";
        /// <summary>
        /// The selected source directory of dynamo files to be migrated.
        /// </summary>
        public string SelectedSourceDirectory
        {
            get
            {
                return this.selectedSourceDirectory;
            }
            set
            {
                selectedSourceDirectory = value;
                RaisePropertyChanged(nameof(SelectedSourceDirectory));
            }
        }

        public const string selectedTargetDirectoryDefault = "No target directory selected, files will be overwritten in source directory.";

        private string selectedTargetDirectory = selectedTargetDirectoryDefault;
        /// <summary>
        /// The selected target directory of dynamo files to be migrated.
        /// </summary>
        public string SelectedTargetDirectory
        {
            get
            {
                return this.selectedTargetDirectory;
            }
            set
            {
                selectedTargetDirectory = value;
                RaisePropertyChanged(nameof(SelectedTargetDirectory));
            }
        }

        private string output = "";
        /// <summary>
        /// A console like output string. Contains info about the migration procedure.
        /// </summary>
        public string Output
        {
            get
            {
                return this.output;
            }
            set
            {
                output = value;
                RaisePropertyChanged(nameof(Output));
            }
        }

        public string UniqueId
        {
            get
            {
                return "5971520e-31d5-44e4-b22a-61122ef1c932";
            }
        }

        public void Dispose()
        {

        }

        public void Loaded(ViewLoadedParams p)
        {
            menuItem = new MenuItem { Header = "MigratorExtension" };
            menuItem.Click += (sender, args) =>
            {
                var window = new MigrationWindow()
                {
                    DataContext = this,
                    MainPanel = { DataContext = this },

                    // Set the owner of the window to the Dynamo window.
                    Owner = p.DynamoWindow
                };

                window.Left = window.Owner.Left + 400;
                window.Top = window.Owner.Top + 200;

                // Show a modeless window.
                window.Show();
            };
            p.AddMenuItem(MenuBarType.View, menuItem);
        }

        public void Shutdown()
        {

        }

        public void Startup(ViewStartupParams p)
        {

        }
    }
}
