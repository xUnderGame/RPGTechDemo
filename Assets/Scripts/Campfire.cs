using UnityEngine;

public class Campfire : MonoBehaviour, IInteractable
{
    public void Interact(GameObject source)
    {
        SaveManager.Instance.SaveDataJSON(null, true);
        Debug.Log("Saved!");
    }
}
