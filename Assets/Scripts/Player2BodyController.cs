using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2BodyController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public int forceConst;
    private float moveHorizontal;
    private float moveVertical;
    private int damageTaken = 0;
    private Player1GloveController player1Glove;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player1Glove = GameObject.FindObjectOfType<Player1GloveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock) && rb.position.y <= 0.5 && rb.position.y >= 0)
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("P2_horizontal");
        moveVertical = Input.GetAxis("P2_vertical");

        rb.AddForce(moveHorizontal * speed, 0, moveVertical * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1_glove")
        {
            Debug.Log("enter");
            takeDamage(player1Glove.GetPunchVector());
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
        Debug.Log("damage on p2");
        damageTaken += 3;
        rb.AddForce(punchDir.x * damageTaken, 0, punchDir.z * damageTaken);
    }
}
