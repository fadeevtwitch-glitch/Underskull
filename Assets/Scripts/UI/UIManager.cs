using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI resourcesText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image healthBar;
    
    private PlayerManager playerManager;
    private ResourceSystem resourceSystem;
    private GameManager gameManager;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        resourceSystem = FindObjectOfType<ResourceSystem>();
        gameManager = FindObjectOfType<GameManager>();
    }
    
    private void Update()
    {
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (playerManager != null)
        {
            healthText.text = $"Health: {playerManager.CurrentHealth:F0}/{playerManager.MaxHealth:F0}";
            healthBar.fillAmount = playerManager.CurrentHealth / playerManager.MaxHealth;
        }
        
        if (resourceSystem != null)
        {
            resourcesText.text = $"Resources: {resourceSystem.CurrentResources:F0}";
        }
        
        if (gameManager != null)
        {
            int minutes = (int)gameManager.TotalPlayTime / 60;
            int seconds = (int)gameManager.TotalPlayTime % 60;
            timeText.text = $"Time: {minutes:D2}:{seconds:D2}";
        }
    }
}