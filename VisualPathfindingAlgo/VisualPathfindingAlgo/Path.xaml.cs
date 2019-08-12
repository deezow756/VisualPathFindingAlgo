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
using System.Windows.Shapes;

namespace VisualPathfindingAlgo
{
    /// <summary>
    /// Interaction logic for Path.xaml
    /// </summary>
    public partial class Path : Window
    {
        Map map;
        Node[] nodes;
        public int[,] matrix;
        FileManager fileManager;

        public Path(string _filePath)
        {
            InitializeComponent();

            fileManager = new FileManager(_filePath);
            fileManager.LoadFile();
            nodes = fileManager.nodes;
            matrix = fileManager.matrix;

            map = new Map(this,nodes, matrix);
            map.DisplayNodesOnMap();

            this.Show();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            PathFinder pathFinder = new PathFinder(this, map, nodes.Length - 1, nodes, matrix, fileManager);
            pathFinder.FindPath();
        }
    }
}
