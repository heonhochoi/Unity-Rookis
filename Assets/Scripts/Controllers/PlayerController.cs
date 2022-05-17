using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // Player State
    public enum Player_state
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

    PlayerStat _stat;
    Vector3 _destPos;

    [SerializeField]
    Player_state _state = Player_state.Idle;

    int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster);

    GameObject _lockTarget;

    public Player_state State
    {
        get { return _state; }
        set
        {
            _state = value;
            Animator anim = GetComponent<Animator>();
            switch (value)
            {
                case Player_state.Die:
                    break;
                case Player_state.Moving:
                    anim.CrossFade("RUN",0.1f);
                    break;
                case Player_state.Idle:
                    anim.CrossFade("WAIT", 0.2f);
                    break;
                case Player_state.Skill:
                    anim.CrossFade("ATTACK", 0.2f,-1,0);
                    break;
            }
        }
    }

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.mouseAction -= OnMouseEvent;
        Managers.Input.mouseAction += OnMouseEvent;

        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    void UpdateDie()
    {
        // 아무것도 안함
        State = Player_state.Die;
    }

    void UpdateMoving()
    {
        //moster가 사정거리 내에 있을 시 공격
        if (_lockTarget != null)
        {
            float _distance = (_destPos - transform.position).magnitude;

            if(_distance <= 1.3f)
            {
                State = Player_state.Skill;
            }
        }

        //이동
        Vector3 dir = _destPos - transform.position;
        //Debug.Log($"dir.magnitude = {dir.magnitude}");
        if (dir.magnitude <= 0.1f )
        {
            State = Player_state.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            nma.Move(dir.normalized * moveDist);

            //Debug.DrawRay(transform.position + Vector3.up, dir.normalized, Color.blue, 1.0f);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if(Input.GetMouseButton(0) == false)
                    _state = Player_state.Idle;
                return;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 15 * Time.deltaTime);
        }
    }

    void UpdateIdle()
    {
    }

    void UpdateSkill()
    {
        if(_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime); 
        }
    }

    void OnHitEvent()
    {
        if(_lockTarget != null)
        {
            Stat _targetStat = _lockTarget.GetComponent<Stat>();
            PlayerStat myStat = gameObject.GetComponent<PlayerStat>();
            int damage = Mathf.Max(0,myStat.Attack - _targetStat.Defence);

            Debug.Log($"{damage}");

            _targetStat.Hp -= damage;
        }

        if (_stopSkill)
        {
            State = Player_state.Idle;
        }
        else
        {
            State = Player_state.Skill;
        }
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
            case Player_state.Skill:
                UpdateSkill();
                break;
        }
    }

    bool _stopSkill = false;
    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Player_state.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Player_state.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Player_state.Skill:                
                {
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;

        }

    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        _destPos.y = 0;
                        State = Player_state.Moving;

                        _stopSkill = false;
                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        {
                            _lockTarget = hit.collider.gameObject;
                        }
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _destPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                {
                    _stopSkill = true;
                }
                break;

        }
    }

}
