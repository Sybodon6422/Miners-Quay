using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private ControlActions actions;
    public float speed;
    public float jumpPower;
    private Vector2 moveVector;

    void Start()
    {
        actions = new ControlActions();
        rb = GetComponent<Rigidbody2D>();
        actions.Enable();

        actions.Character.Movement.performed  += ctx => moveVector = ctx.ReadValue<Vector2>();
        actions.Character.Movement.canceled  += ctx => moveVector = Vector2.zero;
    }
    public LayerMask groundlayerMask;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 sidewaysMovement = new Vector2(moveVector.x,0);
        rb.AddForce(sidewaysMovement*(speed*10));

        if(moveVector.y > .5){Jump();}
        if(rb.IsTouchingLayers(groundlayerMask))
        {
            canJump = true;
            if(moveVector == Vector2.zero)
            {
                //DAMPEN
                rb.velocity = rb.velocity/1.25f;
            }
        }
    }

    bool canJump = true;
    private void Jump()
    {
        if(canJump)
        {
            rb.AddForce(Vector2.up*jumpPower);
            canJump = false;
        }
    }

    private void CheckGround()
    {
        if(rb.IsTouchingLayers(6))
        {

        }
    }
}
