using UnityEngine;
using UnityEngine.UI.Extensions;

namespace PSW.Core.Map
{
    [System.Serializable]
    public class NodeLineConnection
    {
        public UILineRenderer uiLine;
        public MapNode from;
        public MapNode to;

        public NodeLineConnection(UILineRenderer uiLine, MapNode from, MapNode to)
        {
            this.uiLine = uiLine;
            this.from = from;
            this.to = to;
        }

        public void SetColor(Color color)
        {
            if (uiLine != null) uiLine.color = color;
        }
    }
}