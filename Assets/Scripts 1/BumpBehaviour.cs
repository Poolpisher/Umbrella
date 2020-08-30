using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    private Animator myAnimator;
    public float BumpValue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //Lance le IEnumerator Cooldown en public
    public void LauchCooldown()
    {
        StartCoroutine(Cooldown());
    }
    //Créer le IEnumerator Cooldown en privé
    private IEnumerator Cooldown()
    {
        //désactive la hitbox d'un ennemi
        var timer = 0;
        GetComponent<Collider2D>().enabled = false;
        while (timer != 10)
        {
            yield return null;
            timer++;
        }
        GetComponent<Collider2D>().enabled = true;
    }

    //Sur collision entre l'ennemi et un Collider 2D
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Collsion avec le sol
        if (ground == (ground | (1 << other.gameObject.layer)) && other.contacts[0].normal.y >= 0.9f)
        {
            myAnimator.SetBool("Bump", true);
        }
        else
        {
            ///Erreur régler : Object reference not set an instance of an object
            myAnimator.SetBool("Bump", true);
            myAnimator.SetBool("Bump", false);
        }
    }
}
