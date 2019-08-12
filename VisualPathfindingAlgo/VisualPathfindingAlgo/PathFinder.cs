using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VisualPathfindingAlgo
{
    public class PathFinder
    {
        private Path path;
        private Map map;
        
        private int numOfNodes;
        private Node[] nodes;
        private int[,] matrix;
        private List<int> nodesVisited;
        private List<int> nodesToVisit;
        private List<int> currentPath;
        private int currentNode;
        private FileManager fileManager;

        public PathFinder(int numOfNodes, Node[] nodes, int[,] matrix, FileManager fileManager)
        {
            this.numOfNodes = numOfNodes;
            this.nodes = nodes;
            this.matrix = matrix;
            currentNode = 1;
            nodesVisited = new List<int>();
            nodesVisited.Add(1);
            nodesToVisit = new List<int>();
            currentPath = new List<int>();
            this.fileManager = fileManager;
        }

        public PathFinder(Path path, Map map,int numOfNodes, Node[] nodes, int[,] matrix, FileManager fileManager)
        {
            this.path = path;
            this.map = map;
            this.numOfNodes = numOfNodes;
            this.nodes = nodes;
            this.matrix = matrix;
            currentNode = 1;
            nodesVisited = new List<int>();
            nodesVisited.Add(1);
            nodesToVisit = new List<int>();
            currentPath = new List<int>();
            this.fileManager = fileManager;
        }

        public void FindPath()
        {
            bool pathFound = false;
            do
            {
                if (nodes[currentNode].X == nodes[numOfNodes].X && nodes[currentNode].Y == nodes[numOfNodes].Y)
                {
                    //Console.WriteLine(nodes[numOfNodes].TotalDistance.ToString());
                    currentPath.Clear();
                    for (int i = 0; i < nodes[currentNode].Path.Count; i++)
                    {
                        currentPath.Add(nodes[currentNode].Path[i]);
                    }
                    break;
                }

                // Search for availiable nodes
                for (int i = 1; i < numOfNodes + 1; i++)
                {
                    if (matrix[currentNode, i] == 1)
                    {
                        if (!nodesVisited.Contains(i))
                        {
                            double cost = GetDistance(i) + nodes[currentNode].Cost;
                            double totalDis = GetDistance(i, numOfNodes) + cost;
                            if (nodes[i].TotalDistance > totalDis)
                            {
                                nodes[i].Cost = cost;
                                nodes[i].TotalDistance = totalDis;
                                nodes[i].Path.Clear();
                                for (int j = 0; j < nodes[currentNode].Path.Count; j++)
                                {
                                    nodes[i].Path.Add(nodes[currentNode].Path[j]);
                                }
                                nodes[i].Path.Add(i);
                            }
                            if (!nodesToVisit.Contains(i))
                            {
                                nodesToVisit.Add(i);
                                nodes[i].Cost = cost;
                                nodes[i].TotalDistance = totalDis;
                                nodes[i].Path.Clear();
                                for (int j = 0; j < nodes[currentNode].Path.Count; j++)
                                {
                                    nodes[i].Path.Add(nodes[currentNode].Path[j]);
                                }
                                nodes[i].Path.Add(i);
                            }
                        }
                    }
                }

                if (nodesToVisit.Count == 0)
                {
                    currentPath.Clear();
                    currentPath.Add(0);
                    break;
                }

                //for testing

                //string toline = "Nodes To Visit: ";
                // for (int i = 0; i < nodesToVisit.Count; i++)
                //     {
                //         toline += nodesToVisit[i].ToString() + " ";
                //     }
                // Console.WriteLine(toline);
                // Console.Read();
                // Console.Read();

                //finds the lowest distance to the end node
                int lowestNode = nodesToVisit[0];
                for (int i = 0; i < nodesToVisit.Count; i++)
                {
                    if (nodes[nodesToVisit[i]].TotalDistance < nodes[lowestNode].TotalDistance)
                    {
                        lowestNode = nodesToVisit[i];
                    }
                }

                //for testing
                //Console.WriteLine("To Visit Nodes Cost");
                //for (int i = 0; i < nodesToVisit.Count; i++)
                //{
                //    Console.WriteLine(nodesToVisit[i].ToString() + ": " + nodes[nodesToVisit[i]].TotalDistance.ToString());
                //}
                //Console.Read();
                //Console.Read();

                currentNode = lowestNode;
                nodesToVisit.Remove(currentNode);
                nodesVisited.Add(currentNode);                

            } while (pathFound == false);

            currentPath = nodes[currentNode].Path;

            //for testing
            //string pathString = "";
            //for (int i = 0; i < nodes[currentNode].Path.Count; i++)
            //{
            //    pathString += nodes[currentNode].Path[i] + " ";
            //}
            //MessageBox.Show("Path: " + pathString);

            //fileManager.SaveFile(currentPath);

            DisplayPath();
        }

        private void DisplayPath()
        {
            string pathString = "";
            for (int i = 0; i < currentPath.Count; i++)
            {
                pathString += currentPath[i] + ", ";
            }

            path.txtFullPath.Text = pathString;

            for (int i = 0; i < currentPath.Count; i++)
            {
                if(i == currentPath.Count - 1)
                {
                    break;
                }
                else
                {
                    Line line = map.Lines[currentPath[i], currentPath[i + 1]];
                    SolidColorBrush brushColour = new SolidColorBrush();
                    brushColour.Color = Colors.Red;
                    line.Stroke = brushColour;
                    line.StrokeThickness = 3;
                }
            }
        }

        private double GetDistance(int nextNode)
        {
            return Math.Sqrt(((nodes[nextNode].X - nodes[currentNode].X) * (nodes[nextNode].X - nodes[currentNode].X)) + ((nodes[nextNode].Y - nodes[currentNode].Y) * (nodes[nextNode].Y - nodes[currentNode].Y)));
        }
        private double GetDistance(int nextNode, int endNode)
        {
            return Math.Sqrt(((nodes[endNode].X - nodes[nextNode].X) * (nodes[endNode].X - nodes[nextNode].X)) + ((nodes[endNode].Y - nodes[nextNode].Y) * (nodes[endNode].Y - nodes[nextNode].Y)));
        }
    }
}
