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
            var sourcePath = migrationExtension.SelectedSourceDirectory;
            var targetPath = sourcePath;
            var targetDirectoryIsSet = false;
            if (!Directory.Exists(sourcePath))
            {
                (this.DataContext as DynamoMigratorExtension).Output = String.Format("Could not access directory at: {0}.", sourcePath);
                return;
            }
            else if (migrationExtension.SelectedTargetDirectory != migrationExtension.selectedTargetDirectoryDefault)
            {
                targetPath = migrationExtension.SelectedTargetDirectory;
                targetDirectoryIsSet = true;
            }
            var dynamoViewModel = (this.Owner.DataContext as DynamoViewModel);
            var files = System.IO.Directory.EnumerateFiles(sourcePath);
            //clear the output before this migration run.
            migrationExtension.Output = "";
            foreach (var file in files)
            {
                var ext = System.IO.Path.GetExtension(file);
                if (ext == ".dyn" || ext == ".dyf")
                {
                    var newfilepath = file;
                    if (targetDirectoryIsSet)
                    {
                        newfilepath = file.Replace(sourcePath, targetPath);
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
