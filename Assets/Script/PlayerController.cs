
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private float gapDistance = 2f;

    

    private Rigidbody rb;
    private Vector3 playerMovement;
    private Vector3 enemyMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        DataStore.chaseMode = false;
    }

    private void FixedUpdate()
    {
        if (DataStore.chaseMode)
        {
            Vector3 direction = (enemyTransform.position - transform.position).normalized;

             Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
             rb.rotation = toRotation;

             Vector3 desiredPosition = enemyTransform.position - (direction * gapDistance);
             playerMovement = (desiredPosition - transform.position).normalized * speed * Time.fixedDeltaTime;
             enemyMovement = direction * speed * Time.fixedDeltaTime;

             rb.MovePosition(rb.position + playerMovement);
             enemyTransform.position += enemyMovement;


           
        }
        else
        {
            Vector3 direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                rb.rotation = toRotation;
                Vector3 movement = transform.forward * speed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + movement);
            }
        }
   
        
    }


}
