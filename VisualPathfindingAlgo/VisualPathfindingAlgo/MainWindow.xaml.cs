using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace VisualPathfindingAlgo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath;
        string recentPath = "RecentFiles.txt";

        public MainWindow()
        {
            InitializeComponent();

            if(File.Exists(System.IO.Path.GetFullPath(recentPath)))
            {
                List<string> recentFiles = File.ReadAllLines(System.IO.Path.GetFullPath(recentPath)).ToList();
                bool changed = false;
                for (int i = 0; i < recentFiles.Count; i++)
                {
                    if (File.Exists(System.IO.Path.GetFullPath(recentFiles[i])))
                    {
                        listRecent.Items.Add(recentFiles[i]);
                    }
                    else
                    {
                        recentFiles.Remove(recentFiles[i]);
                        changed = true;
                    }
                }
                if(changed)
                {
                    File.WriteAllLines(System.IO.Path.GetFullPath(recentPath), recentFiles.ToArray());
                }
            }
        }

        private void BtnFindPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.cav)|*.cav|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = System.IO.Path.GetFullPath(openFileDialog.FileName);
                if (File.Exists(System.IO.Path.GetFullPath(recentPath)))
                {
                    List<string> files = File.ReadAllLines(System.IO.Path.GetFullPath(recentPath)).ToList();
                    bool temp = false;
                    for (int i = 0; i < files.Count; i++)
                    {
                        if (files[i] == openFileDialog.FileName)
                        {
                            temp = true;
                            break;
                        }
                    }
                    if (!temp)
                    {
                        files.Add(openFileDialog.FileName);
                        File.WriteAllLines(System.IO.Path.GetFullPath(recentPath), files.ToArray());
                    }
                }
                else
                {
                    File.WriteAllText(System.IO.Path.GetFullPath(recentPath), openFileDialog.FileName);
                }
            }

            Path path = new Path(filePath);
            this.Close();
        }

        private void ListRecent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filePath = System.IO.Path.GetFullPath(listRecent.SelectedItem.ToString());

            Path path = new Path(filePath);
            this.Close();
        }
    }
}
