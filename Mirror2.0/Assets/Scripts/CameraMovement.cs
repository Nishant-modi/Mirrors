using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using MoreMountains.Feedbacks;

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
        GameObject target11 = target1;
        GameObject target22 = target2;
        if (target1.activeSelf == false)
        {
            target11 = target22;
            zoom = cam.orthographicSize;
        }
        else if (target2.activeSelf == false)
        {
            target22 = target11;
            zoom = cam.orthographicSize;
        }
        target = (target11.transform.position + target22.transform.position) / 2;
        Vector3 targetPosition = new Vector3(target.x + offset.x, target.y + offset.y, -10f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        zoom = (target11.transform.position - target22.transform.position).magnitude - target22.transform.localScale.x;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref vel, smoothTime);
    }
}
