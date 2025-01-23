using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset = new(0, 2);
    [SerializeField] private float followDamping = 8f;
    private void FixedUpdate()
    {
        transform.Translate(new Vector3((target.position.x - transform.position.x + offset.x) / followDamping, (target.position.y - transform.position.y + offset.y) / followDamping, 0));
    }
}
