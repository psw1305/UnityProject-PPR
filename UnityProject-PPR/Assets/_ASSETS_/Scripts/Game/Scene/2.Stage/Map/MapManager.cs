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

        public EnemyEncounter FirstEncounter()
        {
            var num = Random.Range(0, this.config.first.Length);
            return this.config.first[num];
        }

        public EnemyEncounter RemainEncounter()
        {
            var num = Random.Range(0, this.config.remain.Length);
            return this.config.remain[num];
        }

        public EnemyEncounter EliteEncounter()
        {
            var num = Random.Range(0, this.config.elite.Length);
            return this.config.elite[num];
        }

        public EnemyEncounter BossEncounter()
        {
            var num = Random.Range(0, this.config.boss.Length);
            return this.config.boss[num];
        }
    }
}
