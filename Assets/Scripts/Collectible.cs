using UnityEngine;

public class Collectible : MonoBehaviour, IInteractable
{
    public void Interact(GameObject source)
    {
        // Pickup
        if (!SaveManager.Instance.AddCollectible(gameObject.name)) return;
        GameManager.Instance.player.StartCoroutine(GameManager.Instance.player.PickupBackflip());

        // Add to player and remove collider
        GetComponent<AudioSource>().Play();
        GameManager.Instance.AddCollectibleToPlayer(gameObject);
    }
}
