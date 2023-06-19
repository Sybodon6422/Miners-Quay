using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform trackedPlayer;
    private Rigidbody2D rb;
    public float smoothSpeed = 0.1f;

    void Start()
    {
        trackedPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        SmoothFollow();
    }

    public void SmoothFollow()
    {
        Vector3 targetPos = trackedPlayer.position;
        Vector3 smoothFollow = Vector3.Lerp(transform.position,
        targetPos, smoothSpeed);

        rb.MovePosition(smoothFollow);
    }

    public void SetPosition(Vector2 position)
    {
        rb.isKinematic = true;
        var oldCamPos = transform.position;
        transform.position = new Vector3(position.x,position.y,oldCamPos.z);
        rb.isKinematic = false;
    }
}
