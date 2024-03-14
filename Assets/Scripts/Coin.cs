using System.Linq;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SaveManager.Instance.userData.collectibles.Append(gameObject.name);
        Debug.Log(gameObject.name);
        Destroy(gameObject);
    }
}
