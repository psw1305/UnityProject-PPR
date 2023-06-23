using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace PSW.Core.Map
{
    public class Node
    {
        public readonly Point point;
        public readonly List<Point> incoming = new();
        public readonly List<Point> outgoing = new();
        [JsonConverter(typeof(StringEnumConverter))]
        public readonly MapNodeType nodeType;
        public readonly string blueprintName;

        public Vector2 position;

        public Node(MapNodeType nodeType, string blueprintName, Point point) 
        {
            this.nodeType = nodeType;
            this.blueprintName = blueprintName;
            this.point = point;
        }

        public void AddIncoming(Point p)
        {
            if (this.incoming.Any(element => element.Equals(p))) return;

            this.incoming.Add(p);
        }

        public void AddOutgoing(Point p)
        {
            if (this.outgoing.Any(element => element.Equals(p))) return;

            this.outgoing.Add(p);
        }

        public void RemoveIncoming(Point p) 
        {  
            this.incoming.RemoveAll(element => element.Equals(p));
        }

        public void RemoveOutgoing(Point p)
        {
            this.outgoing.RemoveAll(element => element.Equals(p));
        }

        public bool HasNoConnections()
        {
            return this.incoming.Count == 0 && this.outgoing.Count == 0;
        }
    }
}

