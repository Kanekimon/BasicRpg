using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class Graph
    {

        List<Vector2> nodes;

        public Graph()
        {
            nodes = new List<Vector2>();
        }

        void AddNode(Vector2 node)
        {
            nodes.Add(node);
        }

    }
}
