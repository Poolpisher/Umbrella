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
        velocity = new Vector2(1.75f, 1.1f);
    }
    private void Move()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //infini:
        //myAnimator.SetBool("Limit", true);
        //Marcher a gauche
        WormsRB2D.velocity = new Vector2(-1.0f, 0.0f);
        moving = true;
        t = 0.0f;
        if (moving)
        {
            t = t + Time.deltaTime;
            if (t > 1.0f)
            {
                Debug.Log(gameObject.transform.position.y + " : " + t);
                t = 0.0f;
            }
        }
        //var V2 = new Vector2();
        //WormsRB2D.AddForce(V2 * Speed);
        //myAnimator.SetBool("Limit", false);
        //Marcher a droite

    }
}
