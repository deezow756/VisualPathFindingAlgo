using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace VisualPathfindingAlgo
{
    public class FileManager
    {
        public string FileName { get; set; }
        public string Extention { get; set; }
        public string FilePath { get; set; }

        public Node[] nodes { get; set; }
        public int[,] matrix { get; set; }

        public FileManager(string fileName)
        {
            FileName = fileName;
            FilePath = System.IO.Path.GetFullPath(FileName);
        }

        public void SaveFile(List<int> currentPath)
        {
            string line = "";
            for (int i = 0; i < currentPath.Count; i++)
            {
                line += currentPath[i].ToString() + " ";
            }
            //Console.WriteLine(line);
            using (StreamWriter file = new StreamWriter(System.IO.Path.GetFullPath(FileName + ".csn")))
            {
                file.WriteLine(line);
            }
        }

        public void LoadFile()
        {
            if (File.Exists(FilePath))
            {
                int numOfNodes;                

                string line;
                using (StreamReader file = new StreamReader(FilePath))
                {
                    line = file.ReadLine();
                }
                string[] lineSplit = line.Split(',');
                numOfNodes = int.Parse(lineSplit[0]);

                nodes = new Node[numOfNodes + 1];
                matrix = new int[numOfNodes + 1, numOfNodes + 1];

                nodes[0] = new Node();
                int nodeCounter = 1;
                int counter = 1;

                // extracts the nodes coordinates
                do
                {
                    nodes[nodeCounter] = new Node();
                    nodes[nodeCounter].TotalDistance = 0;
                    nodes[nodeCounter].Cost = 0;
                    nodes[nodeCounter].Path = new List<int>();
                    nodes[nodeCounter].Path.Add(1);
                    nodes[nodeCounter].X = int.Parse(lineSplit[counter]);
                    counter += 1;
                    nodes[nodeCounter].Y = int.Parse(lineSplit[counter]);
                    counter += 1;
                    nodeCounter += 1;

                } while (nodeCounter < numOfNodes + 1);

                // sets the counter to after the node coordinates
                int lineCounter = numOfNodes * 2 + 1;

                // extracts the matrix data
                for (int i = 1; i < numOfNodes + 1; i++)
                {
                    for (int j = 1; j < numOfNodes + 1; j++)
                    {
                        matrix[j, i] = int.Parse(lineSplit[lineCounter]);
                        lineCounter += 1;
                    }
                }

                //for testing
                //for (int i = 1; i < numOfNodes + 1; i++)
                //    {
                //        Console.WriteLine(nodes[i].X + "," + nodes[i].Y);
                //    }
                //Console.WriteLine();
                //for (int i = 1; i < numOfNodes + 1; i++)
                //{
                //    string newLine = "";
                //    for (int j = 1; j < numOfNodes + 1; j++)
                //    {
                //        newLine += matrix[j, i].ToString() + " ";
                //    }
                //    Console.WriteLine(newLine);
                //}
                //Console.Read();
            }
            else
            {
                MessageBox.Show("File Path Was Invalid");
                return;
            }
        }
    }
}
