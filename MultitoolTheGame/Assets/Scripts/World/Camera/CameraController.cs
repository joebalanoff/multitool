using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow")]
    public Transform target;
    public Vector3 offset;
    public float moveSpeed;
    private Vector2 velocity;

    [Header("Settings")]
    public Vector2 minPos;
    public Vector2 maxPos;

    void Start()
    {
        transform.position = target.position + offset;
    }

    void LateUpdate()
    {
        float x = Mathf.SmoothDamp(transform.position.x, target.position.x + offset.x, ref velocity.x, moveSpeed);
        float y = Mathf.SmoothDamp(transform.position.y, target.position.y + offset.y, ref velocity.y, moveSpeed);
        transform.position = new Vector3(x, y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x), Mathf.Clamp(transform.position.y, minPos.y, maxPos.y), transform.position.z);
    }
}