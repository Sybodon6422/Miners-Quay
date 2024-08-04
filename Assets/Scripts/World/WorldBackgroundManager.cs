using UnityEngine;

public class WorldBackgroundManager : MonoBehaviour
{
    private Transform camTransform;
    void Start()
    {
        camTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the background with the camera with a parallax effect
        transform.position = new Vector3(camTransform.position.x, camTransform.position.y, transform.position.z);
        transform.rotation = Quaternion.identity;

    }
}
