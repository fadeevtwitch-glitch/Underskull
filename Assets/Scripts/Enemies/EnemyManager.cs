using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float health = 30f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private string enemyType = "Basic";
    
    private float currentHealth;
    private Transform playerTransform;
    private bool isAlive = true;
    
    private void Start()
    {
        currentHealth = health;
        playerTransform = FindObjectOfType<PlayerManager>()?.transform;
    }
    
    private void Update()
    {
        if (!isAlive) return;
        
        if (playerTransform == null) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        if (distanceToPlayer < detectionRange)
        {
            ChasePlayer();
        }
    }
    
    private void ChasePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
    
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        isAlive = false;
        Destroy(gameObject);
    }
    
    public float CurrentHealth => currentHealth;
    public float MaxHealth => health;
    public string EnemyType => enemyType;
}