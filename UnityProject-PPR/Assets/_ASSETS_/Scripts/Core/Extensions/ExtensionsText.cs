using PSW.Core.Enums;
using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace PSW.Core.Extensions
{
    public static class ExtensionsText
    {
        /// <summary>
        /// 주어진 from 에서 to 까지 point text 숫자 카운팅 애니메이션
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
        /// 아이템 등급에 따른 텍스트 색깔 변환
        /// </summary>
        /// <param name="tmproText"></param>
        /// <param name="itemGrade"></param>
        public static void TextColorFromItemGrade(this TextMeshProUGUI tmproText, ItemGradeType itemGrade)
        {
            switch (itemGrade)
            {
                case ItemGradeType.Common:
                    tmproText.text = "일반";
                    tmproText.color = Color.white;
                    break;
                case ItemGradeType.Uncommon:
                    tmproText.text = "고급";
                    tmproText.color = Color.green;
                    break;
                case ItemGradeType.Rare:
                    tmproText.text = "희귀";
                    tmproText.color = Color.magenta;
                    break;
                case ItemGradeType.Legend:
                    tmproText.text = "전설";
                    tmproText.color = Color.yellow;
                    break;
            }
        }

        #region Floating Damage Text

        public static void FloatingDamageText(this TextMeshProUGUI tmproText, string damageText)
        {
            tmproText.text = damageText;
            TextSequence(tmproText);
        }

        private static Sequence TextSequence(TextMeshProUGUI tmproText)
        {
            return DOTween.Sequence()
                .SetAutoKill(false)
                .OnStart
                    (() =>
                    {
                        tmproText.color = new Color32(228, 59, 68, 255);
                        tmproText.transform.localPosition = new Vector2(0, 0);
                    })
                .Append(tmproText.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.25f))
                .Join(tmproText.transform.DOLocalMoveY(15, 0.25f))
                .Append(tmproText.transform.DOScale(Vector3.one, 0.25f).SetDelay(0.25f))
                .Join(tmproText.DOFade(0, 0.25f).SetDelay(0.25f))
                .OnComplete(() => tmproText.gameObject.SetActive(false));
        }
        #endregion
    }
}
