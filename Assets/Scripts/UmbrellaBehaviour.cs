using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UmbrellaBehaviour : MonoBehaviour
{
    [SerializeField] private int ShootBumpValue;
    //InputAction
    private Player playerInput;
    private Vector2 inputValue;
    //RigidBody
    private Rigidbody2D FOX;
    //Bool
    private bool CanJump;
    public bool CanShoot;
    //HUD
    private GameObject Bullet;
    private void OnEnable()
    {
        //Activation des input du joueur
        playerInput = new Player();
        playerInput.Enable();
        playerInput.Main.Umbrella.performed += Move;
        playerInput.Main.Umbrella.canceled += Stop;
        playerInput.Main.Shoot.performed += Shoot;
    }
    //Input action
        //Fonction tir
        private void Shoot(InputAction.CallbackContext obj)
        {
            //Valeur opposé de la direction du parapluie
            var NinputValue = inputValue * -1;
            if (CanShoot == true)
            {
                CanShoot = false;
                //Déplace le joueur
                FOX.AddForce(NinputValue * ShootBumpValue, ForceMode2D.Impulse);
                //Désactive la balle dans le HUD
                Bullet.SetActive(false);
                    //Tir: (à faire)
                /*
                //var path = inputValue;
                RaycastHit2D hit = Physics2D.Raycast(inputValue, (Vector2.up));
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
                */
            }
        }
        //Déplacement du parapluie: Stick droit/flèches
        private void Move(InputAction.CallbackContext obj)
            {
                inputValue = obj.ReadValue<Vector2>();
            }
        //Retour du parapluie à sa position initial
        private void Stop(InputAction.CallbackContext obj)
        {
            inputValue = Vector2.zero;
        }
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
        FOX = transform.parent.GetComponent<Rigidbody2D>();
        //Enregistre les éléments du HUD pour les supprimer/réafficher via les variables
        Bullet = GameObject.FindGameObjectWithTag("Bullet");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //RigidBody du parapluie
        var RB = GetComponent<Rigidbody2D>();
        //Déplacement du parapluie
        RB.MovePosition(inputValue.normalized+(Vector2)transform.parent.position);
        var aimAngle = Mathf.Atan2(-inputValue.x, inputValue.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }

    //Collision du parapluie Collider2D
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Si la collision se fait avec un ennemi
        if(other.gameObject.layer == LayerMask.NameToLayer("Ennemy"))
        {
            //Valeur opposé de la direction du parapluie
            var NinputValue = inputValue * -1;
            //Récupération de la valeur de saut sur l'ennemi
            var CollisionBumpValue = other.gameObject.GetComponent<BumpBehaviour>().BumpValue;
            //Déplace le joueur
            FOX.AddForce(NinputValue * CollisionBumpValue, ForceMode2D.Impulse);
            //Lance la coroutine Cooldown de EnnemyBehaviour (pour désactiver la hitbox de l'ennemi)
            other.gameObject.GetComponent<BumpBehaviour>().LauchCooldown();
        }
    }
    void OnDestroy()
    {
        //Détruit les input à chaque rechargement de scene
        playerInput.Disable();
    }
}
