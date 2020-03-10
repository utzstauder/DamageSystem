using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DamageModInfo
{
    public float damageMultiplier;
    // TODO: add damage type modifier

    public DamageModInfo(float damageMultiplier = 1f)
    {
        this.damageMultiplier = damageMultiplier;
    }
}

public interface IDamageModifier
{
    DamageModInfo ModifyDamage(DamageInfo damageInfo);
}
