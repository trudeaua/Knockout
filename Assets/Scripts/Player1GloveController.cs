using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1GloveController : MonoBehaviour
{

    // private Rigidbody rb;
    public Vector3 offset;

    private Player1BodyController player1BodyCtrl;
    private Player2BodyController player2BodyCtrl;
    private GameObject player1Body;
    private bool punching = false;
    private Vector3 oldMovement;
    private Vector3 punchVector;
    // Use this for initialization
    void Start()
    {
        player1BodyCtrl = GameObject.FindObjectOfType<Player1BodyController>();
        player2BodyCtrl = GameObject.FindObjectOfType<Player2BodyController>();

        player1Body = GameObject.FindGameObjectWithTag("Player1_body");
        // rb = GetComponent<Rigidbody>();
        if (player1BodyCtrl == null)
        {
            Debug.Log("Cannot find controller");
        }
    }
    void Update()
    {
        if (!punching && Input.GetKeyDown(KeyCode.RightShift))
        {
            StartCoroutine(Punch(0.1f, 1.25f, transform.forward));
        }
    }
    void FixedUpdate()
    {
        Vector3 movement = player1BodyCtrl.GetMovement();

        if (movement.magnitude != 0)
        {
            //set position in direction ball is travelling
            transform.position = player1Body.transform.position +
                new Vector3(movement.normalized.x * offset.x, 0, movement.normalized.z * offset.z);
            oldMovement = movement;
            //set rotation to direction ball is travelling
            transform.rotation = Quaternion.LookRotation(movement);
        }
        else
        {
            //set position to previous direction travelled if no movement, same for rotation
            transform.position = player1Body.transform.position +
                new Vector3(oldMovement.normalized.x * offset.x, 0, oldMovement.normalized.z * offset.z);
            // rb.transform.rotation = Quaternion.LookRotation(oldMovement);
        }

        // rb.transform.Translate(movement * 20 * Time.deltaTime, Space.World);
    }
    //got Punch() from https://answers.unity.com/questions/737209/punching-objects.html
    IEnumerator Punch(float time, float distance, Vector3 direction)
    {
        punching = true;
        var timer = 0.0f;
        var orgPos = transform.position;
        Collider c = GetComponent<Collider>();
        c.transform.position = orgPos;
        direction.Normalize();
        while (timer <= time)
        {
            transform.position = orgPos + (Mathf.Sin(timer / time * Mathf.PI) + 1.0f) * direction;
            c.transform.position = transform.position;
            yield return null;
            timer += Time.deltaTime;
        }
        punchVector = transform.position - orgPos;
        transform.position = orgPos;
        punching = false;
    }
    public Vector3 GetPunchVector() {
        return punchVector;
    }
}
