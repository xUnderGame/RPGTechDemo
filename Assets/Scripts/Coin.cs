using System.Linq;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SaveManager.Instance.AddCollectible(gameObject.name);
        Destroy(gameObject);
    }
}
