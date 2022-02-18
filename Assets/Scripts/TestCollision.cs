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
        Debug.Log($"Collision ! {collision.gameObject.name}");
    }

    /* OnTriggerEnter �۵� ���� */
    // 1. �� �� Collider�� �����ؾ� �Ѵ�.
    // 2. �� �� �ϳ��� IsTrigger : On
    // 3. �� �� �ϳ��� RigidBody�� �־�� ��.
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
