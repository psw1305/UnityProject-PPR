using PSW.Core.Enums;
using System;
using UnityEngine;

namespace PSW.Core.Structs
{
    [Serializable]
    public struct StatModifierData
    {
        public StatType type;
        public int value;

        public string StatText()
        {
            if (value >= 0)
                return type + " +" + value;
            else 
                return type + " -" + value;
        }
    }

    [Serializable]
    public struct AbilityData
    {
        public UseableAbility type;
        public ElementBlueprint element;
    }
}

namespace PSW.Core.Map
{
    [Serializable]
    public struct MapLayer
    {
        public MapNodeType nodeType;
        public float distanceFromPreviousLayer;
        public float nodesApartDistance;
        [Range(0.0f, 1.0f)] public float randomizeNodes;
    }
}