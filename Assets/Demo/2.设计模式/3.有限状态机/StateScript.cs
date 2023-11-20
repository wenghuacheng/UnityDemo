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
    /// 状态枚举
    /// </summary>
    public enum TestBotStatusEnum
    {
        Idle,
        Walk,
        Chase
    }

    #region 状态
    /// <summary>
    /// 状态接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IState<T> where T : Enum
    {
        T Id { get; }

        //进入状态
        void OnEnter();

        //状态更新
        void OnUpdate();

        //状态退出
        void OnExit();

        //判断是否需要切换状态
        bool TransitionState(out T id);
    }

    /// <summary>
    /// 测试专题基类
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
        /// 获取玩家碰撞
        /// </summary>
        protected Transform GetPlayerCollider()
        {
            return Physics2D.OverlapCircle(this._self.transform.position, radius, 1 << LayerMask.NameToLayer("Player"))?.transform;
        }
    }


    /// <summary>
    /// 空闲状态
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
                //检测到玩家则追击
                id = TestBotStatusEnum.Chase;
                return true;
            }
            else if (relaxTime >= 3)
            {
                //休息超过3秒则切换到行走
                id = TestBotStatusEnum.Walk;
                return true;
            }

            id = TestBotStatusEnum.Idle;
            return false;
        }
    }

    /// <summary>
    /// 行走状态
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

            //判断是否到达目标并切换到下一个路径点
            if (Vector3.Distance(this._self.transform.position, currentTargetTransform.position) <= 0.1f)
            {
                currentPathIndex = (currentPathIndex + 1) % _pathList.Count;
                //切换为空闲
                _isMatchPositon = true;
            }
        }

        public override bool TransitionState(out TestBotStatusEnum id)
        {
            if (GetPlayerCollider() != null)
            {
                //检测到玩家则追击
                id = TestBotStatusEnum.Chase;
                return true;
            }
            else if (_isMatchPositon)
            {
                //到达训练点,休息一下
                id = TestBotStatusEnum.Idle;
                return true;
            }

            id = TestBotStatusEnum.Walk;
            return false;
        }
    }


    /// <summary>
    /// 追击状态
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
            //通过碰撞检测判断是否超过检测范围
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


    #region 系统管理类
    public class FsmSystem<T> where T : Enum
    {
        //状态容器
        private Dictionary<T, IState<T>> _stateDict;
        //当前状态与处理类
        private T _currentStateId;
        private IState<T> _currentState;

        #region Properites
        //当前id
        public T CurrentId => _currentStateId;
        #endregion

        #region Ctor
        public FsmSystem()
        {
            _stateDict = new Dictionary<T, IState<T>>();
        }
        #endregion

        //添加状态
        public void Add(IState<T> state)
        {
            if (state == null) return;
            if (_stateDict.ContainsKey(state.Id)) return;
            _stateDict.Add(state.Id, state);
        }

        //移除状态
        public void Remove(T id)
        {
            if (!_stateDict.ContainsKey(id)) return;
            _stateDict.Remove(id);
        }

        //开启状态
        public bool Enable(T id)
        {
            if (!_stateDict.ContainsKey(id)) return false;

            _currentStateId = id;
            _currentState = _stateDict[id];

            _currentState.OnEnter();
            return true;
        }

        //状态内部执行
        public void Update()
        {
            //判断是否需要切换，如果需要则切换状态
            if (_currentState.TransitionState(out T id))
                TransferState(id);

            //更新状态操作
            _currentState.OnUpdate();
        }

        /// <summary>
        /// 状态变换
        /// </summary>
        /// <param name="id"></param>
        private void TransferState(T id)
        {
            if (!_stateDict.ContainsKey(id)) return;
            //退出之前状态并进入新的状态
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

            //生成状态
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