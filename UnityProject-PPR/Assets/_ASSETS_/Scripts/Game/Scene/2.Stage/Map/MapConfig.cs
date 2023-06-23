using PSW.Core.MinMax;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Core.Map
{
    [CreateAssetMenu]
    public class MapConfig : ScriptableObject
    {
        [Header("Map Types")]
        public List<NodeBlueprint> nodeBlueprints;
        public int GridWidth => Mathf.Max(numOfPreBossNodes.max, numOfStartingNodes.max);

        [Header("Map Setting")]
        public IntMinMax numOfPreBossNodes;
        public IntMinMax numOfStartingNodes;
        public int extraPaths;

        [Header("Map Layer")]
        public MapLayer[] layers;

        [Header("Enemys Setting")]
        public List<EnemyBlueprint> minors;
        public List<EnemyBlueprint> elites;
        public List<EnemyBlueprint> bosses;
    }
}
