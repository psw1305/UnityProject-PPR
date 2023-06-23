using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace PSW.Core.Extensions
{
    public static class ExtensionsCanvasGroup
    {
        /// <summary>
        /// 페이드 인 => 딜레이 추가
        /// </summary>
        /// <param name="canvasGroup"></param>
        /// <param name="duration"></param>
        /// <param name="delay"></param>
        public static void CanvasFadeIn(this CanvasGroup canvasGroup, float duration, float delay)
        {
            if (canvasGroup.alpha != 0) canvasGroup.alpha = 0;

            canvasGroup
                .DOFade(1, duration)
                .OnStart(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                })
                .OnComplete(() =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                })
                .SetEase(Ease.OutSine)
                .SetDelay(delay);
        }

        /// <summary>
        /// 페이드 인 => 포지션 초기화
        /// </summary>
        /// <param name="canvasGroup"></param>
        /// <param name="duration"></param>
        public static void CanvasFadeIn(this CanvasGroup canvasGroup, float duration)
        {
            if (canvasGroup.alpha != 0) canvasGroup.alpha = 0;

            canvasGroup
                .DOFade(1, duration)
                .OnStart(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                    canvasGroup.transform.localPosition = Vector3.zero;
                })
                .OnComplete(() =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                })
                .SetEase(Ease.OutSine);
        }

        /// <summary>
        /// 페이드 아웃 => 포지션 세팅
        /// </summary>
        /// <param name="canvasGroup"></param>
        /// <param name="duration"></param>
        /// <param name="position"></param>
        public static void CanvasFadeOut(this CanvasGroup canvasGroup, float duration, Vector3 position)
        {
            canvasGroup
                 .DOFade(0, duration)
                 .OnStart(() =>
                 {
                     canvasGroup.interactable = false;
                     canvasGroup.blocksRaycasts = false;
                 })
                 .OnComplete(() =>
                 {
                     canvasGroup.interactable = true;
                     canvasGroup.blocksRaycasts = true;
                     canvasGroup.transform.localPosition = position;
                 })
                 .SetEase(Ease.OutSine);
        }
    }
}
