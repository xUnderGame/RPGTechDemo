using UnityEngine;

public abstract class Character : MonoBehaviour, IHurtable
{
    public int maxHP;
    public int currentHP;

    public void Awake()
    {
        maxHP = 100;
        currentHP = 100;
    }

    public abstract void Hurt(int damage, GameObject damageSource);
}
