using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1BodyController : MonoBehaviour
{
    private Rigidbody rb;
    private Collider cdr;
    private GameController gameController;

    public float speed;
    public int forceConst;
    private float moveHorizontal;
    private float moveVertical;
    private float damageTaken = 0;
    private Player1GloveController player1Glove;
    private Player2GloveController player2Glove;

    private bool canJump;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cdr = GetComponent<Collider>();
        player2Glove = GameObject.FindObjectOfType<Player2GloveController>();
        player1Glove = GameObject.FindObjectOfType<Player1GloveController>();
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && rb.position.y <= 0.5 && rb.position.y >= 0)
        {
            Jump();
        }
        if (rb.position.y <= -15) {
            gameController.GameOver(1);
            Destroy(gameObject);
            Destroy(player1Glove);
        }
    }
    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("P1_horizontal");
        moveVertical = Input.GetAxis("P1_vertical");

        rb.AddForce(moveHorizontal * speed, 0, moveVertical * speed);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision enter");

        if (collision.collider.tag == "Player2_glove")
        {
            Debug.Log("enter");
            takeDamage(player2Glove.GetPunchVector());
            //takeDamage(collision.transform.position);
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
        damageTaken += 3.0f;
        if (cdr.material.bounciness < 0.95f)
        {
            cdr.material.bounciness += 0.05f;
        }
        gameController.DamageP1(damageTaken);
        rb.AddExplosionForce(damageTaken, punchDir, 5.0f, 3.0f);
    }
}
