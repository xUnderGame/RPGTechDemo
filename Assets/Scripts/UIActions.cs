using UnityEngine;

public class UIActions : MonoBehaviour
{
    public void UpdateSensitvitySlider(float value)
    {
        if (!GameManager.Instance) return;

        GameManager.Instance.player.sensitivity = value;
    }
}
