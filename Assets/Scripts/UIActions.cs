using UnityEngine;

public class UIActions : MonoBehaviour
{
    public void UpdateSensitvitySlider(float value)
    {
        if (!GameManager.Instance) return;

        GameManager.Instance.player.sensitivity = value;
    }

    public void CallRespawn()
    {
        if (!SaveManager.Instance || !GameManager.Instance) return;

        SaveManager.Instance.RespawnPlayer();
        GameManager.Instance.gameoverUI.SetActive(false);
    }
}
