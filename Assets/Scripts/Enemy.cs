using UnityEngine;

public class Enemy : Character
{
    public override void Attack() { }

    public override void Hurt(int damage, GameObject damageSource)
    {
        currentHP -= damage;
        if (currentHP <= 0) Destroy(gameObject);
    }
}
