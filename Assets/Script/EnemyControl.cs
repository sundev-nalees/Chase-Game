using UnityEngine;


public enum EnemyState
{
    Idle,
    Flee,
    attack
}
public class EnemyControl : MonoBehaviour
{
    [SerializeField] private float fleeSpeed = 5f;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float proximityInRange = 5f;
    [SerializeField] private float proximityOutRange = 10f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float attackDelay;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject attackPlayer;
    
    private Rigidbody rb;
    private bool attack;
    private EnemyState currentState = EnemyState.Idle;
    private float attackTimer;  

    private void Start()
    {
        attackPlayer.SetActive(false);
        attack = false;
        attackTimer = attackDelay;
    }

    private void Update()
    {
        EnemyStateChange();
    }

    private void FixedUpdate()
    {
        if (attack)
        {
            Quaternion currentRotation = transform.rotation;
            Vector3 movement = currentRotation * Vector3.back * speed * Time.deltaTime;
            transform.position += movement;
        }
        
    }

    private void EnemyStateChange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (currentState == EnemyState.Idle)
        {
            if (distanceToPlayer <= proximityInRange)
            {
                currentState = EnemyState.Flee;
                Debug.Log("Flee");
                FleeFromPlayer();
            }
        }
        else if (currentState == EnemyState.Flee)
        {
            if (distanceToPlayer > proximityOutRange)
            {
                currentState = EnemyState.Idle;
                Debug.Log("Idel");
                attackTimer = attackDelay;
            }
            else
            {
                FleeFromPlayer();
            }
        }
    }

    private void FleeFromPlayer()
    {
        Vector3 fleeDirection = transform.position - playerTransform.position;
        Vector3 fleeTarget = transform.position + fleeDirection.normalized * fleeSpeed * Time.deltaTime;
        transform.position = fleeTarget;
        transform.LookAt(playerTransform);

        attackTimer -= Time.fixedDeltaTime;
        Debug.Log("AttackTime=" + attackTimer);
        if (attackTimer <= 0f)
        {
            currentState = EnemyState.attack;
            DataStore.chaseMode = true;
        }
    }

    public void PlayerAttacked()
    {
        attack = true;
        player.SetActive(false);
        attackPlayer.SetActive(true);
    }

}
