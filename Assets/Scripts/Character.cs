using UnityEngine;

public abstract class Character : MonoBehaviour, IHurtable
{
    private GameObject healthbar;

    private void Awake()
    {
        healthbar = transform.Find("Healthbar").gameObject;
    }

    public abstract void Hurt(int damage, GameObject damageSource);
}
