using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackDamage = 10f;
    
    private float lastAttackTime = 0f;
    
    public bool TryAttack(Vector3 targetPosition)
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return false;
        
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance > attackRange)
            return false;
        
        lastAttackTime = Time.time;
        PerformAttack(targetPosition);
        return true;
    }
    
    private void PerformAttack(Vector3 targetPosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(targetPosition, attackRange);
        
        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyManager enemy = hit.GetComponent<EnemyManager>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                }
            }
        }
    }
    
    public float AttackDamage => attackDamage;
    public float AttackRange => attackRange;
    public float AttackCooldown => attackCooldown;
}