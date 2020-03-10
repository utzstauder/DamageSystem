using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemyBase : MonoBehaviour, IDamageReceiver, IDamageModifier
{
    public int initialHp = 1;
    int currentHp;

    public DamageModInfo blockInfo;

    private void OnEnable()
    {
        currentHp = initialHp;
    }

    public void ReceiveDamage(DamageInfo damageInfo)
    {
        Debug.Log($"Received {damageInfo.amount} dmg");

        currentHp -= damageInfo.amount;

        if(currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"Enemy {gameObject.name} defeated.");
        gameObject.SetActive(false);
    }

    [ContextMenu("Apply 100 dmg")]
    void ApplyDamageDebug()
    {
        ReceiveDamage(new DamageInfo(100));
    }

    public DamageModInfo ModifyDamage(DamageInfo damageInfo)
    {
        return blockInfo;
    }
}
