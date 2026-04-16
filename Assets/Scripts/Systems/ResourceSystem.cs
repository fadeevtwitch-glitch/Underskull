using UnityEngine;
using System.Collections.Generic;

public class ResourceSystem : MonoBehaviour
{
    public static ResourceSystem Instance { get; private set; }
    
    [SerializeField] private float startingResources = 100f;
    
    private float currentResources;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start()
    {
        currentResources = startingResources;
    }
    
    public bool SpendResources(float amount)
    {
        if (currentResources >= amount)
        {
            currentResources -= amount;
            return true;
        }
        return false;
    }
    
    public void AddResources(float amount)
    {
        currentResources += amount;
    }
    
    public void AddToInventory(string itemName, int quantity)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += quantity;
        }
        else
        {
            inventory[itemName] = quantity;
        }
    }
    
    public int GetInventoryCount(string itemName)
    {
        return inventory.ContainsKey(itemName) ? inventory[itemName] : 0;
    }
    
    public float CurrentResources => currentResources;
    public Dictionary<string, int> Inventory => inventory;
}