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
        // Local <-> World <-> (ViewPort <-> Screen) (ȭ��)

        //Debug.Log(Input.mousePosition); //Screen ��ǥ

        Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // ViewPort ��ǥ
    }
}
