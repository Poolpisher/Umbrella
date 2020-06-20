using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float deadZone;
    Rigidbody2D Parent;
    // Start is called before the first frame update
    void Start()
    {
        Parent = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var rightStickDirection = new Vector2(
        Input.GetAxis("Umbrella_LR2"),
        Input.GetAxis("Umbrella_UD2")
        );
        if (rightStickDirection.magnitude <= deadZone)
            rightStickDirection = Vector3.zero;
        rightStickDirection = Vector3.ClampMagnitude(rightStickDirection, 0.5f);

        var rightStickDirection2 = new Vector2(
        Input.GetAxis("Umbrella_LR"),
        Input.GetAxis("Umbrella_UD")
        );

        var layerMask = LayerMask.GetMask("Player");
        // Invert the mask
        layerMask = ~layerMask;
        var hit = Physics2D.Raycast(transform.parent.position, rightStickDirection, rightStickDirection.magnitude, layerMask);
        var Sposition = rightStickDirection;
        if (hit.collider != null && Sposition.sqrMagnitude > 0)
        {
            Sposition = hit.point;
        }
        else
        {
            Sposition += (Vector2)transform.parent.position;
        }
        transform.position = Sposition;
        
        //Angle:
        var aimAngle = Mathf.Atan2(rightStickDirection2.x, rightStickDirection2.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }

    public void Jump(int force)
    {
        Parent.velocity= new Vector2(Parent.velocity.x, 0);
        Parent.AddForce(Vector2.up * force);
        StartCoroutine(CoroutineTest());
    }
    
    IEnumerator CoroutineTest()
    {
        
        var desactive = gameObject.GetComponent<BoxCollider2D>();
        desactive.enabled = false;
        var compte = 0;
        while(compte < 10) {
            yield return new WaitForEndOfFrame();
            compte++;
        }
        desactive.enabled = true;
    }
        
}
