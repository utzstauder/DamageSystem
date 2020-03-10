using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DamageInfo
{
    public int amount;
    public bool penetrate;
    public bool damageFalloff;
    public float maxDistance;
    public AnimationCurve falloffCurve;

    public DamageInfo(
        int amount = 1,
        bool penetrate = false,
        bool damageFalloff = false,
        float maxDistance = float.MaxValue,
        AnimationCurve falloffCurve = default(AnimationCurve)
        )
    {
        this.amount = amount;
        this.penetrate = penetrate;
        this.damageFalloff = damageFalloff;
        this.maxDistance = maxDistance;
        this.falloffCurve = falloffCurve;
    }
}

public interface IDamageReceiver
{
    void ReceiveDamage(DamageInfo damageInfo);
}
