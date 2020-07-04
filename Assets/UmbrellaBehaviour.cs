using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UmbrellaBehaviour : MonoBehaviour
{
    private Player playerInput;
    private Vector2 inputValue;
    private Rigidbody2D FOX;
    private void OnEnable()
    {
        playerInput = new Player();
        playerInput.Enable();
        playerInput.Main.Umbrella.performed += Move;
        playerInput.Main.Umbrella.canceled += Stop;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        var NinputValue = inputValue * -1;
        if(other.gameObject.layer == LayerMask.NameToLayer("Ennemy"))
        {
            var CollisionBumpValue = other.gameObject.GetComponent<EnnemyBehaviour>().BumpValue;
            FOX.AddForce(NinputValue * CollisionBumpValue, ForceMode2D.Impulse);
            Debug.Log("test");
        }
    }
}
