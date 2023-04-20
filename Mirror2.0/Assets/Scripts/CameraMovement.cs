using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    public GameObject target1;
    public GameObject target2;
    public Camera cam;
    public Vector3 target;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    private float zoom;
    //public float zoomMultiplier = 4f;
    public float minZoom = 2f;
    public float maxZoom = 8f;
    private float vel = 0f;
    void Update()
    {
        if (target1.activeSelf == false)
        {
            target1 = target2;
            zoom = cam.orthographicSize;
        }
        else if (target2.activeSelf == false)
        {
            target2 = target1;
        }
        target = (target1.transform.position + target2.transform.position) / 2;
        Vector3 targetPosition = new Vector3(target.x + offset.x, 0f, -10f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        zoom = (target1.transform.position - target2.transform.position).magnitude - target2.transform.localScale.x;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref vel, smoothTime);
    }
}
