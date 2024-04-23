using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy self;
    private Coroutine attackCoro = null;

    private void Awake()
    {
        self = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player") && self.currentHP > self.maxHP / 2) attackCoro = StartCoroutine(Attack(other)); }
    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player") && attackCoro != null) StopCoroutine(attackCoro); }

    private IEnumerator Attack(Collider collider)
    {
        while (true)
        {
            if (collider.TryGetComponent(out IHurtable hurt)) hurt.Hurt(15, transform.root.gameObject);
            yield return new WaitForSeconds(1.25f);
        }
    }
}
