using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) { if (other.TryGetComponent(out IHurtable hurt)) hurt.Hurt(15, transform.root.gameObject); }
}
