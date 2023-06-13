using System.Collections;
using UnityEngine;

namespace PSW.Core.Extensions
{
    public static class ExtensionsRectTransform
    {
        public static IEnumerator ShakeCoroutine(this RectTransform rectTransform, float force, float amounts)
        {
            float shakeTime = 0.3f;
            Vector3 originPos = rectTransform.localPosition;
            
            for (int i = 0; i < amounts; i++)
            {
                Vector3 newPos = Random.insideUnitSphere * force + originPos;
                newPos.z = rectTransform.localPosition.z;
                rectTransform.localPosition = newPos;

                yield return new WaitForSeconds(shakeTime / amounts);
            }

            rectTransform.localPosition = originPos;
        }
    }
}
