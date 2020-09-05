using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormsBehaviour : MonoBehaviour
{
    [SerializeField] private int Speed;
    [SerializeField] LayerMask layerMask;
    private Vector2 velocity;
    private Rigidbody2D WormsRB2D;
    private Animator myAnimator;
    //Raycast
    private float t = 0.0f;
    //Bool
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
        //Test Raycast
        /*
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

            RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector2.down));
            // Does the ray intersect any objects excluding the player layer
            if (hit.collider != null)
            {
                Debug.DrawRay(transform.position, (Vector2.down) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, (Vector2.down) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
            //myAnimator.SetBool("Limit", false);
            //Marcher a droite
        */
    }
}
