using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destPos;

    void Start()
    {
        Managers.Input.mouseAction -= OnMouseClicked; 
        Managers.Input.mouseAction += OnMouseClicked; 
    }

    float wait_run_ratio = 0;

    public enum Player_state
    { 
        Die,
        Moving,
        Idle,
    }

    Player_state _state = Player_state.Idle;

    void UpdateDie()
    {
        // 아무것도 안함
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.001f)
        {
            _state = Player_state.Idle;
        }
        else
        {
            // _speed * time.deltaTime이 남은 거리보다 커질수 있으므로
            // Clamp함수를 통해 최대 최소 값 조절
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 15 * Time.deltaTime);
        }

        // 애니메이션 처리
        wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("wait_run_ratio", wait_run_ratio);
        anim.Play("WAIT_RUN");
    }

    void UpdateIdle()
    {
        wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("wait_run_ratio", wait_run_ratio);
        anim.Play("WAIT_RUN");
    }

    void Update()
    {           
        switch (_state)
        {
            case Player_state.Die:
                UpdateDie();
                break;
            case Player_state.Moving:
                UpdateMoving();
                break;
            case Player_state.Idle:
                UpdateIdle();
                break;
        }
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == Player_state.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall"))) // 벽만 충돌되게 수정
        {
            _destPos = hit.point;
            _state = Player_state.Moving; 
        }
    }
}
