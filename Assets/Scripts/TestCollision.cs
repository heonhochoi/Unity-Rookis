using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision: MonoBehaviour
{
    /* OnCollisoinEnter �۵� ���� */
    // 1. RigidBody ����(��) - (IsKinematic : OFF)
    // 2. Collider ���� (���� ���) - (IsTrigger : OFF) 
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
