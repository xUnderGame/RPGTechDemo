using System.Collections;
using UnityEngine;

public class Campfire : MonoBehaviour, IInteractable
{
    public void Interact(GameObject source)
    {
        SaveManager.Instance.SaveDataJSON(null, true);
        GameManager.Instance.savedTextUI.CrossFadeAlpha(1, 0, true);
        GameManager.Instance.savedTextUI.CrossFadeAlpha(0, 1.5f, true);
    }
}
