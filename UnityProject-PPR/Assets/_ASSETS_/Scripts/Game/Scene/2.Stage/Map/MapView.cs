using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace PSW.Core.Map
{
    public class MapView : BehaviourSingleton<MapView>
    {
        public List<MapConfig> allMapConfigs;
        public GameObject stageNodePrefab;
        public float orientationOffset;

        [Header("View Settings")]
        [SerializeField] private Camera stageCamera;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private float paddingScrollHeight = 64;
        private GameObject mapParent;

        [Header("Line Settings")]
        public GameObject uiLinePrefab;
        public float offsetFromNodes = 0.1f;

        [Header("Colors")]
        public Color32 visitedColor = Color.white;
        public Color32 lockedColor = Color.gray;
        public Color32 lineVisitedColor = Color.white;
        public Color32 lineLockedColor = Color.gray;

        public readonly List<MapNode> MapNodes = new ();
        protected readonly List<NodeLineConnection> lineConnections = new ();

        public Map Map { get; protected set; }


        private MapNode GetNode(Point p)
        {
            return this.MapNodes.FirstOrDefault(n => n.Node.point.Equals(p));
        }

        private MapConfig GetConfig(string configName)
        {
            return this.allMapConfigs.FirstOrDefault(c => c.name == configName);
        }

        private NodeBlueprint GetBlueprint(string blueprintName)
        {
            var config = GetConfig(MapManager.Instance.CurrentMap.configName);
            return config.nodeBlueprints.FirstOrDefault(n => n.name == blueprintName);
        }

        public GameObject GetMapParent()
        { 
            return this.mapParent; 
        }

        public void ShowMap(Map m)
        {
            if (m == null) return;

            this.Map = m;
            this.MapNodes.Clear();

            CreateMapParent();
            CreateNodes(m.nodes);
            DrawLines();
            SetAttainableNodes();
            SetLineColors();
        }

        private void CreateMapParent()
        {
            this.mapParent = new GameObject("MapParent");
            this.mapParent.transform.SetParent(this.scrollRect.content.transform);
            this.mapParent.transform.localScale = Vector3.one;
            
            var mapParentRT = this.mapParent.AddComponent<RectTransform>();
            Stretch(mapParentRT);

            SetMapLength();
        }

        private static void Stretch(RectTransform rt)
        {
            rt.localPosition = Vector3.zero;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.sizeDelta = Vector2.zero;
            rt.anchoredPosition = Vector2.zero;
        }

        private void SetMapLength()
        {
            var content = this.scrollRect.content;
            var length = Map.DistanceBetweenFirstAndLastLayers();
            var sizeDelta = content.sizeDelta;
            sizeDelta.y = length + paddingScrollHeight * 2.0f;

            content.sizeDelta = sizeDelta;
            content.localPosition = new Vector3(0, orientationOffset, 0);
            this.mapParent.transform.localPosition += new Vector3(0, -(length / 2.0f), 0);
        }

        private void CreateNodes(IEnumerable<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                var mapNode = CreateMapNode(node);
                this.MapNodes.Add(mapNode);
            }
        }

        private MapNode CreateMapNode(Node node)
        {
            var mapNodeObject = Instantiate(this.stageNodePrefab, this.mapParent.transform);
            var mapNode = mapNodeObject.GetComponent<MapNode>();
            var blueprint = GetBlueprint(node.blueprintName);
            
            mapNode.Set(node, blueprint);
            mapNode.transform.localPosition = node.position;
            
            return mapNode;
        }

        public void SetAttainableNodes()
        {
            foreach (var node in this.MapNodes)
            {
                node.SetState(StageState.Locked);
            }

            var currentMap = MapManager.Instance.CurrentMap;

            if (currentMap.path.Count == 0)
            {
                foreach(var node in this.MapNodes.Where(n => n.Node.point.y == 0))
                {
                    node.SetState(StageState.Attainable);
                }
            }
            else
            {
                foreach (var point in currentMap.path)
                {
                    var mapNode = GetNode(point);
                    
                    if (mapNode != null)
                    {
                        mapNode.SetState(StageState.Visited);
                    }
                }

                var currentPoint = currentMap.path[currentMap.path.Count - 1];
                var currentNode = currentMap.GetNode(currentPoint);

                foreach (var point in currentNode.outgoing)
                {
                    var mapNode = GetNode(point);

                    if (mapNode != null)
                    {
                        mapNode.SetState(StageState.Attainable);
                    }
                }
            }
        }

        public void SetLineColors()
        {
            foreach (var connection in this.lineConnections)
            {
                connection.SetColor(lineLockedColor);
            }

            var currentMap = MapManager.Instance.CurrentMap;

            if (currentMap.path.Count == 0) return;

            var currentPoint = currentMap.path[currentMap.path.Count - 1];
            var currentNode = currentMap.GetNode(currentPoint);

            foreach (var point in currentNode.outgoing)
            {
                var lineConnection = this.lineConnections.FirstOrDefault
                    (
                        conn => conn.from.Node == currentNode && conn.to.Node.point.Equals(point)
                    );

                lineConnection?.SetColor(this.lineVisitedColor);
            }

            if (currentMap.path.Count <= 1) return;

            for (var i = 0; i < currentMap.path.Count - 1; i++)
            {
                var current = currentMap.path[i];
                var next = currentMap.path[i + 1];
                var lineConnection = this.lineConnections.FirstOrDefault
                    (
                        conn => conn.from.Node.point.Equals(current) && conn.to.Node.point.Equals(next)
                    );

                lineConnection?.SetColor(this.lineVisitedColor);
            }
        }

        private void DrawLines()
        {
            foreach (var node in MapNodes)
            {
                foreach (var connection in node.Node.outgoing)
                {
                    AddLineConnection(node, GetNode(connection));
                }
            }
        }

        private void AddLineConnection(MapNode from, MapNode to)
        {
            if (this.uiLinePrefab == null) return;

            var uiLineObject = Instantiate(this.uiLinePrefab, this.mapParent.transform);
            var uiLineRenderer = uiLineObject.GetComponent<UILineRenderer>();
            uiLineRenderer.transform.SetAsFirstSibling();

            var fromRT = from.transform as RectTransform;
            var toRT = to.transform as RectTransform;
            var fromPoint = fromRT.anchoredPosition + NormalizedPoint(toRT, fromRT);
            var toPoint = toRT.anchoredPosition + NormalizedPoint(fromRT, toRT);

            uiLineRenderer.transform.position = from.transform.position + (Vector3)NormalizedPoint(toRT, fromRT);

            var list = new List<Vector2>();
            for (int i = 0; i < 2; i++)
            {
                list.Add(Vector3.Lerp(Vector3.zero, (toPoint - fromPoint) + NormalizedPoint(fromRT, toRT) * 200, i));
            }
            uiLineRenderer.Points = list.ToArray();

            this.lineConnections.Add(new NodeLineConnection(uiLineRenderer, from, to));
        }

        private Vector2 NormalizedPoint(RectTransform from, RectTransform to)
        {
            return (from.anchoredPosition - to.anchoredPosition).normalized * this.offsetFromNodes;
        }
    }
}
