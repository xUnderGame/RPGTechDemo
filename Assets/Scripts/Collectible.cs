using UnityEngine;

public class Collectible : MonoBehaviour, IInteractable
{
    public void Interact(GameObject source)
    {
        SaveManager.Instance.AddCollectible(gameObject.name);
        Destroy(gameObject);
    }
}
