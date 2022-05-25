using System;
using System.Collections.Generic;

namespace CIM_Labyrint
{
    class Grafer<T>
    {


        public List<Node<T>> Nodes { get; private set; } = new List<Node<T>>();

        public void AddNode(T data)
        {
            Nodes.Add(new Node<T>(data));
        }

        public void AddDirectionalEdge(T from, T to)
        {
            Node<T> fromNode = Nodes.Find(x => x.Data.Equals(from));

            Node<T> toNode = Nodes.Find(x => x.Data.Equals(to));

            if (fromNode.Equals(default(T)) && toNode.Equals(default(T)))
            {
                fromNode.AddEdge(toNode);
            }
            else
            {
                Console.WriteLine("Node not Found");
            }


        }

        public void AddEdge(T from, T to)
        {
            Node<T> fromNode = Nodes.Find(x => x.Data.Equals(from));

            Node<T> toNode = Nodes.Find(x => x.Data.Equals(to));

            if (!fromNode.Equals(default(T)) && !toNode.Equals(default(T)))
            {
                fromNode.AddEdge(toNode);
                toNode.AddEdge(toNode);
            }
            else
            {
                Console.WriteLine("Node not Found");
            }
        }



    }
}
