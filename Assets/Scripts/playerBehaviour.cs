using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerBehaviour : MonoBehaviour
{
    [SerializeField] private int Speed;
    [SerializeField] private int MaxSpeed;
    [SerializeField] private int JumpForce;
    [SerializeField] Animator[] AnimatorsToSaveHUD;
    [SerializeField] private LayerMask ground;
    //HUD
    private GameObject StartMenu;
    private GameObject HUDMenu;
    private GameObject GameOverMenu;
    private GameObject Bullet;
    //Animator
    private Animator myAnimator;
    private AnimatorStateInfo[] m_Animator;
    //InputAction
    private Player playerInput;
    private float inputValue;
    //RigidBody
    private Rigidbody2D RB2D;
    //Bool
    private bool IsPause;
    public bool CanJump;
    public bool CanShoot;
    
    private void OnEnable()
    {
        //Activation des input du joueur
        playerInput = new Player();
        playerInput.Enable();
        playerInput.Main.Move.performed += Move;
        playerInput.Main.Move.canceled += Stop;
        playerInput.Main.Jump.performed += Jump;
        playerInput.Main.Pause.performed += Pause;
    }

    //Input action
        //Déplacement du joueur: Stick gauche/zqsd
        private void Move(InputAction.CallbackContext obj)
        {
            inputValue = obj.ReadValue<float>();
            myAnimator.SetBool("running", true);
        }
        //Arret du déplacement du joueur
        private void Stop(InputAction.CallbackContext obj)
        {
            inputValue = 0;
            myAnimator.SetBool("running", false);
        }
        // Start/Escape: Pause
        private void Pause(InputAction.CallbackContext obj)
        {
            //Change la valeur de la pause
            IsPause = !IsPause;
            if (IsPause)
            {
                //Sauvegarde l'état des animations dans un tableau
                for(var i=0; i<AnimatorsToSaveHUD.Length; i++)
                {
                    m_Animator[i] = AnimatorsToSaveHUD[i].GetCurrentAnimatorStateInfo(0);
                }
                //desactive HUD
                HUDMenu.SetActive(false);
                //arrete le temps
                Time.timeScale = 0f;
                //active canvas pause
                StartMenu.SetActive(true);
            }
            else
            {
                //desactive canvas pause
                StartMenu.SetActive(false);
                Time.timeScale = 1f;
                //active HUD
                HUDMenu.SetActive(true);
                //applique Animator m_Animator

            }
        }
        // L1/Espace: Saut
        private void Jump(InputAction.CallbackContext obj)
        {
            if (CanJump && RB2D.velocity.y < 2) {
                CanJump = false;
                var JumpDirection = Vector2.up * JumpForce;
                RB2D.AddForce(JumpDirection, ForceMode2D.Impulse);
                myAnimator.SetBool("Grounded", false);
            }
        }
    //Sur collision entre le joueur et un Collider 2D
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Collsion avec le sol
        if (ground == (ground | (1 << other.gameObject.layer)) && other.contacts[0].normal.y >= 0.9f)
            {
            CanJump = true;
            CanShoot = true;
            Bullet.SetActive(true);
            myAnimator.SetBool("Grounded", true);
        }
        //Collision avec un ennemi
        else
        {
            //Désactivation de toute les animations pour laisser celle de mort
            myAnimator.SetBool("Grounded", false);
            myAnimator.SetBool("Running", false);
            myAnimator.SetBool("JumpUP", false);
            myAnimator.SetBool("JumpDOWN", false);
            myAnimator.SetBool("Death", true);
            //desactive HUD
            HUDMenu.SetActive(false);
            //active Game Over
            GameOverMenu.SetActive(true);
            //arrete le temps
            Time.timeScale = 0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CanJump = true;
        RB2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        //Enregistre les éléments du HUD pour les supprimer/réafficher via les variables
        Bullet = GameObject.FindGameObjectWithTag("Bullet");
        StartMenu = GameObject.FindGameObjectWithTag("Start");
        GameOverMenu = GameObject.FindGameObjectWithTag("GameOver");
        GameOverMenu.SetActive(false);
        HUDMenu = GameObject.FindGameObjectWithTag("HUD");
        StartMenu.SetActive(false);
        //Sauvegarde l'état des animations du HUD
        m_Animator = new AnimatorStateInfo[AnimatorsToSaveHUD.Length];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Récupération du stick sur l'axe X
        var V2 = new Vector2 {
            x = inputValue,
            y = 0};
        //Déplace le joueur de la vitesse minimal à la maximal
        if (RB2D.velocity.x < MaxSpeed)
        {
            RB2D.AddForce(V2 * Speed);
        }
        myAnimator.SetBool("JumpDOWN", RB2D.velocity.y < -0.0001f);
        myAnimator.SetBool("JumpUP", RB2D.velocity.y > 0.0001f);
    }

    void OnDestroy()
    {
        //Détruit les input à chaque rechargement de scene
        playerInput.Disable();
    }
}