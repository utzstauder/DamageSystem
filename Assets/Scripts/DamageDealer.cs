using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public DamageInfo damageInfo;
    public Transform raycastOriginTransform;
    public Vector3 RaycastOrigin => (raycastOriginTransform == null) ? transform.position : raycastOriginTransform.position;
    public float MaxDistance => (damageInfo.damageFalloff) ? damageInfo.maxDistance : float.PositiveInfinity;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = new Ray(RaycastOrigin, transform.forward);
        DamageInfo damage = damageInfo;


        if (damageInfo.penetrate)
        {
            RaycastHit[] hits = Physics.RaycastAll(ray, MaxDistance);
            // if (hits.Length > 0) Debug.Log($"Hit {hits.Length} objects");

            // sort by distance
            Array.Sort(
                hits,
                delegate (RaycastHit a, RaycastHit b) {
                        return Vector3.Distance(RaycastOrigin, a.point).CompareTo(Vector3.Distance(RaycastOrigin, b.point));
                    }
                );

            for (int i = 0; i < hits.Length; i++)
            {
                var blocker = hits[i].collider.GetComponent<IDamageModifier>();
                DamageModInfo blockInfo = new DamageModInfo();

                if (blocker != null)
                {
                    // TODO: what happens if damage.amount <= 0?
                    blockInfo = blocker.ModifyDamage(damage);
                }

                // Debug.Log(Vector3.Distance(RaycastOrigin, hits[i].point));

                if (damage.damageFalloff)
                {
                    float normalizedDistanceToTarget = Vector3.Distance(transform.position, hits[i].point) / damage.maxDistance;
                    damage.amount = (int)(damage.falloffCurve.Evaluate(normalizedDistanceToTarget) * damage.amount);
                }

                if (damage.amount <= 0) break;

                hits[i].collider.GetComponent<IDamageReceiver>()?.ReceiveDamage(damage);

                if (blocker != null)
                {
                    damage.amount = (int)(damage.amount * blockInfo.damageMultiplier);
                }
            }
        } else
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, MaxDistance))
            {
                // Debug.Log($"Hit {hitInfo.collider.gameObject.name} at {hitInfo.point}");
                if (damage.damageFalloff)
                {
                    float normalizedDistanceToTarget = Vector3.Distance(transform.position, hitInfo.point) / damage.maxDistance;
                    damage.amount = (int)(damage.falloffCurve.Evaluate(normalizedDistanceToTarget) * damage.amount);
                }
                hitInfo.collider.GetComponent<IDamageReceiver>()?.ReceiveDamage(damage);
            }
        }

    }
}
