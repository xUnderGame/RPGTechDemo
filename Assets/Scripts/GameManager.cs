using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [HideInInspector] public Player player;
    public GameObject playerObject;
    public GameObject knight;
    public GameObject mainUI;
    public GameObject pauseUI;
    public GameObject gameoverUI;
    public GameObject collectiblesUI;
    public Image hpUI;
    public Text savedTextUI;
    public AudioSource BGM;

    void Awake()
    {
        // Only one GameManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }

        // Main references
        playerObject = GameObject.Find("Player");
        knight = playerObject.transform.Find("Knight D Pelegrini").gameObject;
        player = playerObject.GetComponent<Player>();
        BGM = GetComponent<AudioSource>();

        // UI references
        mainUI = GameObject.Find("UI");
        collectiblesUI = mainUI.transform.Find("Collectibles").gameObject;
        pauseUI = mainUI.transform.Find("Pause").gameObject;
        gameoverUI = mainUI.transform.Find("Game Over").gameObject;
        savedTextUI = mainUI.transform.Find("Saved Text").GetComponent<Text>();
        hpUI = mainUI.transform.Find("HP Bar").GetComponent<Image>();
        savedTextUI.CrossFadeAlpha(0, 0, true);

        // Set default slider value
        pauseUI.transform.Find("Sensitivity Slider").GetComponent<Slider>().value = player.sensitivity;
    }

    // Adds collectible to player and enables it on the UI
    public void AddCollectibleToPlayer(GameObject collectible)
    {
        if (!collectible) return;

        // Disables stupid stuff
        collectible.GetComponent<Collider>().enabled = false;
        collectible.GetComponent<Rigidbody>().isKinematic = true;
        collectible.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        // La idea era de que los colectibles rotaran alrededor del jugador,
        // pero los quaternions han sido demasiado para mi.
        // tambien podias utilizarlos para disparar o algo.
        // al final no!
        Transform slot = knight.transform.Find("Pickups").Find($"{collectible.name} Slot");
        collectible.transform.SetParent(slot);
        collectible.transform.position = slot.position;

        // Update UI
        collectiblesUI.transform.Find($"{collectible.name} UI").GetComponent<RawImage>().color
            = collectible.GetComponent<MeshRenderer>().material.color;
    }
}
