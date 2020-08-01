using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormsBehaviour : MonoBehaviour
{
    [SerializeField] private int Speed;
    private Vector2 velocity;
    private Rigidbody2D WormsRB2D;
    private Animator myAnimator;
    private float t = 0.0f;
    private bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        WormsRB2D = GetComponent<Rigidbody2D>();
        velocity = new Vector2(1.75f, 1.1f);
    }
    private void Move()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //infini:
        //myAnimator.SetBool("Limit", true);
        //Marcher a gauche
        WormsRB2D.velocity = new Vector2(1.0f, 0.0f);
        moving = true;
        t = 0.0f;
        if (moving)
        {
            t = t + Time.fixedDeltaTime;
            if (t > 1.0f)
            {
                Debug.Log(gameObject.transform.position.y + " : " + t);
                t = 0.0f;
            }
        }

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector2.right), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
        //myAnimator.SetBool("Limit", false);
        //Marcher a droite
    }
}
