using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;

namespace CIM_Labyrint
{
    public class StartAlgoritme : Component
    {
        private Thread StartAlgoritmeThread;

        public static Semaphore mySemaphore = new Semaphore(1, 1);

        static Mutex m = new Mutex();

        public Vector2 velocityd;

        public StartAlgoritme()
        {

            StartAlgoritmeThread = new Thread(StartBFS);

            StartAlgoritmeThread.IsBackground = true;

            StartAlgoritmeThread.Start();

        }

        private void StartBFS()
        {
            while (true)
            {
                BeregningePosition();

            }

        }

        void BeregningePosition()
        {
            mySemaphore.WaitOne();

            Grafer<string> grafer = new Grafer<string>();

            grafer.AddNode("start");
            grafer.AddNode("Ice Blaster");
            grafer.AddNode("Slot Machines");
            grafer.AddNode("Rocket Ships");
            grafer.AddNode("3D Cinema");
            grafer.AddNode("Slot Machines");
            grafer.AddNode("Funhouse");
 
            grafer.AddEdge("start", "Ice Blaster");
            grafer.AddEdge("start", "Funhouse");
            grafer.AddEdge("start", "Slot Machines");
            grafer.AddEdge("Ice Blaster", "Funhouse");
            grafer.AddEdge("Ice Blaster", "Slot Machines");
            grafer.AddEdge("Ice Blaster", "Rocket Ships");
            grafer.AddEdge("Ice Blaster", "3D Cinema");










            Node<string> n = DFS<string>(grafer.Nodes.Find(x => x.Data == "start"), grafer.Nodes.Find(x => x.Data == "3D Cinema"));



            List<Node<string>> path = FindPath<string>(n, grafer.Nodes.Find(x => x.Data == "start"));

            foreach (Node<string> item in path)
            {
                Node<string> item1 = item;
                Bevegelse(item1);
            }


            mySemaphore.Release();

        }

        private void Bevegelse(Node<string> item1)

        {
            if (m.WaitOne(1))
            {



                switch (item1.Data)
                {
                    case "start":
                        velocityd += new Vector2(0.001f, 0);
                        Thread.Sleep(500);
                        break;
                    case "Ice Blaster":
                        velocityd += new Vector2(0.001f, 0);
                        Thread.Sleep(500);
                        break;

                }

            }

        }



        private static Node<T> DFS<T>(Node<T> start, Node<T> goal)
        {
            Queue<Edge<T>> edges = new Queue<Edge<T>>();
            edges.Enqueue(new Edge<T>(start, start));
            while (edges.Count > 0)
            {
                Edge<T> edge = edges.Dequeue();

                if (!edge.To.Discovered)
                {
                    edge.To.Discovered = true;

                    edge.To.Parent = edge.From;

                }
                if (edge.To == goal)
                {

                    return edge.To;
                }

                foreach (var item in edge.To.Edges)
                {
                    if (!item.To.Discovered)
                    {

                        Thread.Sleep(30);
                        edges.Enqueue(item);
                    }
                }


            }
            return null;
        }
        private static List<Node<T>> FindPath<T>(Node<T> node, Node<T> start)
        {
            List<Node<T>> path = new List<Node<T>>();
            while (!node.Equals(start))
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Add(start);
            path.Reverse();
            return path;
        }

    }
}
