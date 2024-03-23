using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [HideInInspector] public Player player;
    public GameObject playerObject;
    public GameObject mainUI;
    public GameObject pauseUI;

    void Start()
    {
        // Only one GameManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }

        // Main references
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();

        // UI references
        mainUI = GameObject.Find("UI");
        pauseUI = mainUI.transform.Find("Pause").gameObject;

        // Set default slider value
        pauseUI.transform.Find("Sensitivity Slider").GetComponent<Slider>().value = player.sensitivity;
    }
}
