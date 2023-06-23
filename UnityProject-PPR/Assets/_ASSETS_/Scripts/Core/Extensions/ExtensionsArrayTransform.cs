using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace PSW.Core.Extensions
{
    public static class ExtensionsArrayTransform
    {
        public static IEnumerator FadeInCoroutine(this Transform[] imagesGroup, float fadeDuration)
        {
            for (int i = 0; i < imagesGroup.Length; i++)
            {
                imagesGroup[i].DOScale(1.05f, 0.2f).SetEase(Ease.OutSine);

                yield return new WaitForSeconds(fadeDuration / imagesGroup.Length);
            }

            yield return new WaitForSeconds(0.2f);
        }

        public static IEnumerator FadeOutCoroutine(this Transform[] imagesGroup, float fadeDuration)
        {
            for (int i = 0; i < imagesGroup.Length; i++)
            {
                imagesGroup[i].DOScale(0.0f, 0.2f).SetEase(Ease.OutSine);

                yield return new WaitForSeconds(fadeDuration / imagesGroup.Length);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
