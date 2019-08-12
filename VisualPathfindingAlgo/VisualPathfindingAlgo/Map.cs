using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VisualPathfindingAlgo
{
    public class Map
    {
        public Path path { get; set; }

        public const int Multi = 12;

        public Node[] Nodes { get; set; }
        public int[,] Matrix { get; set; }

        public List<Button> UiNodes { get; set; }
        public Line[,] Lines { get; set; }

        public Map(Path path, Node[] _nodes, int[,] _matrix)
        {
            this.path = path;
            this.Nodes = _nodes;
            this.Matrix = _matrix;
            UiNodes = new List<Button>();
            Lines = new Line[Nodes.Length, Nodes.Length];
        }

        public void DisplayNodesOnMap()
        {
            for (int i = 1; i < Nodes.Length; i++)
            {
                for (int j = 1; j < Nodes.Length; j++)
                {
                    if (Matrix[i, j] == 1)
                    {
                        System.Windows.Shapes.
                        Line line = new Line();
                        line.X1 = ((Nodes[i].X + 1) * Multi) + 10;
                        line.Y1 = ((Nodes[i].Y + 1) * Multi) + 10;
                        line.X2 = ((Nodes[j].X + 1) * Multi) + 10;
                        line.Y2 = ((Nodes[j].Y + 1) * Multi) + 10;                        

                        SolidColorBrush brushColour = new SolidColorBrush();
                        brushColour.Color = Colors.Black;

                        line.Stroke = brushColour;
                        line.StrokeThickness = 1;

                        line.VerticalAlignment = VerticalAlignment.Top;
                        line.HorizontalAlignment = HorizontalAlignment.Left;

                        path.grid.Children.Add(line);

                        Lines[i, j] = line;
                    }
                }
            }

            for (int i = 1; i < Nodes.Length; i++)
            {
                Button button = new Button();

                button.Content = (i).ToString();
                button.Name = "Node" + (i).ToString();
                button.Height = 20;
                button.Width = 20;
                button.Click += NodeButton_Click;
                path.grid.Children.Add(button);
                button.VerticalAlignment = VerticalAlignment.Top;
                button.HorizontalAlignment = HorizontalAlignment.Left;

                int X = 0;
                int Y = 0;

                X = (Nodes[i].X + 1) * Multi;

                Y = (Nodes[i].Y + 1) * Multi;

                button.Margin = new Thickness(X, Y, 0, 0);

                UiNodes.Add(button);
            }            
        }

        private void NodeButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = ((Button)sender);
            string path = "";
            Node node = Nodes[int.Parse(btn.Name.Split('e')[1])];

            if (node.Path != null)
            {
                for (int i = 0; i < node.Path.Count; i++)
                {
                    path += node.Path[i].ToString();
                }
            }

            MessageBox.Show("X: " + node.X.ToString() + "\nY: " + node.Y.ToString() + "\nPath: " + path +
                "\nTotal Distance: " + node.TotalDistance.ToString() + "\nCost: " + node.Cost.ToString());
        }
    }
}
