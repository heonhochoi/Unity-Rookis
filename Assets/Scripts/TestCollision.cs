using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision: MonoBehaviour
{
    /* OnCollisoinEnter 작동 조건 */
    // 1. RigidBody 존재(나) - (IsKinematic : OFF)
    // 2. Collider 존재 (나와 상대) - (IsTrigger : OFF) 
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision ! {collision.gameObject.name}");
    }

    /* OnTriggerEnter 작동 조건 */
    // 1. 둘 다 Collider가 존재해야 한다.
    // 2. 둘 중 하나는 IsTrigger : On
    // 3. 둘 중 하나는 RigidBody가 있어야 함.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger ! {other.gameObject.name} ");
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 look = transform.TransformDirection(Vector3.forward);   
        Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);

        foreach(RaycastHit hit in hits)
        {
            Debug.Log($"LayCast {hit.collider.gameObject.name}!");
        }         
    }
}
