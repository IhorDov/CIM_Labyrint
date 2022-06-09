using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;

namespace CIM_Labyrint
{
    public class StartAlgoritme
    {
        private Thread StartAlgoritmeThread;

        public static Semaphore mySemaphore = new Semaphore(1, 1);

        static Mutex m = new Mutex();

        public Vector2 velocityd;

        private Enemy enemy = new Enemy();


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

            Grafer<Vector2> grafer = new Grafer<Vector2>();

            grafer.AddNode(new Vector2(96, 96));//1
            grafer.AddNode(new Vector2(160, 480));//2
            grafer.AddNode(new Vector2(288, 480));//3
            grafer.AddNode(new Vector2(288, 96));//4
            grafer.AddNode(new Vector2(416, 96));//5
            grafer.AddNode(new Vector2(416, 480));//6
            grafer.AddNode(new Vector2(544, 480));//7
            grafer.AddNode(new Vector2(544, 160));//8
            grafer.AddNode(new Vector2(800, 160));//9
            grafer.AddNode(new Vector2(800, 608));//10
            grafer.AddNode(new Vector2(928, 608));//11
            grafer.AddNode(new Vector2(928, 160));//12
            grafer.AddNode(new Vector2(992, 160));//13
            grafer.AddNode(new Vector2(992, 608));//14
            grafer.AddNode(new Vector2(1056, 608));//15
            grafer.AddNode(new Vector2(1056, 224));//16
            grafer.AddNode(new Vector2(1184, 224));//17
            grafer.AddNode(new Vector2(1184, 672));//18
            grafer.AddNode(new Vector2(608, 672));//19
            grafer.AddNode(new Vector2(608, 608));//20
            grafer.AddNode(new Vector2(352, 608));//21
            grafer.AddNode(new Vector2(352, 800));//22
            grafer.AddNode(new Vector2(1184, 800));//23
            grafer.AddNode(new Vector2(1184, 864));//24
            grafer.AddNode(new Vector2(96, 864));//25
            grafer.AddNode(new Vector2(96, 800));//26
            grafer.AddNode(new Vector2(224, 800));//27
            grafer.AddNode(new Vector2(224, 608));//28


            grafer.AddEdge(new Vector2(96, 96), new Vector2(160, 480));//1x2
            grafer.AddEdge(new Vector2(160, 480), new Vector2(288, 480));//2x3
            grafer.AddEdge(new Vector2(288, 480), new Vector2(416, 480));//3x6
            grafer.AddEdge(new Vector2(416, 480), new Vector2(416, 96));//6x5
            grafer.AddEdge(new Vector2(416, 480), new Vector2(544, 480));//6x7
            grafer.AddEdge(new Vector2(96, 96), new Vector2(288, 96));//1x4
            grafer.AddEdge(new Vector2(288, 96), new Vector2(416, 96));//4x5
            grafer.AddEdge(new Vector2(416, 96), new Vector2(544, 160));//5x8
            grafer.AddEdge(new Vector2(544, 160), new Vector2(800, 160));//8x9
            grafer.AddEdge(new Vector2(800, 160), new Vector2(928, 160));//9x12
            grafer.AddEdge(new Vector2(928, 160), new Vector2(992, 160));//12x13
            grafer.AddEdge(new Vector2(992, 160), new Vector2(1056, 224));//13x16
            grafer.AddEdge(new Vector2(1056, 224), new Vector2(1184, 224));//16x17
            grafer.AddEdge(new Vector2(1184, 224), new Vector2(1184, 672));//17x18
            grafer.AddEdge(new Vector2(1184, 672), new Vector2(608, 672));//18x19
            grafer.AddEdge(new Vector2(928, 160), new Vector2(928, 608));//12x11
            grafer.AddEdge(new Vector2(992, 160), new Vector2(992, 608));//13x14
            grafer.AddEdge(new Vector2(1056, 224), new Vector2(1056, 608));//16x15
            grafer.AddEdge(new Vector2(800, 160), new Vector2(800, 608));//9x10
            grafer.AddEdge(new Vector2(800, 608), new Vector2(608, 608));//10x20
            grafer.AddEdge(new Vector2(608, 608), new Vector2(352, 608));//20x21
            grafer.AddEdge(new Vector2(352, 608), new Vector2(224, 608));//21x28
            grafer.AddEdge(new Vector2(224, 608), new Vector2(224, 800));//28x27
            grafer.AddEdge(new Vector2(224, 800), new Vector2(96, 800));//27x26
            grafer.AddEdge(new Vector2(352, 608), new Vector2(352, 800));//21x22
            grafer.AddEdge(new Vector2(352, 800), new Vector2(1184, 800));//22x23
            grafer.AddEdge(new Vector2(1184, 800), new Vector2(1184, 864));//23x24
            grafer.AddEdge(new Vector2(1184, 864), new Vector2(96, 864));//24x25

            Node<Vector2> pathToGoal = DFS<Vector2>(grafer.Nodes.Find(x => x.Data == new Vector2(96, 96)), grafer.Nodes.Find(x => x.Data == new Vector2(96, 864)));

            List<Node<Vector2>> path = FindPath<Vector2>(pathToGoal, grafer.Nodes.Find(x => x.Data == new Vector2(96, 96)));

            foreach (Node<Vector2> pathNode in path)
            {
                Node<Vector2> pathNode1 = pathNode;
                enemy.EnemyMoveToNode(pathNode1);
            }

            mySemaphore.Release();

            //mySemaphore.WaitOne();

            //Grafer<string> grafer = new Grafer<string>();

            //grafer.AddNode("start");
            //grafer.AddNode("Ice Blaster");
            //grafer.AddNode("Slot Machines");
            //grafer.AddNode("Rocket Ships");
            //grafer.AddNode("3D Cinema");
            //grafer.AddNode("Slot Machines");
            //grafer.AddNode("Funhouse");

            //grafer.AddEdge("start", "Ice Blaster");
            //grafer.AddEdge("start", "Funhouse");
            //grafer.AddEdge("start", "Slot Machines");
            //grafer.AddEdge("Ice Blaster", "Funhouse");
            //grafer.AddEdge("Ice Blaster", "Slot Machines");
            //grafer.AddEdge("Ice Blaster", "Rocket Ships");
            //grafer.AddEdge("Ice Blaster", "3D Cinema");

            //Node<string> n = DFS<string>(grafer.Nodes.Find(x => x.Data == "start"), grafer.Nodes.Find(x => x.Data == "3D Cinema"));



            //List<Node<string>> path = FindPath<string>(n, grafer.Nodes.Find(x => x.Data == "start"));

            //foreach (Node<string> item in path)
            //{
            //    Node<string> item1 = item;
            //    Bevegelse(item1);
            //}


            //mySemaphore.Release();

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
