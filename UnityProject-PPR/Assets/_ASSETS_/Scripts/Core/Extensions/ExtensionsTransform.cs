using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace PSW.Core.Extensions
{
    public static class ExtensionsTransform
    {
        public static void ScaleCoroutine(this Transform transform, Vector2 from, Vector2 to, float duration, UnityAction onComplete = null)
        {
            transform
                .DOScale(to, duration)
                .OnStart(() => 
                {
                    transform.localScale = from;
                })
                .OnComplete(() => 
                { 
                    onComplete?.Invoke(); 
                });
        }

        public static void MoveCoroutine(this Transform transform, Vector2 targetPos, float duration, UnityAction onComplete = null)
        {
            transform
                .DOMove(targetPos, duration)
                .OnComplete(() => 
                { 
                    onComplete?.Invoke(); 
                });
        }
    }
}
