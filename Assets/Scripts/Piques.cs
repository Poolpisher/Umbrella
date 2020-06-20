using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piques : MonoBehaviour
{
    [SerializeField] private int force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return;
        }
        if (collision.gameObject.CompareTag("Umbrella"))
        {
            //Faire rebondir le joueur
            collision.gameObject.GetComponent<Umbrella>().Jump(force);
        }
        else
        {
            //tuer le joueur
            collision.gameObject.GetComponent<RobotBoyBehaviour>().Kill();
        }
    }
}