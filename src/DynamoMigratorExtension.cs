using Dynamo.Core;
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
                "This extension will automate the process of converting xml dynamo files to json based 2.0 files."+
                "Select A directory which contains dynamo files and press migrate."+
                "Files will have jsonMigrated appended to their names and will be saved into the same directory.";
            }
        }

        public string Name
        {
            get
            {
                return "dynamojsonMigrationExtension";
            }
        }

        private string selectedDirectory = "no Directory Selected";
        /// <summary>
        /// The selected directory of dynamo files to be migrated.
        /// </summary>
        public string SelectedDirectory
        {
            get
            {
                return this.selectedDirectory;
            }
            set
            {
                selectedDirectory = value;
                RaisePropertyChanged(nameof(SelectedDirectory));
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
