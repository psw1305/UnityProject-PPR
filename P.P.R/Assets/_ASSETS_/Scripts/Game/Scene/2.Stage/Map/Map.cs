using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PSW.Core.Map
{
    public class Map
    {
        public List<Node> nodes;
        public List<Point> path;
        public string bossNodeNames;
        public string configName;

        public Map(string configName, string bossNodeName, List<Node> nodes, List<Point> path) 
        {
            this.configName = configName;
            this.bossNodeNames = bossNodeName;
            this.nodes = nodes;
            this.path = path;
        }

        public Node GetBossNode()
        {
            return nodes.FirstOrDefault(n => n.nodeType == MapNodeType.Boss);
        }

        public float DistanceBetweenFirstAndLastLayers()
        {
            var bossNode = GetBossNode();
            var firstLayerNode = nodes.FirstOrDefault(n => n.point.y == 0);

            if (bossNode == null || firstLayerNode == null) return 0;

            return bossNode.position.y - firstLayerNode.position.y;
        }

        public Node GetNode(Point point) 
        {
            return nodes.FirstOrDefault(n => n.point.Equals(point));
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}

