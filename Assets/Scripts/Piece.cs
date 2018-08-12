using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int letters;
    private Vector3 velocity;
    private Vector3 rapidVelocity = new Vector3(7.5f, 0, 0);
    [SerializeField] private BoxCollider bc;
    Rigidbody rb;
    GameManager gm;

    bool stayed = false;
    bool isFullyOnBoard = false;
    bool isPartiallyOnBoard = false;
    bool complete = false;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        gm = FindObjectOfType<GameManager>();

        velocity = new Vector3(2f + gm.velocityModifier, 0, 0);
        rb.velocity = velocity;
	}

    private void FixedUpdate()
    {
        if (transform.position.x - bc.size.x / 2 > gm.settings.LeftBorder * gm.settings.PixelToUnitScale)
        {
            isPartiallyOnBoard = false;
            isFullyOnBoard = true;
        }
        else if (transform.position.x + bc.size.x / 2 > gm.settings.LeftBorder * gm.settings.PixelToUnitScale) isPartiallyOnBoard = true;

        if ((isPartiallyOnBoard || (!isPartiallyOnBoard && !isFullyOnBoard)) && stayed)
        {
            Debug.Log("u died");
            gm.playing = false;
            gm.dead = true;
        }

        if (!complete && isFullyOnBoard)
        {
            complete = true;
            gm.canGenerateNewWord = true;
        }

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x + GetComponent<BoxCollider>().size.x / 2, transform.position.y), transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity))
        {
            if (hit.distance <= .1f && !hit.collider.isTrigger)
            {
                rb.velocity = Vector3.zero;
                stayed = true;
            }
            else if (hit.distance > 1.0f && stayed && (hit.collider.isTrigger || hit.collider.gameObject.tag == "borderCollider"))
            {
                rb.velocity = rapidVelocity;
            }

            if (hit.distance <= .5f && rb.velocity == rapidVelocity)
            {
                rb.velocity = velocity;
            }

        }


        Debug.DrawLine(new Vector3(transform.position.x + GetComponent<BoxCollider>().size.x / 2, transform.position.y), hit.point, Color.yellow, .1f);
    }

    public void TakeAway()
    {
        if (!complete)
        {
            complete = true;
            gm.canGenerateNewWord = true;
        }

        gm.score++;

        Destroy(gameObject);
    }
}
