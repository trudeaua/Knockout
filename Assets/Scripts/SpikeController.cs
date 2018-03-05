using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{

    private Player1BodyController player1BodyController;
    private Player2BodyController player2BodyController;
    public int magnitude;
    // Use this for initialization
    void Start()
    {
        player1BodyController = FindObjectOfType<Player1BodyController>();
        player2BodyController = FindObjectOfType<Player2BodyController>();
    }
    void OnCollisionEnter(Collision collision)
    {
        Vector3 force = CalcSpikeDamage(collision);
        force.y = 1;
        AudioSource audio = GetComponent<AudioSource>();
        switch (collision.collider.tag)
        {
            case "Player1_body":
                player1BodyController.takeDamage(force * magnitude);
                audio.Play();
                break;
            case "Player2_body":
                player2BodyController.takeDamage(force * magnitude);
                audio.Play();
                break;
        }
    }

    Vector3 CalcSpikeDamage(Collision collision)
    {
        Vector3 force = collision.collider.transform.position - transform.position;
        force.Normalize();
        return force;
    }
}
