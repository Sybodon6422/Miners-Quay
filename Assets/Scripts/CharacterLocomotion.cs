using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{ 
    private Astronaut astronaut; public Astronaut Astronaut { get { return astronaut; } }
    private Vector2 mousePos;
    private ControlActions actions;

    public Inventory inventory { get { return astronaut.inventory; } }

    public void EnableAstro(Astronaut astro)
    {
        astronaut = astro;
        actions = new ControlActions();

        actions.Enable();

        actions.Character.Movement.performed += ctx => astronaut.SetMoveVector(ctx.ReadValue<Vector2>());
        actions.Character.Movement.canceled += ctx => astronaut.SetMoveVector(Vector2.zero);

        actions.Character.MousePos.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
        actions.Character.MousePos.canceled += ctx => mousePos = Vector2.zero;

        actions.Character.Jump.performed += ctx => astronaut.Jump();
        actions.Character.Jump.canceled += ctx => astronaut.EndJump();

        actions.Character.Attack.performed += ctx => Attack();

        actions.Character.Inventory.performed += ctx => InventoryManager.I.ToggleInventory();
        actions.Character.Enteract.performed += ctx => EnteractAttempt();
    }

    public void DisableAstro()
    {
        actions.Disable();
        astronaut = null;
    }

    #region singleton
    private static CharacterLocomotion _instance;
    public static CharacterLocomotion I { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    #endregion

    private void FixedUpdate()
    {
        CheckForInteractions();
    }

    public void EnteractAttempt()
    {

    }

    void CheckForInteractions()
    {
        bool showInteraction = false;
        if(astronaut.canEnterBuilding)
        {
            showInteraction = true;
        }
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 mousePos2D = new Vector2(mouseWorldPosition.x,mouseWorldPosition.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D,Vector2.zero,3f,LayerMask.GetMask("EnteractionOnly"));
        if(hit.collider != null)
        {
            IEnteractable enteractable = hit.collider.GetComponent<IEnteractable>();
            if(enteractable != null)
            {
                showInteraction = true;
            }
        }

        ToggleInteractionObject(showInteraction);
    }

    private void Attack()
    {
        GetComponentInChildren<PickAxe>().Swing();
    }

    public void EnteractionCall()
    {

    }

    private void ToggleInteractionObject(bool turnOn)
    {
        astronaut.interactionShow.SetActive(turnOn);
    }
}
