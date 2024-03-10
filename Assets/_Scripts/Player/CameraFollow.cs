using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    public BoxCollider2D mapBounds;
    private float xMin,xMax,yMin,yMax;
    private float camX, camY;
    private float camOrthsize;
    private float camRatio;
    private Camera mainCam;

    private void Start()
    {
        xMin = mapBounds.bounds.min.x;
        xMax = mapBounds.bounds.max.x;
        yMin = mapBounds.bounds.min.y;
        yMax = mapBounds.bounds.max.y;
        mainCam = GetComponent<Camera>();
        camOrthsize = mainCam.orthographicSize;
        camRatio = (xMax + camOrthsize) / 2.0f;
    }
    private void FixedUpdate()
    {
        camX = Mathf.Clamp(followTransform.position.x, xMin + camRatio, xMax - camRatio);
        camY = Mathf.Clamp(followTransform.position.y, yMin + camOrthsize, yMax - camOrthsize);
        if (followTransform != null)
        {
            this.transform.position = new Vector3(camX, camY, this.transform.position.z);
        }
    }

}
