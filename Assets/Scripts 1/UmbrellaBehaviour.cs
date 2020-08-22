using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UmbrellaBehaviour : MonoBehaviour
{
    [SerializeField] private int ShootBumpValue;
    private Player playerInput;
    private Vector2 inputValue;
    private Rigidbody2D FOX;
    private bool CanJump;
    public bool CanShoot;
    private GameObject Bullet;
    private void OnEnable()
    {
        playerInput = new Player();
        playerInput.Enable();
        playerInput.Main.Umbrella.performed += Move;
        playerInput.Main.Umbrella.canceled += Stop;
        playerInput.Main.Shoot.performed += Shoot;
    }
    //Fonction tir
    private void Shoot(InputAction.CallbackContext obj)
    {
        var NinputValue = inputValue * -1;
        if (CanShoot == true)
        {
            CanShoot = false;
            FOX.AddForce(NinputValue * ShootBumpValue, ForceMode2D.Impulse);
            Bullet = GameObject.FindGameObjectWithTag("Bullet");
            Bullet.SetActive(false);
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
    private void Move(InputAction.CallbackContext obj)
    {
        inputValue = obj.ReadValue<Vector2>();
    }
    private void Stop(InputAction.CallbackContext obj)
    {
        inputValue = Vector2.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
        FOX = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var RB = GetComponent<Rigidbody2D>();
        RB.MovePosition(inputValue.normalized+(Vector2)transform.parent.position);
        var aimAngle = Mathf.Atan2(-inputValue.x, inputValue.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }
    //Collision du parapluie sur un ennemi
    private void OnCollisionEnter2D(Collision2D other)
    {
        var NinputValue = inputValue * -1;
        if(other.gameObject.layer == LayerMask.NameToLayer("Ennemy"))
        {
            var CollisionBumpValue = other.gameObject.GetComponent<BumpBehaviour>().BumpValue;
            FOX.AddForce(NinputValue * CollisionBumpValue, ForceMode2D.Impulse);
            //Lance la coroutine Cooldown de EnnemyBehaviour (pour désactiver la hitbox de l'ennemi)
            other.gameObject.GetComponent<BumpBehaviour>().LauchCooldown();
        }
    }
}
