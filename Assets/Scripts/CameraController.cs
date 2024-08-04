using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region singleton

    private static CameraController _instance;
    public static CameraController I { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    #region public methods

    public void SetCameraToStaticMode(Vector3 camStaticPos)
    {
        rb.isKinematic = true;
        transform.position = camStaticPos;
        staticMode = true;
    }

    public void SetPosition(Vector2 position)
    {
        rb.isKinematic = true;
        var oldCamPos = transform.position;
        transform.position = new Vector3(position.x, position.y, oldCamPos.z);
        rb.isKinematic = false;
    }

    public void SetCameraZoom(float zoom)
    {
        Camera.main.orthographicSize = zoom;
    }

    public void SetTrackedObject(Transform newTrackedObject)
    {
        trackedPlayer = newTrackedObject;
        staticMode = false;
        rb.isKinematic = false;
    }

    private bool takingOver = false;

    public void TakeOverMode()
    {
        takingOver = true;
    }

    #endregion

    [SerializeField] private Transform trackedPlayer;
    private Rigidbody2D rb;
    private float smoothSpeed = 0.07f;

    private bool staticMode = true;

    private ControlActions actions;
    private Vector2 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        actions = new ControlActions();
        actions.Enable();
        actions.Character.MousePos.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
        actions.Character.MousePos.canceled += ctx => mousePos = Vector2.zero;
        actions.Character.Attack.performed += ctx => MouseAction();
    }

    #region astronaut control mode

    private void MouseAction()
    {
        if (staticMode)
        {
            Enteract();
            return;
        }
        Enteract();
    }

    private void Enteract()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 mousePos2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 99);
        if (hit.collider == null) {
            return; }

        IEnteractable enteractable = hit.collider.GetComponent<IEnteractable>();
        if (enteractable == null) {
            return; }

        if (takingOver)
        {
            if (enteractable is Astronaut)
            {
                GameManager.I.TakeOverAstronaut((Astronaut)enteractable);
                takingOver = false;
                return;
            }
        }
        else
        {
            var enteractionType = enteractable.OnEnteract(GameManager.I.controlledAstro.Astronaut);
            if (enteractionType.astronautEnteractionOnly)
            {
                //need a ref to astro that were controlling
                GameManager.I.controlledAstro.EnteractAttempt();
            }
            else
            {
                //world type enteraction. like buildings
            }
        }


        /*
        if (canEnterBuilding)
        {
            buildingToEnter.OnEnteract(this);
        }
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 mousePos2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 3f, LayerMask.GetMask("EnteractionOnly"));
        if (hit.collider != null)
        {
            IEnteractable enteractable = hit.collider.GetComponent<IEnteractable>();
            if (enteractable != null)
            {
                enteractable.OnEnteract(this);
            }
        }*/
    }

    #endregion

    private void LateUpdate()
    {
        if (staticMode)
        {
            return;
        }
        SmoothFollow();
    }

    private void SmoothFollow()
    {
        Vector3 targetPos = trackedPlayer.position;
        Vector3 smoothFollow = Vector3.Lerp(transform.position,
        targetPos, smoothSpeed);

        rb.MovePosition(smoothFollow);
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
