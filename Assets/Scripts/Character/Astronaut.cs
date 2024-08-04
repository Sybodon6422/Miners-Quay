using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour, IDamagable, IEnteractable
{
    #region data management
    private AstronautData astronautData;
    public AstronautData AstronautData { get { return astronautData; } }
    public void SetAstronautData(AstronautData astronautData)
    {
        this.astronautData = astronautData;
    }

    #endregion

    public bool canEnterBuilding = false;
    internal bool insideBuilding = false;
    public bool InsideBuilding { get { return insideBuilding; } }
    private protected CelestialBody currentPlanet;
    public float maxOxygen = 100; public float MaxOxygen { get { return maxOxygen; } }
    internal float oxygen = 100;
    private float damageCoolDown = .2f;

    private Rigidbody2D rb;
    public Inventory inventory;
    public Door buildingToEnter;

    [SerializeField] public GameObject interactionShow;
    private Vector2 moveVector; public void SetMoveVector(Vector2 moveVector) { this.moveVector = moveVector; }

    void Start()
    {
        currentPlanet = GameManager.I.GetCurrentPlanet();
        oxygen = maxOxygen;

        rb = GetComponent<Rigidbody2D>();
        inventory = new Inventory();
        inventory.InitializeInventory();

        Setup();
    }

    private protected virtual void Setup(){}

    #region planet managment

    public void SetPlanet(CelestialBody planet)
    {
        currentPlanet = planet;
        rb.gravityScale = planet.gravity;
    }

    #endregion

    public void BuildingSwitch(bool inside)
    {
        insideBuilding = inside;
        if(insideBuilding)
        {
            gameObject.layer = 15;
        }
        else
        {
            gameObject.layer = 3;
        }
    }

    private void EnterBuildilng()
    {
        if (canEnterBuilding)
        {
            buildingToEnter.OnEnteract(this);
        }
    }

    void FixedUpdate()
    {
        damageCoolDown = Mathf.Clamp(damageCoolDown-Time.deltaTime,0,.2f);

        if (!currentPlanet.oxygen)
        {
            if (insideBuilding) { oxygen = Mathf.Clamp(oxygen + Time.deltaTime, 0, maxOxygen); }
            else { oxygen = Mathf.Clamp(oxygen - Time.deltaTime, -1, maxOxygen); }
            if (oxygen <= 0) { TakeDamage(1); }
        }
        BaseUpdateCall();
    }



    private void BaseUpdateCall()
    {
        if (currentPlanet.oxygen) { HUDManager.I.UpdateOxygenBar(oxygen, maxOxygen); }

        Vector2 sidewaysMovement = new Vector2(moveVector.x, 0);
        rb.AddForce(sidewaysMovement * (speed * 10));
        if (moveVector.x < -.2f) { transform.rotation = Quaternion.Euler(0, 180, 0); }
        else if (moveVector.x > .2f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (moveVector.y > .5) { }//try to climb
        //limit max speed
        float verticalSpeedLimited = Mathf.Clamp(rb.velocity.y, -30, maxspeed);
        float horizontalSpeedLimited = Mathf.Clamp(rb.velocity.x, -maxspeed, maxspeed);
        if (jumping)
        {
            if (jumpTime > 0)
            {
                verticalSpeedLimited += jumpPower;
                jumpTime -= Time.deltaTime;
            }
        }
        rb.velocity = new Vector2(horizontalSpeedLimited, verticalSpeedLimited);
        if (moveVector == Vector2.zero && !jumping)
        {
            if (grounded)
            {
                jumpTime = maxJumpTime;
                //DAMPEN
                rb.velocity = rb.velocity / 1.25f;
                return;
            }
        }
    }

    #region movement
    [Header("Movement")]
    public float maxspeed = 4;
    public LayerMask groundlayerMask;
    [SerializeField] private float jumpTime = 0f;
    [SerializeField] private float maxJumpTime = .1f;

    public float speed;
    public float jumpPower;

    private bool canJump = true;
    private bool grounded = true;
    private bool wallJump = false;
    private bool jumping = false;

    void OnCollisionEnter2D(Collision2D collision) { GroundCheck(); }
    void OnCollisionExit2D(Collision2D collision) { GroundCheck(); }

    private void GroundCheck()
    {
        if (rb.IsTouchingLayers(groundlayerMask))
        {
            jumpTime = maxJumpTime;
            grounded = true;
            canJump = true;
            /*         List<ContactPoint2D> touching = new List<ContactPoint2D>();
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
                             jumpTime = maxJumpTime;
                             grounded = true;
                             canJump = true;
                             return;
                         }   
                     }    */
        }
        else
        {
            canJump = false;
            grounded = false;
        }
    }

    public void Jump()
    {
        if (canJump)
        {
            //initial jump burst
            rb.AddForce(Vector2.up * jumpPower * 100);
            jumping = true;
            canJump = false;
        }
    }

    public void EndJump()
    {
        jumping = false;
    }

    #endregion

    int health = 20;

    public void TakeDamage(int damage)
    {
        if(damageCoolDown > 0){return;}
        health -= damage;
        damageCoolDown = .2f;
        
        if(health <=0)
        {
            //dead
            Destroy(this.gameObject);
        }
    }

    public IEnteractable.EnteractionData OnEnteract(Astronaut astro)
    {
        return new IEnteractable.EnteractionData(false);
    }
}
