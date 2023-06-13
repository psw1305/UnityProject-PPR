using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

namespace PSW.Core.Map
{   
    public class MapManager : BehaviourSingleton<MapManager>
    {
        public MapConfig config;

        public Map CurrentMap { get; private set; }

        private void Start()
        {
            //if (PlayerPrefs.HasKey("Map"))
            //{
            //    var mapJson = PlayerPrefs.GetString("Map");
            //    var map = JsonConvert.DeserializeObject<Map>(mapJson);

            //    if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
            //    {
            //        GenerateNewMap();
            //    }
            //    else
            //    {
            //        this.CurrentMap = map;
            //        MapView.Instance.ShowMap(map);
            //    }
            //}
            //else
            //{
            //    GenerateNewMap();
            //}

            GenerateNewMap();
        }

        /// <summary>
        /// 스테이지 맵 새로 생성
        /// </summary>
        public void GenerateNewMap()
        {
            var map = MapGenerator.GetMap(this.config);
            this.CurrentMap = map;
            //Debug.Log(map.ToJson());
            MapView.Instance.ShowMap(map);
        }

        /// <summary>
        /// 스테이지 맵 저장
        /// </summary>
        public void SaveMap()
        {
            if (this.CurrentMap == null) return;

            var json = JsonConvert.SerializeObject(this.CurrentMap, Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            PlayerPrefs.SetString("Map", json);
            PlayerPrefs.Save();
        }

        private void OnApplicationQuit()
        {
            //SaveMap();
        }

        public EnemyBlueprint GetMinorEnemy()
        {
            var num = Random.Range(0, this.config.minors.Count);
            return this.config.minors[num];
        }

        public EnemyBlueprint GetEliteEnemy()
        {
            var num = Random.Range(0, this.config.elites.Count);
            return this.config.elites[num];
        }

        public EnemyBlueprint GetBossEnemy()
        {
            var num = Random.Range(0, this.config.bosses.Count);
            return this.config.bosses[num];
        }
    }
}
