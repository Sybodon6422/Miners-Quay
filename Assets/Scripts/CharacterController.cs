using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IDamagable
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

        actions.Character.Attack.performed  += ctx => Attack();
    }
    public float maxspeed = 4;
    public LayerMask groundlayerMask;
    void FixedUpdate()
    {
        Vector2 sidewaysMovement = new Vector2(moveVector.x,0);
        rb.AddForce(sidewaysMovement*(speed*10));
        if(moveVector.x < -.2f){transform.rotation = Quaternion.Euler(0,180,0);}else if(moveVector.x>.2f){
            transform.rotation = Quaternion.Euler(0,0,0);
        }
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
        //limit max speed
        float maxHorizontalSpeed = Mathf.Clamp(rb.velocity.y,-maxspeed,maxspeed);
        float maxVerticalSpeed = Mathf.Clamp(rb.velocity.x,-maxspeed,maxspeed);
        rb.velocity = new Vector2(maxVerticalSpeed,maxHorizontalSpeed);
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
    private void Attack()
    {
        GetComponentInChildren<PickAxe>().Swing();
    }

    int health = 20;

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if(health <=0)
        {
            //dead
            Destroy(this.gameObject);
        }
    }
}
