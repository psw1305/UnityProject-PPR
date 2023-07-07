namespace PSW.Core.Probability
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GameBoardElementList<T>
    {
        private List<ElementList> elementList = new();

        public bool IsEmpty { get { return elementList.Count <= 0; } }

        /// <summary>
        /// ���� ����ġ ��� class
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
        /// ���� ����ġ �迭�� ��� �߰�
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

            // �迭�� ��ϵ� ��� ��ҿ� ����ġ �ջ�
            for (int i = 0; i < this.elementList.Count; i++)
            {
                totalProbability += this.elementList[i].probability;
            }

            // ���� ����ġ ������ �������� ���� �ϳ� ����
            float pick = Random.value * totalProbability;

            for (int i = 0; i < this.elementList.Count; i++)
            {
                if (pick < this.elementList[i].probability)
                {
                    // ����ġ���� ���� ��� return
                    return this.elementList[i].target;
                }
                else
                {
                    // ����ġ���� ���� ��� pick ���� �ش� ����ġ ��ŭ ����
                    pick -= this.elementList[i].probability;
                }
            }

            // null
            return default;
        }
    }
}
