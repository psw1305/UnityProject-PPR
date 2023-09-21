using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using PSW.Core.Enums;

namespace PSW.Core.Extensions
{
    public static class ExtensionsText
    {
        /// <summary>
        /// �־��� from ���� to ���� point text ���� ī���� �ִϸ��̼�
        /// </summary>
        /// <returns></returns>
        public static IEnumerator UpdateTextCoroutine(this TextMeshProUGUI tmproText, int from, int to, float duration, string addText = "")
        {
            float currentTime = Time.timeSinceLevelLoad;
            float elapsedTime = 0.0f;
            float lastTime = currentTime;

            while (duration > 0 && elapsedTime < duration)
            {
                currentTime = Time.timeSinceLevelLoad;
                elapsedTime += currentTime - lastTime;
                lastTime = currentTime;

                float value = Mathf.Lerp(from, to, elapsedTime / duration);
                tmproText.text = ((int)value).ToString();
                tmproText.text += addText;

                yield return null;
            }

            tmproText.text = to.ToString();
            tmproText.text += addText;
        }

        public static void TypingText(this TextMeshProUGUI tmproText, float duration)
        {
            tmproText.maxVisibleCharacters = 0; 
            DOTween.To(x => tmproText.maxVisibleCharacters = (int)x, 0f, tmproText.text.Length, duration);
        }

        /// <summary>
        /// ������ ��޿� ���� �ؽ�Ʈ ���� ��ȯ
        /// </summary>
        /// <param name="tmproText"></param>
        /// <param name="itemGrade"></param>
        public static void ItemGradeColor(this TextMeshProUGUI tmproText, ItemGrade itemGrade)
        {
            switch (itemGrade)
            {
                case ItemGrade.Common:
                    tmproText.text = "�Ϲ�";
                    tmproText.color = Color.white;
                    break;
                case ItemGrade.Uncommon:
                    tmproText.text = "���";
                    tmproText.color = Color.green;
                    break;
                case ItemGrade.Rare:
                    tmproText.text = "���";
                    tmproText.color = Color.magenta;
                    break;
                case ItemGrade.Legend:
                    tmproText.text = "����";
                    tmproText.color = Color.yellow;
                    break;
            }
        }
    }
}
