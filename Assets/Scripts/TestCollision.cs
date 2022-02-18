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
        Debug.Log("Collision !");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger !");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
