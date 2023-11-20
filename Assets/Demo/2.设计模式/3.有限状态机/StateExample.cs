using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Patterns
{
    public class StateExample : MonoBehaviour
    {
        [SerializeField] private List<Transform> pathList;

        private FsmSystem<TestBotStatusEnum> _system;

        void Start()
        {
            _system = FsmFactory.CreateTestBotFsm(this.gameObject, pathList);
        }

        // Update is called once per frame
        void Update()
        {
            _system.Update();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(this.transform.position, 5);
        }
    }
}