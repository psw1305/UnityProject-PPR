namespace PSW.Core.Extensions
{
    using System.Collections;
    using UnityEngine;
    using TMPro;

    public static class ExtensionsText
    {
        /// <summary>
        /// �־��� from ���� to ���� point text ���� ī���� �ִϸ��̼�
        /// </summary>
        /// <param name="tmproText"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static IEnumerator UpdateTextCoroutine(this TextMeshProUGUI tmproText, int from, int to, float time, string addText = "")
        {
            float currentTime = Time.timeSinceLevelLoad;
            float elapsedTime = 0.0f;
            float lastTime = currentTime;

            while (time > 0 && elapsedTime < time)
            {
                currentTime = Time.timeSinceLevelLoad;
                elapsedTime += currentTime - lastTime;
                lastTime = currentTime;

                float value = Mathf.Lerp(from, to, elapsedTime / time);
                tmproText.text = ((int)value).ToString();
                tmproText.text += addText;

                yield return null;
            }

            tmproText.text = to.ToString();
            tmproText.text += addText;
        }
    }
}
