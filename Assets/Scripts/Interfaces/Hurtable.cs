using UnityEngine;

public interface IHurtable
{
    public abstract void Hurt(int damage, GameObject damageSource);
}