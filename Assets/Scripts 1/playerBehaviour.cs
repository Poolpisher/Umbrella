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
    private GameObject StartMenu;
    private GameObject HUDMenu;
    private GameObject Bullet;
    [Header("Miscellaneous")]
    [SerializeField] private LayerMask ground;
    private Animator myAnimator;
    private Player playerInput;
    private float inputValue;
    private Rigidbody2D RB2D;
    private AnimatorStateInfo[] m_Animator;
    private bool IsPause;
    public bool CanJump;
    public bool CanShoot;
    private void OnEnable()
    {
        playerInput = new Player();
        playerInput.Enable();
        playerInput.Main.Move.performed += Move;
        playerInput.Main.Move.canceled += Stop;
        playerInput.Main.Jump.performed += Jump;
        playerInput.Main.Pause.performed += Pause;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        inputValue = obj.ReadValue<float>();
        myAnimator.SetBool("running", true);
    }
    private void Pause(InputAction.CallbackContext obj)
    {
        IsPause = !IsPause;
        if (IsPause)
        {
            for(var i=0; i<AnimatorsToSaveHUD.Length; i++)
            {
                m_Animator[i] = AnimatorsToSaveHUD[i].GetCurrentAnimatorStateInfo(0);
            }
            //desactive HUD
            HUDMenu.SetActive(false);
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
    private void Stop(InputAction.CallbackContext obj)
    {
        inputValue = 0;
        myAnimator.SetBool("running", false);
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        if (CanJump && RB2D.velocity.y < 2) {
            CanJump = false;
            var JumpDirection = Vector2.up * JumpForce;
            RB2D.AddForce(JumpDirection, ForceMode2D.Impulse);
            myAnimator.SetBool("Grounded", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (ground == (ground | (1 << other.gameObject.layer)) && other.contacts[0].normal.y >= 0.99999f)
            {
            CanJump = true;
            CanShoot = true;
            myAnimator.SetBool("Grounded", true);
            Bullet.SetActive(true);
        }
        else
        {
            myAnimator.SetBool("Death", true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Bullet = GameObject.FindGameObjectWithTag("Bullet");
        StartMenu = GameObject.FindGameObjectWithTag("Start");
        HUDMenu = GameObject.FindGameObjectWithTag("HUD");
        StartMenu.SetActive(false);
            myAnimator = GetComponent<Animator>();
            m_Animator = new AnimatorStateInfo[AnimatorsToSaveHUD.Length];
            CanJump = true;
            RB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Récupération du stick sur l'axe X
        var V2 = new Vector2 {
            x = inputValue,
            y = 0};
        //RB2D.velocity = V2*Speed*Time.fixedDeltaTime;
        if (RB2D.velocity.x < MaxSpeed)
        {
            RB2D.AddForce(V2 * Speed);
        }
        myAnimator.SetBool("JumpDOWN", RB2D.velocity.y < -0.0001f);
        myAnimator.SetBool("JumpUP", RB2D.velocity.y > 0.0001f);
    }

    void OnDestroy()
    {
        playerInput.Disable();
    }
}