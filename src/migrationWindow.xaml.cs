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
            var path = migrationExtension.SelectedDirectory;
            if (!Directory.Exists(path))
            {
                (this.DataContext as DynamoMigratorExtension).Output = String.Format("Could not access directory at: {0}.", path);
                return;
            }
            var dynamoViewModel = (this.Owner.DataContext as DynamoViewModel);
            var files = System.IO.Directory.EnumerateFiles(path);
            //clear the output before this migration run.
            migrationExtension.Output = "";
            foreach (var file in files)
            {
                var ext = System.IO.Path.GetExtension(file);
                if (ext == ".dyn" || ext == ".dyf")
                {
                    if (System.IO.Path.GetFileName(file).Contains("jsonMigrated"))
                    {
                       migrationExtension.Output =
                       migrationExtension.Output + Environment.NewLine +
                       string.Format("⚪ attempted to migrate{0}, but it appears it was already migrated (contained jsonMigrated in its filename).", file);
                        continue;
                    }
                    var newfilepath = System.IO.Path.ChangeExtension(file, null) + "jsonMigrated" + ext;
                    dynamoViewModel.OpenCommand.Execute(file);
                    dynamoViewModel.SaveAsCommand.Execute(newfilepath);

                    //append some text to the output string.
                    migrationExtension.Output =
                        migrationExtension.Output + Environment.NewLine +
                        string.Format("⚪ attempted to migrate{0} to json and save at {1}", file, newfilepath);
                }
            }
        }

        private void select_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };

            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                (this.DataContext as DynamoMigratorExtension).SelectedDirectory = openDialog.SelectedPath;
            }
        }
    }
}
