using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Inventory inventory;
    public InventoryUI inventoryUI;

    public GameObject sun;
    public Light sunLight;

    public float dayHours = 24f;
    public float currentDayTime = 0;

    public float sunSpeed = 5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        sunLight = sun.GetComponent<Light>();
    }

    private void Update()
    {
        sun.transform.Rotate(sunSpeed * Time.deltaTime, 0, 0);
    }

    public void SetInventory(Inventory newInventory)
    {
        inventory = newInventory;
    }
    public void UpdateInventory()
    {
        inventoryUI.UpdateInventory(inventory);
    }
}