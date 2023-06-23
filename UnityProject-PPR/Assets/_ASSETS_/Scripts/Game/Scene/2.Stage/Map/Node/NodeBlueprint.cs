using UnityEngine;

namespace PSW.Core.Map
{
    [CreateAssetMenu]
    public class NodeBlueprint : ScriptableObject
    {
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite visitedSprite;
        [SerializeField] private MapNodeType nodeType;

        public Sprite GetNormalSprite() => this.normalSprite;
        public Sprite GetVisitedSprite() => this.visitedSprite;
        public MapNodeType GetNodeType() => this.nodeType;
    }
}