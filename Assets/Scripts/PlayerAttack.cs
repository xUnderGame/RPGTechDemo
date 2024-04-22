using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) { if (other.TryGetComponent(out IHurtable hurt)) hurt.Hurt(50, transform.root.gameObject); }
}
