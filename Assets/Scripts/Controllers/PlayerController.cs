using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    bool _moveToDest = false;
    Vector3 _destPos;

    void Start()
    {
        Managers.Input.keyAction -= OnKeyboard;
        Managers.Input.keyAction += OnKeyboard;
        Managers.Input.mouseAction -= OnMouseClicked; 
        Managers.Input.mouseAction += OnMouseClicked; 
    }

    void Update()
    {   
        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;
            if(dir.magnitude < 0.001f)
            {
                _moveToDest = false;
            }
            else
            {
                // _speed * time.deltaTime이 남은 거리보다 커질수 있으므로
                // Clamp함수를 통해 최대 최소 값 조절
                float moveDist = Mathf.Clamp(_speed * Time.deltaTime,0,dir.magnitude);
                transform.position += dir.normalized * moveDist;
                transform.LookAt(_destPos);
            }
        }
    }

    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += (Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += (Vector3.back * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += (Vector3.left * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += (Vector3.right * Time.deltaTime * _speed);
        }

        _moveToDest = false;
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall"))) // 벽만 충돌되게 수정
        {
            _destPos = hit.point;
            _moveToDest = true; 
        }
    }
}
