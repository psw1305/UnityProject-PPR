using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PSW.Core.Map
{
    public class MapPlayerTracker : BehaviourSingleton<MapPlayerTracker>
    {
        [SerializeField] private bool lockAfterSelecting = false;
        [SerializeField] private float enterNodeDelay = 0.3f;

        public bool Locked { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            this.Locked = false;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (this.Locked) return;

            if (MapManager.Instance.CurrentMap.path.Count == 0)
            {
                if (mapNode.Node.point.y == 0)
                {
                    SendPlayerToNode(mapNode);
                }
            }
            else
            {
                var currentPoint = MapManager.Instance.CurrentMap.path[MapManager.Instance.CurrentMap.path.Count - 1];
                var currentNode = MapManager.Instance.CurrentMap.GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                {
                    SendPlayerToNode(mapNode);
                }
            }
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            UISFX.Instance.Play(UISFX.Instance.mapClicks);

            this.Locked = this.lockAfterSelecting;
            MapManager.Instance.CurrentMap.path.Add(mapNode.Node.point);
            //MapManager.Instance.SaveMap();
            MapView.Instance.SetAttainableNodes();
            MapView.Instance.SetLineColors();
            mapNode.ShowSwirlAnimation();

            DOTween.Sequence().AppendInterval(this.enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }

        private static void EnterNode(MapNode mapNode)
        {
            switch (mapNode.Node.nodeType)
            {
                case MapNodeType.Starting:
                    SceneLoader.Instance.LoadAdditiveScene(SceneNames.Mystery);
                    break;
                case MapNodeType.MinorEnemy:
                    Player.BattleEnemy = MapManager.Instance.GetMinorEnemy();
                    SceneLoader.Instance.LoadAdditiveScene(SceneNames.Battle);
                    break;
                case MapNodeType.EliteEnemy:
                    Player.BattleEnemy = MapManager.Instance.GetEliteEnemy();
                    SceneLoader.Instance.LoadAdditiveScene(SceneNames.Battle);
                    break;
                case MapNodeType.RestSite:
                    SceneLoader.Instance.LoadAdditiveScene(SceneNames.RestSite);
                    break;
                case MapNodeType.Treasure:
                    SceneLoader.Instance.LoadAdditiveScene(SceneNames.Treasure);
                    break;
                case MapNodeType.Shop:
                    SceneLoader.Instance.LoadAdditiveScene(SceneNames.Shop);
                    break;
                case MapNodeType.Mystery:
                    SceneLoader.Instance.LoadAdditiveScene(SceneNames.Mystery);
                    break;
                case MapNodeType.Boss:
                    Player.BattleEnemy = MapManager.Instance.GetBossEnemy();
                    SceneLoader.Instance.LoadAdditiveScene(SceneNames.Battle);
                    break;
            }
        }
    }
}
