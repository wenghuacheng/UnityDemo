using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.Patterns
{
    /// <summary>
    /// ״̬ö��
    /// </summary>
    public enum TestBotStatusEnum
    {
        Idle,
        Walk,
        Chase
    }

    #region ״̬
    /// <summary>
    /// ״̬�ӿ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IState<T> where T : Enum
    {
        T Id { get; }

        //����״̬
        void OnEnter();

        //״̬����
        void OnUpdate();

        //״̬�˳�
        void OnExit();

        //�ж��Ƿ���Ҫ�л�״̬
        bool TransitionState(out T id);
    }

    /// <summary>
    /// ����ר�����
    /// </summary>
    public abstract class TestBotState : IState<TestBotStatusEnum>
    {
        private readonly TestBotStatusEnum _id;
        protected GameObject _self;

        private float radius = 5;

        public TestBotState(GameObject self, TestBotStatusEnum id)
        {
            _id = id;
            this._self = self;
        }

        public TestBotStatusEnum Id => _id;

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public abstract void OnUpdate();

        public abstract bool TransitionState(out TestBotStatusEnum id);

        /// <summary>
        /// ��ȡ�����ײ
        /// </summary>
        protected Transform GetPlayerCollider()
        {
            return Physics2D.OverlapCircle(this._self.transform.position, radius, 1 << LayerMask.NameToLayer("Player"))?.transform;
        }
    }


    /// <summary>
    /// ����״̬
    /// </summary>
    public class TestBotIdleState : TestBotState
    {
        private float relaxTime;

        public TestBotIdleState(GameObject self) : base(self, TestBotStatusEnum.Idle)
        {
        }

        public override void OnEnter()
        {
            relaxTime = 0;
        }

        public override void OnUpdate()
        {
            relaxTime += Time.deltaTime;
        }

        public override bool TransitionState(out TestBotStatusEnum id)
        {
            if (GetPlayerCollider() != null)
            {
                //��⵽�����׷��
                id = TestBotStatusEnum.Chase;
                return true;
            }
            else if (relaxTime >= 3)
            {
                //��Ϣ����3�����л�������
                id = TestBotStatusEnum.Walk;
                return true;
            }

            id = TestBotStatusEnum.Idle;
            return false;
        }
    }

    /// <summary>
    /// ����״̬
    /// </summary>
    public class TestBotWalkState : TestBotState
    {
        private List<Transform> _pathList;

        private int currentPathIndex = 0;
        private bool _isMatchPositon = false;

        public TestBotWalkState(GameObject self, List<Transform> pathList)
            : base(self, TestBotStatusEnum.Walk)
        {
            this._self = self;
            this._pathList = pathList;
        }

        public override void OnEnter()
        {
            _isMatchPositon = false;
        }

        public override void OnUpdate()
        {
            if (_pathList.Count <= 1) return;

            var currentTargetTransform = _pathList[currentPathIndex];

            var direction = (currentTargetTransform.position - this._self.transform.position).normalized;
            this._self.transform.Translate(direction * Time.deltaTime);

            //�ж��Ƿ񵽴�Ŀ�겢�л�����һ��·����
            if (Vector3.Distance(this._self.transform.position, currentTargetTransform.position) <= 0.1f)
            {
                currentPathIndex = (currentPathIndex + 1) % _pathList.Count;
                //�л�Ϊ����
                _isMatchPositon = true;
            }
        }

        public override bool TransitionState(out TestBotStatusEnum id)
        {
            if (GetPlayerCollider() != null)
            {
                //��⵽�����׷��
                id = TestBotStatusEnum.Chase;
                return true;
            }
            else if (_isMatchPositon)
            {
                //����ѵ����,��Ϣһ��
                id = TestBotStatusEnum.Idle;
                return true;
            }

            id = TestBotStatusEnum.Walk;
            return false;
        }
    }


    /// <summary>
    /// ׷��״̬
    /// </summary>
    public class TestBotChaseState : TestBotState
    {
        private Transform _target;

        public TestBotChaseState(GameObject self) : base(self, TestBotStatusEnum.Chase)
        {
        }

        public override void OnEnter()
        {
            _target = GetPlayerCollider();
        }

        public override void OnUpdate()
        {
            if (_target == null) return;

            var direction = (_target.position - this._self.transform.position).normalized;
            this._self.transform.Translate(direction * Time.deltaTime);
        }

        public override void OnExit()
        {
            _target = null;
        }

        public override bool TransitionState(out TestBotStatusEnum id)
        {
            //ͨ����ײ����ж��Ƿ񳬹���ⷶΧ
            if (GetPlayerCollider() == null)
            {
                id = TestBotStatusEnum.Idle;
                return true;
            }

            id = TestBotStatusEnum.Chase;
            return false;
        }
    }

    #endregion


    #region ϵͳ������
    public class FsmSystem<T> where T : Enum
    {
        //״̬����
        private Dictionary<T, IState<T>> _stateDict;
        //��ǰ״̬�봦����
        private T _currentStateId;
        private IState<T> _currentState;

        #region Properites
        //��ǰid
        public T CurrentId => _currentStateId;
        #endregion

        #region Ctor
        public FsmSystem()
        {
            _stateDict = new Dictionary<T, IState<T>>();
        }
        #endregion

        //���״̬
        public void Add(IState<T> state)
        {
            if (state == null) return;
            if (_stateDict.ContainsKey(state.Id)) return;
            _stateDict.Add(state.Id, state);
        }

        //�Ƴ�״̬
        public void Remove(T id)
        {
            if (!_stateDict.ContainsKey(id)) return;
            _stateDict.Remove(id);
        }

        //����״̬
        public bool Enable(T id)
        {
            if (!_stateDict.ContainsKey(id)) return false;

            _currentStateId = id;
            _currentState = _stateDict[id];

            _currentState.OnEnter();
            return true;
        }

        //״̬�ڲ�ִ��
        public void Update()
        {
            //�ж��Ƿ���Ҫ�л��������Ҫ���л�״̬
            if (_currentState.TransitionState(out T id))
                TransferState(id);

            //����״̬����
            _currentState.OnUpdate();
        }

        /// <summary>
        /// ״̬�任
        /// </summary>
        /// <param name="id"></param>
        private void TransferState(T id)
        {
            if (!_stateDict.ContainsKey(id)) return;
            //�˳�֮ǰ״̬�������µ�״̬
            _currentState.OnExit();
            _currentState = _stateDict[id];
            _currentStateId = id;
            _currentState.OnEnter();
        }
    }

    #endregion

    public class FsmFactory
    {
        public static FsmSystem<TestBotStatusEnum> CreateTestBotFsm(GameObject obj, List<Transform> pathList)
        {
            var fsm = new FsmSystem<TestBotStatusEnum>();

            //����״̬
            var idleState = new TestBotIdleState(obj);
            var walkState = new TestBotWalkState(obj, pathList);
            var chaseState = new TestBotChaseState(obj);

            fsm.Add(idleState);
            fsm.Add(walkState);
            fsm.Add(chaseState);

            fsm.Enable(TestBotStatusEnum.Idle);
            return fsm;
        }
    }
}