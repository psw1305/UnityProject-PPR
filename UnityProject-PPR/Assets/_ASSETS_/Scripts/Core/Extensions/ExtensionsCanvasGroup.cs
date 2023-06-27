using UnityEngine;
using DG.Tweening;

namespace PSW.Core.Extensions
{
    public static class ExtensionsCanvasGroup
    {
        public static void CanvasInit(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.transform.localPosition = new Vector3(0, 660, 0);
        }

        /// <summary>
        /// ���̵��� => ������
        /// </summary>
        /// <param name="canvasGroup"></param>
        /// <param name="duration"></param>
        /// <param name="delay"></param>
        public static void CanvasFadeInDelay(this CanvasGroup canvasGroup, float duration, float delay)
        {
            canvasGroup
                .DOFade(1, duration)
                .OnStart(() =>
                {
                    canvasGroup.alpha = 0;
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
        /// ���̵� �� => ������ �ʱ�ȭ
        /// </summary>
        /// <param name="canvasGroup"></param>
        /// <param name="duration"></param>
        public static void CanvasFadeIn(this CanvasGroup canvasGroup, float duration)
        {
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
        /// ���̵� �ƿ� => ������ ����
        /// </summary>
        /// <param name="canvasGroup"></param>
        /// <param name="duration"></param>
        /// <param name="position"></param>
        public static void CanvasFadeOut(this CanvasGroup canvasGroup, float duration)
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
                     canvasGroup.transform.localPosition = new Vector3(0, 660, 0);
                 })
                 .SetEase(Ease.OutSine);
        }
    }
}
