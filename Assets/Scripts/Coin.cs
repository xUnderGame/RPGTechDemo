using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted");
        Destroy(gameObject);
    }
}
