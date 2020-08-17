using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow")]
    public Transform target;
    public Vector3 offset;
    public float moveSpeed;

    [Header("Settings")]
    public float whiteSpace;
    public Vector2 minPos;
    public Vector2 maxPos;

    void Start()
    {
        transform.position = target.position + offset;
    }

    void LateUpdate() {
        if (Vector2.Distance(target.position, transform.position) > whiteSpace)
            transform.position = Vector3.Lerp(transform.position, target.position + offset, moveSpeed * Time.deltaTime);

        Vector3 clamped = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x),
            Mathf.Clamp(transform.position.y, minPos.y, maxPos.y),
            transform.position.z);

        transform.position = clamped;
    }
}