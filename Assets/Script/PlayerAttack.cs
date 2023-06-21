using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackUi;
    [SerializeField] private GameObject joystick;
    [SerializeField] private EnemyControl enemyControl;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float forwardJumpMultiplier = 5f;
    [SerializeField] private GameObject enemyHeadAttachmentPoint;
    [SerializeField] private GameObject playerHead;
    [SerializeField] private float delayTime;

    private Rigidbody rb;

    void Start()
    {
        attackUi.SetActive(false);
        joystick.SetActive(true);

        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (DataStore.chaseMode == true)
        {
            attackUi.SetActive(true);
            joystick.SetActive(false);
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(delayTime);
        enemyControl.PlayerAttacked();
    }
    

   
}
