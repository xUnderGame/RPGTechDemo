using UnityEngine;

public abstract class Character : MonoBehaviour, IHurtable
{
    public GameObject healthbar;
    public int maxHP;
    public int currentHP;

    private void Awake()
    {
        //healthbar = transform.Find("Healthbar").gameObject;
        maxHP = 100;
        currentHP = maxHP;
    }

    public abstract void Attack();

    public abstract void Hurt(int damage, GameObject damageSource);
}
