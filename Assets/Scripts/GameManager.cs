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
    public GameObject collectiblesUI;

    void Awake()
    {
        // Only one GameManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }

        // Main references
        playerObject = GameObject.Find("Player");
        knight = playerObject.transform.Find("Knight D Pelegrini").gameObject;
        player = playerObject.GetComponent<Player>();

        // UI references
        mainUI = GameObject.Find("UI");
        collectiblesUI = mainUI.transform.Find("Collectibles").gameObject;
        pauseUI = mainUI.transform.Find("Pause").gameObject;

        // Set default slider value
        pauseUI.transform.Find("Sensitivity Slider").GetComponent<Slider>().value = player.sensitivity;
    }

    public void AddCollectibleToPlayer(GameObject collectible)
    {
        if (!collectible) return;

        collectible.GetComponent<Collider>().enabled = false;
        collectible.GetComponent<Rigidbody>().isKinematic = true;
        collectible.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        Transform slot = knight.transform.Find("Pickups").Find($"{collectible.name} Slot");
        collectible.transform.SetParent(slot);
        collectible.transform.position = slot.position;

        // Update UI
        collectiblesUI.transform.Find($"{collectible.name} UI").GetComponent<RawImage>().color
            = collectible.GetComponent<MeshRenderer>().material.color;
    }
}
