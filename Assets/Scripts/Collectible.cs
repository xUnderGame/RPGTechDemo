using UnityEngine;

public class Collectible : MonoBehaviour, IInteractable
{
    public void Interact(GameObject source)
    {
        if (!SaveManager.Instance.AddCollectible(gameObject.name)) return;
        Destroy(gameObject);
    }
}
