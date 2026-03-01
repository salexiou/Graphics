using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

    public GameObject inv;
    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject temporaryDisplayPanel;
    public TMP_Text temporaryDisplayName;
    public RawImage temporaryDisplayIcon;

    public GameObject panelTime;
    public GameObject panelUsage;

    private Item currentItem;

    public AudioSource timeSnd;

    private Button button1;
    private Button button2;
    public List<Image> heartContainers; 
    public HealthEffect health; 
    public CinemachineVirtualCamera cam_3d;
    public CinemachineVirtualCamera map_Cam;
    public GameObject trees;

    private void Awake()
    {
        Instance = this;
        inv.SetActive(false);
        temporaryDisplayPanel.SetActive(false);
        panelUsage.SetActive(false);

        button1 = panelUsage.transform.Find("Button").GetComponent<Button>();
        button2 = panelUsage.transform.Find("Button (1)").GetComponent<Button>();
        if(map_Cam.Priority > 0 && map_Cam != null){
            map_Cam.Priority = 0;
        }
    }

    public void Add(Item item)
    {
        Item existingItem = items.Find(i => i.itemName == item.itemName);

        if (existingItem != null)
        {
            existingItem.value += 1;
        }
        else
        {
            items.Add(item);
            item.value = 1;
        }
        ListItems();        
    }

    public void Remove(Item item)
    {
        Item existingItem = items.Find(i => i.itemName == item.itemName);
        if (existingItem != null)
        {
            existingItem.value -= 1;

            if (existingItem.value <= 0)
            {
                items.Remove(existingItem);
            }
        }
        ListItems();
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemImage").GetComponent<RawImage>();
            var itemBtn = obj.transform.Find("QuantityBtn").GetComponent<Button>(); 
            var itemBtnTxt = itemBtn.GetComponentInChildren<TMP_Text>(); 

            if (itemName == null || itemIcon == null || itemBtn == null || itemBtnTxt == null)
            {
                return;
            }

            itemName.text = item.itemName;
            itemIcon.texture = item.Icon;
            itemBtnTxt.text = item.value.ToString();

            obj.GetComponent<Button>().onClick.AddListener(() => HandleItems(item));
        }
    }

   public void HandleItems(Item item)
{
    if (item.id == 1 || item.id == 2 || (item.id >= 6 && item.id <= 11))
    {
        temporaryDisplayName.text = item.itemName;
        temporaryDisplayIcon.texture = item.Icon;
        temporaryDisplayPanel.SetActive(true);
        StartCoroutine(HideItemAfterDelay(2.0f));
    }
    else if (item.id == 3 || item.id == 4 || item.id == 5 || item.id == 12)
    {
        temporaryDisplayName.text = item.itemName;
        temporaryDisplayIcon.texture = item.Icon;
        temporaryDisplayPanel.SetActive(true);
        panelUsage.SetActive(true);

        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        button1.onClick.AddListener(() => OnButton1Click(item));
        button2.onClick.AddListener(() => OnButton2Click());
    }
}

void OnButton1Click(Item item)
{
    if (item.id == 5 && item.value > 0) 
    {
        Image firstInactiveHeart = heartContainers.Find(h => !h.gameObject.activeSelf);

        if (firstInactiveHeart != null)
        {
            firstInactiveHeart.gameObject.SetActive(true);
            health.TriggerHealthEffect();
            Remove(item);

            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            playerHealth.RecoverHealth();
        }
    }
    else if (item.id == 3 && item.value > 0) 
    {
        Timer timerScript = panelTime.GetComponent<Timer>();
        timerScript.timeRemaining += 300;
        timeSnd.Play();
        Remove(item);
    }
    else if (item.id == 4 && item.value > 0)
    {
        PlayerMovement1 playerMovement = FindObjectOfType<PlayerMovement1>();
        playerMovement.IncreaseAttackDamage(5);
        PowerEffect powerEffect = FindObjectOfType<PowerEffect>();
        powerEffect.TriggerDamageEffect();
         
        Remove(item);

    }
    else if (item.id == 12 && item.value > 0) 
    {
        if (cam_3d.Priority == 10)
        {
            map_Cam.Priority = 10;
            cam_3d.Priority = 0;
            trees.SetActive(false);
            StartCoroutine(ChangeCamera(4f));
        }
    }

    ListItems();
    StartCoroutine(HideItemAfterDelay(1.0f));
}


    void OnButton2Click()
    {
        StartCoroutine(HideItemAfterDelay(1.0f));
    }

    private IEnumerator HideItemAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        temporaryDisplayPanel.SetActive(false);

        if (panelUsage.activeSelf)
        {
            panelUsage.SetActive(false);
        }
    }

    private IEnumerator ChangeCamera(float delay)
    {
        yield return new WaitForSeconds(delay);
            if(map_Cam.Priority == 10)
            {
                map_Cam.Priority = 0;
                cam_3d.Priority = 10;
            }
            trees.SetActive(true);
    }
}
