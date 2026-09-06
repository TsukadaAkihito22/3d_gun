using UnityEngine;

public struct DamageData
{
    public float Damage;
    public Vector3 HitPoint;
    public HitPartTypes HitPartTypes;

    public DamageData(float damage, Vector3 hitPoint, HitPartTypes hitPartTypes)
    {
        Damage = damage;
        HitPoint = hitPoint;
        HitPartTypes = hitPartTypes;
    }
}
