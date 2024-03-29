using UnityEngine;

public class Campfire : MonoBehaviour, IInteractable
{
    private AudioSource sfx;
    
    void Start() { sfx = GetComponent<AudioSource>(); }

    public void Interact(GameObject source)
    {
        SaveManager.Instance.SaveDataJSON(null, true);
        GameManager.Instance.savedTextUI.CrossFadeAlpha(1, 0, true);
        GameManager.Instance.savedTextUI.CrossFadeAlpha(0, 1.5f, true);
        sfx.PlayOneShot(sfx.clip);
    }
}
