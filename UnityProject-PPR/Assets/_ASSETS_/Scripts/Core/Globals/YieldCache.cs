using System.Collections.Generic;
using UnityEngine;

public static class YieldCache
{
    /// <summary>
    /// Boxing �߻����� �ʰ� ���ָ�, �ǵ�ġ �ʰ� �������� �����Ǵ� ���� ����
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
    /// �ڷ�ƾ Yield WaitForSeconds ����ȭ
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
