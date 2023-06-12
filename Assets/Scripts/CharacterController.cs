using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IDamagable
{
    #region variables
    private Rigidbody2D rb;
    private ControlActions actions;
    public float speed;
    public float jumpPower;
    private Vector2 moveVector;
    private Vector2 mousePos;
    public Inventory inventory;

#endregion

    void Start()
    {
        actions = new ControlActions();
        rb = GetComponent<Rigidbody2D>();
        actions.Enable();
        inventory = new Inventory();
        inventory.InitializeInventory();

        actions.Character.Movement.performed  += ctx => moveVector = ctx.ReadValue<Vector2>();
        actions.Character.Movement.canceled  += ctx => moveVector = Vector2.zero;
        actions.Character.Jump.performed  += ctx => Jump();
        actions.Character.Attack.performed  += ctx => Attack();
        actions.Character.Inventory.performed  += ctx => inventory.OpenInventory();
        actions.Character.Enteract.performed  += ctx => Enteract();

        actions.Character.MousePos.performed  += ctx => mousePos = ctx.ReadValue<Vector2>();
        actions.Character.MousePos.canceled  += ctx => mousePos = Vector2.zero;
    }

    void FixedUpdate()
    {
        Vector2 sidewaysMovement = new Vector2(moveVector.x,0);
        rb.AddForce(sidewaysMovement*(speed*10));
        if(moveVector.x < -.2f){transform.rotation = Quaternion.Euler(0,180,0);}else if(moveVector.x>.2f){
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        if(moveVector.y > .5){}//try to climb
        //limit max speed
        float maxHorizontalSpeed = Mathf.Clamp(rb.velocity.y,-maxspeed,maxspeed);
        float maxVerticalSpeed = Mathf.Clamp(rb.velocity.x,-maxspeed,maxspeed);
        rb.velocity = new Vector2(maxVerticalSpeed,maxHorizontalSpeed);

        if(moveVector == Vector2.zero)
        {
            if(grounded)
            {
                //DAMPEN
                rb.velocity = rb.velocity/1.25f;
                return;
            }
        }
    }

    #region movement

    private void GroundCheck()
    {
        if(rb.IsTouchingLayers(groundlayerMask))
        {
            List<ContactPoint2D> touching = new List<ContactPoint2D>();
            rb.GetContacts(touching);
            for (int i = 0; i < touching.Count; i++)
            {
                if(touching[i].normal.x != 0)
                {
                    //walljump
                    if(!wallJump){canJump = true;}
                    grounded = false;
                }else
                {
                    grounded = true;
                    canJump = true;
                    return;
                }   
            }          
        }
        canJump = false;
        grounded = false;
    }

    public float maxspeed = 4;
    public LayerMask groundlayerMask;

    bool canJump = true;
    bool grounded = true;
    bool wallJump = false;
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

    void OnCollisionEnter2D(Collision2D collision)  {   GroundCheck();  }
    void OnCollisionExit2D(Collision2D collision)   {   GroundCheck();  }

    #endregion

    private void Enteract()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 mousePos2D = new Vector2(mouseWorldPosition.x,mouseWorldPosition.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D,Vector2.zero);
        if(hit.collider != null)
        {
            IEnteractable enteractable = hit.collider.GetComponent<IEnteractable>();
            if(enteractable != null)
            {
                enteractable.OnEnteract();
            }
        }
    }
}
