namespace PSW.Core.Probability
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GameBoardElementList<T>
    {
        private List<ElementList> elementList = new();

        public bool IsEmpty { get { return elementList.Count <= 0; } }

        /// <summary>
        /// 랜덤 가중치 요소 class
        /// </summary>
        public class ElementList
        {
            public T target;
            public float probability;

            public ElementList(T target, float probability)
            {
                this.target = target;
                this.probability = probability;
            }
        }

        /// <summary>
        /// 랜덤 가중치 배열에 요소 추가
        /// </summary>
        /// <param name="target"></param>
        /// <param name="probability"></param>
        public void Add(T target, float probability)
        {
            this.elementList.Add(new ElementList(target, probability));
        }

        public T Get()
        {
            float totalProbability = 0;

            // 배열에 등록된 모든 요소에 가중치 합산
            for (int i = 0; i < this.elementList.Count; i++)
            {
                totalProbability += this.elementList[i].probability;
            }

            // 총합 가중치 내에서 랜덤으로 숫자 하나 선택
            float pick = Random.value * totalProbability;

            for (int i = 0; i < this.elementList.Count; i++)
            {
                if (pick < this.elementList[i].probability)
                {
                    // 가중치보다 낮을 경우 return
                    return this.elementList[i].target;
                }
                else
                {
                    // 가중치보다 높을 경우 pick 에서 해당 가중치 만큼 감산
                    pick -= this.elementList[i].probability;
                }
            }

            // null
            return default;
        }
    }
}
