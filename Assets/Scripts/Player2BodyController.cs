using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2BodyController : MonoBehaviour
{
    private Rigidbody rb;
    private GameController gameController;
    public float speed;
    public int forceConst;
    private float moveHorizontal;
    private float moveVertical;
    private float damageTaken = 0;
    private Player1GloveController player1Glove;
    private Player2GloveController player2Glove;
    public int punchForce;
    private int numJumps;
    private bool movementEnabled;

    // Use this for initialization
    void Start()
    {
        numJumps = 0;
        rb = GetComponent<Rigidbody>();
        player1Glove = GameObject.FindObjectOfType<Player1GloveController>();
        player2Glove = GameObject.FindObjectOfType<Player2GloveController>();
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))// && rb.position.y <= 0.50001 && rb.position.y >= 0)
        {
            Jump();
        }
        if (rb.position.y <= -15)
        {
            gameController.GameOver(1);
            Destroy(gameObject);
            Destroy(player2Glove);
        }
    }
    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("P2_horizontal");
        moveVertical = Input.GetAxis("P2_vertical");
        if (movementEnabled)
        {
            rb.AddForce(moveHorizontal * speed, 0, moveVertical * speed);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision enter");
        if (collision.collider.tag == "Player1_glove")
        {
            Debug.Log("enter");
            Vector3 force = transform.position - collision.collider.transform.position;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            takeDamage(force * punchForce);
            //takeDamage(collision.rigidbody.position);
        }
    }
    public Vector3 GetMovement()
    {
        return new Vector3(moveHorizontal, 0, moveVertical);
    }
    private void Jump()
    {
        bool grounded = isGrounded();
        Debug.Log(grounded);
        if (numJumps < 1)
        {
            rb.AddForce(0, forceConst, 0, ForceMode.Impulse);
            numJumps += 1;
        }
        if (grounded)
        {
            numJumps = 0;
        }
    }
    public void takeDamage(Vector3 punchDir)
    {
        Debug.Log("damage on p2");
        if (damageTaken < 996)
        {
            damageTaken += 3.0f;
        }
        gameController.DamageP2(damageTaken);
        // rb.AddExplosionForce(damageTaken, punchDir, 5.0f, 3.0f);
        rb.AddForce(punchDir * damageTaken, ForceMode.Acceleration);
    }
    bool isGrounded()
    {
        Collider collider = GetComponent<Collider>();
        return Physics.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y + 0.1f);
    }
    public void EnableMovement()
    {
        movementEnabled = true;
    }
}
