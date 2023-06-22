using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackUi;
    [SerializeField] private GameObject joystick;
    [SerializeField] private EnemyControl enemyControl;
    [SerializeField] private GameObject playerHead;
    [SerializeField] private float delayTime;

    [SerializeField] private Transform targetTransform; 
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDuration;

    private Vector3 initialPosition;
    private float elapsedTime;
    private bool isJumping = false;

    void Start()
    {
        attackUi.SetActive(false);
        joystick.SetActive(true);
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
        initialPosition = transform.position;
        StartCoroutine(Jumped());
        StartCoroutine(Attack());
    }
    private IEnumerator Jumped()
    {
        isJumping = true;
        elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            float t = elapsedTime / jumpDuration;
            float jumpCurve = Mathf.Sin(t * Mathf.PI); 

            transform.position = Vector3.Lerp(initialPosition, targetTransform.position, jumpCurve * jumpHeight);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetTransform.position; 
        isJumping = false;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(delayTime);
        enemyControl.PlayerAttacked();
    }
    

   
}
