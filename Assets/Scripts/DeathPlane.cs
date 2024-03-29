using UnityEngine;

public class DeathPlane : MonoBehaviour, IInteractable
{
    public void Interact(GameObject source) { StartCoroutine(GameManager.Instance.player.Kill()); }
}
