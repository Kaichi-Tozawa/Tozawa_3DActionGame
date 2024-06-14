using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCComander : MonoBehaviour,IPauseAble,INPCComander
{
    [SerializeField, Header("各ステートの稼働時間")] float[] _workTimes = new float[Enum.GetValues(typeof(NPCState)).Length];
    [SerializeField, Header("移動速度")] float _moveSpeed = 5f;
    [SerializeField, Header("どれだけ接近したら攻撃し始めるか")] float _attackRange = 3f;
    [SerializeField, Header("防衛対象からどれくらい離れたら戻ろうとするか")] float _guardDistance =20;
    [SerializeField] float _minimalSetBackTime = 2;
    bool _attackParam = false;
    bool _isPause = false;
    bool _inRange = true;
    bool _isActiveGuard = false;
    GameObject _priorityTarget  = null;
    GameObject _guardTarget = null;
    Vector3 _moveDirection;

    IStateRevolver<NPCState> _iRevolver;
    IMove _iMove;
    IMoveAnimate _iMoveAnim;
    IAttackAnimate _iAttackAnim;
    IUnitDetector _iUnitDetector;
    NPCState _state = NPCState.StandBy;
    
    private void OnEnable()
    {
        Initialize();
        StartTimer();
    }


    void StartTimer()
    {
        StartCoroutine(StateBehaviorTimerCorutine(() =>
        {
            _iMove.Brake();
            StartTimer();
        }
        ,_workTimes[(int)_iRevolver.CurrentState()]));
    }
    
    private void FixedUpdate()
    {
        if(!_isPause)
        {
            VariableUpdate();
            SetAnim();
        }
    }
    void SetAnim()
    {
        _iAttackAnim.SetAttackBool("Attack", _attackParam);
        _iMoveAnim.SetMoveVelocityParam();
    }
    private void VariableUpdate()
    {
        switch ( _state ) 
        {
            case NPCState.StandBy:
                StandBy();
                break;
            case NPCState.Running:
                Running();
                break;
            case NPCState.Attacking:
                Attack();
                break;
        }
    }
    

    void StandBy()
    {
        _attackParam = false;
        _iMove.Behold(_iUnitDetector.TargetDirection());
    }
    void Running()
    {
        if (_guardTarget　!= null)
        {
            DefenceApproach();
        }
        else
        {
            RaidApproach();
        }
        _iMove.Move(_moveDirection);
    }
    void DefenceApproach()
    {
        _inRange = (_guardDistance > Vector3.Distance(transform.position,_guardTarget.transform.position))  ? true : false;
        if(_inRange&& !_isActiveGuard)
        {
            if (_iUnitDetector.TargetDistance() <= _attackRange)
            {
                CancelAction();
            }
            else
            {
                _moveDirection = _iUnitDetector.TargetDirection();
            }
        }
        else
        {
            StartCoroutine(ReturnToGuardTarget());
        }
        
    }
    IEnumerator ReturnToGuardTarget()
    {
        _isActiveGuard = true;
        _moveDirection = GuardDirection();
        yield return new WaitUntil(() => _inRange);
        yield return new WaitForSeconds(_minimalSetBackTime);
        _isActiveGuard = false;
    }
    Vector3 GuardDirection()
    {
        var dir = _guardTarget.transform.position - this.transform.position;
        return dir;
    }

    void RaidApproach()
    {
        if(_priorityTarget  )
        {
            _moveDirection = _priorityTarget.transform.position - this.transform.position;
            if(Vector3.Distance(this.transform.position, _priorityTarget.transform.position) <_attackRange)
            {
                _priorityTarget = null;
            }
        }
        else
        {
            ConbatMove();
        }
    }
    
    void ConbatMove()
    {
        if (_iUnitDetector.TargetDistance() <= _attackRange)
        {
            CancelAction();
        }
        else
        {
            _moveDirection = _iUnitDetector.TargetDirection();
        }
    }

    void Attack()
    {
        if (_iUnitDetector.TargetDistance() < _attackRange && _iUnitDetector.TargetDistance() != 0)
        {
            _attackParam = true;
        }
    }
    void CancelAction()
    {
        StopAllCoroutines();
        _iRevolver.NextState();
        StartTimer();
    }


    /// <summary>
    /// その規定の稼働時間経ったらステートを切り替える
    /// </summary>
    /// <param name="action">追加でさせたい処理</param>
    /// <param name="operatingtime">そのステートの処理</param>
    IEnumerator StateBehaviorTimerCorutine(Action action, float operatingtime)
    {
        _state = _iRevolver.CurrentState();
        //Debug.Log(_state+"開始"+operatingtime);
        yield return new WaitForSeconds(operatingtime);
        _iRevolver.NextState();
        action.Invoke();
        
    }

    void IPauseAble.Pause()
    {
        StopAllCoroutines();
        _isPause = true;
        _iMoveAnim.Pause();
        _iMove.Pause();
    }

    void IPauseAble.Reboot()
    {
        StartTimer();
        _isPause = false;
        _iMoveAnim.Reboot();
        _iMove.Reboot();
    }
    void Initialize()
    {
        var rb = GetComponent<Rigidbody>();
        var anim = GetComponent<Animator>();

        _iRevolver = new StateRevolver<NPCState>();
        _iMove = new MoveByVelocity(rb, _moveSpeed);
        _iMoveAnim = new CharacterAnimate(rb,anim,"Speed");
        _iAttackAnim = new CharacterAnimate(rb, anim, "Speed");
        _iUnitDetector = new UnitDetector(this.tag,this.gameObject);
        _state = _iRevolver.CurrentState();
    }

    IMove INPCComander.IMoveInstance()
    {
        if(_iMove == null)
        {
            _iMove = new MoveByVelocity(GetComponent<Rigidbody>(), _moveSpeed);
        }
        return _iMove;
    }

    void INPCComander.SetPriorityTarget(GameObject t)
    {
        _priorityTarget = t;
    }

    void INPCComander.DefenceContract(GameObject obj)
    {
        _guardTarget = obj;
    }
}
