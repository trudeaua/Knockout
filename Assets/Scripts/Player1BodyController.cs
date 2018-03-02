using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1BodyController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public int forceConst;
    private float moveHorizontal;
    private float moveVertical;
    private int damageTaken = 1;
    private Player2GloveController player2Glove;

    private bool canJump;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player2Glove = GameObject.FindObjectOfType<Player2GloveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && rb.position.y <= 0.5 && rb.position.y >= 0)
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("P1_horizontal");
        moveVertical = Input.GetAxis("P1_vertical");

        rb.AddForce(moveHorizontal * speed, 0, moveVertical * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player2_glove")
        {
            Debug.Log("enter");
            takeDamage(player2Glove.GetPunchVector());
        }
    }
    public Vector3 GetMovement()
    {
        return new Vector3(moveHorizontal, 0, moveVertical);
    }
    private void Jump()
    {
        rb.AddForce(0, forceConst, 0, ForceMode.Impulse);
    }

    public void takeDamage(Vector3 punchDir)
    {
        Debug.Log("damage on p1");
        damageTaken += 3;
    }
}
