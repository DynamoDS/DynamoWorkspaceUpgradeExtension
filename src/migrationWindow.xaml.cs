using Dynamo.ViewModels;
using System;
using System.IO;
using System.Windows;

using System.Windows.Forms;

namespace DynamoXMLToJsonMigrator
{
    /// <summary>
    /// Interaction logic for migrationWindow.xaml
    /// </summary>
    public partial class MigrationWindow : Window
    {
        public MigrationWindow()
        {
            InitializeComponent();
        }

        private void migrate_Click(object sender, RoutedEventArgs e)
        {
            var migrationExtension = (this.DataContext as DynamoMigratorExtension);
            var sourcepath = migrationExtension.SelectedSourceDirectory;
            var targetpath = sourcepath;
            var replacedir = false;
            if (!Directory.Exists(sourcepath))
            {
                (this.DataContext as DynamoMigratorExtension).Output = String.Format("Could not access directory at: {0}.", sourcepath);
                return;
            }
            else if (migrationExtension.SelectedTargetDirectory != migrationExtension.selectedTargetDirectoryDefault)
            {
                targetpath = migrationExtension.SelectedTargetDirectory;
                replacedir = true;
            }
            var dynamoViewModel = (this.Owner.DataContext as DynamoViewModel);
            var files = System.IO.Directory.EnumerateFiles(sourcepath);
            //clear the output before this migration run.
            migrationExtension.Output = "";
            foreach (var file in files)
            {
                var ext = System.IO.Path.GetExtension(file);
                if (ext == ".dyn" || ext == ".dyf")
                {
                    var newfilepath = file;
                    if (replacedir)
                    {
                        newfilepath = file.Replace(sourcepath, targetpath);
                    }
                    dynamoViewModel.OpenCommand.Execute(file);
                    dynamoViewModel.SaveAsCommand.Execute(newfilepath);

                    //append some text to the output string.
                    migrationExtension.Output =
                        migrationExtension.Output + Environment.NewLine +
                        string.Format("⚪ Attempted to migrate {0} to JSON and save at {1}", file, newfilepath);
                }
            }
        }

        private void selectSource_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };

            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                (this.DataContext as DynamoMigratorExtension).SelectedSourceDirectory = openDialog.SelectedPath;
            }
        }


        private void selectTarget_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };

            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                (this.DataContext as DynamoMigratorExtension).SelectedTargetDirectory = openDialog.SelectedPath;
            }
        }
    }
}
