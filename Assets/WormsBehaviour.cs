using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormsBehaviour : MonoBehaviour
{
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Move()
    {
    //infini:
        myAnimator.SetBool("Limit", true);
        //Marcher a gauche
        myAnimator.SetBool("Limit", false);
        //Marcher a droite
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
