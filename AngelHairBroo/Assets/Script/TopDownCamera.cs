using UnityEngine;
using System.Collections;

public class TopDownCamera : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 targetOffset;
    public float moveSpeed = 2f;

    private Transform myTransform;

    void Start()
    {
        myTransform = transform;
    }

    void LateUpdate()
    {
        myTransform.position = Vector3.Lerp(myTransform.position, followTarget.position + targetOffset, moveSpeed * Time.deltaTime);
    }
}