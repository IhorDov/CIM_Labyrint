using System.Collections.Generic;
using System.Threading;

namespace CIM_Labyrint
{
    public class StartAlgoritme
    {
        private Thread StartAlgoritmeThread;

        public static Semaphore mySemaphore = new Semaphore(1, 1);


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


            mySemaphore.Release();

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
