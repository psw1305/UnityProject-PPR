using System.Collections.Generic;
using UnityEngine;

public static class YieldCache
{
    /// <summary>
    /// Boxing 발생하지 않게 해주며, 의도치 않게 가비지가 생성되는 것을 방지
    /// </summary>
    class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals(float x, float y)
        {
            return x == y;
        }
        int IEqualityComparer<float>.GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }

    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new();

    private static readonly Dictionary<float, WaitForSeconds> _timeInterval = new();

    /// <summary>
    /// 코루틴 Yield WaitForSeconds 최적화
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        if (!_timeInterval.TryGetValue(seconds, out WaitForSeconds waitForSeconds))
            _timeInterval.Add(seconds, waitForSeconds = new WaitForSeconds(seconds));
        return waitForSeconds;
    }
}
