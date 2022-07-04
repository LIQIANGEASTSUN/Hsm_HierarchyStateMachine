using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    public interface IStateMachineTransition
    {
        void Execute();

        void ChangeNode(int toNodeId);

        void ChangeState(int id);

        void ChangeSubMachine(int id);

        void Clear();

    }

    public class HSMStateMachineTransition : IStateMachineTransition
    {
        private HSMStateBase _currentState;
        private HSMSubStateMachine _currentSubMachine = null;

        private HSMStateEntry _stateEntry;
        private HSMStateExit _stateExit;
        private Dictionary<int, HSMStateBase> _stateDic = new Dictionary<int, HSMStateBase>();
        private Dictionary<int, HSMSubStateMachine> _subMachineDic = new Dictionary<int, HSMSubStateMachine>();

        public void Execute()
        {
            int toNodeId = 0;
            if (null != _currentState)
            {
                _currentState.Execute();
                if (_currentState.Transition(ref toNodeId))
                {
                    ChangeNode(toNodeId);
                }
            }

            if (null == _currentState && null == _currentSubMachine)
            {
                _stateEntry.Execute();
                if (_stateEntry.Transition(ref toNodeId))
                {
                    ChangeNode(toNodeId);
                }
            }

            if (null != _currentSubMachine)
            {
                _currentSubMachine.Execute();
                if (_currentSubMachine.Transition(ref toNodeId))
                {
                    ChangeNode(toNodeId);
                }
                else
                {
                    _currentSubMachine.StateMachineTransition.Execute();
                }
            }
        }

        /// <summary>
        /// 切换到想要的状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool ChangeDestinationNode(HSMStateBase state)
        {
            bool result = false;
            if (null == state)
            {
                return result;
            }

            if (_stateDic.ContainsKey(state.NodeId))
            {
                ChangeState(state.NodeId);
                result = true;
                return result;
            }
            else
            {
                // 查找想去的状态在哪个地方
                HSMSubStateMachine parentSubMachine = state.ParentSubMachine;
                int toSubMachineId = -1;
                while (null != parentSubMachine)
                {
                    int id = parentSubMachine.NodeId;
                    if (_subMachineDic.ContainsKey(id))
                    {
                        toSubMachineId = id;
                        parentSubMachine = null;
                        break;
                    }

                    parentSubMachine = parentSubMachine.ParentSubMachine;
                }

                if (toSubMachineId >= 0)
                {
                    if (null != _currentSubMachine && _currentSubMachine.NodeId != toSubMachineId)
                    {
                        ChangeSubMachine(toSubMachineId);
                    }
                    if (null != _currentSubMachine && _currentSubMachine.NodeId == toSubMachineId)
                    {
                        result = _currentSubMachine.StateMachineTransition.ChangeDestinationNode(state);
                    }
                }
            }

            return result;
        }

        public void ChangeNode(int toNodeId)
        {
            // Debug.LogError("ChangeNode:" + toNodeId);
            if (IsState(toNodeId))
            {
                HSMStateBase toState = GetState(toNodeId);
                bool needChange = (null == _currentState || _currentState.NodeId != toState.NodeId);
                if (needChange)
                {
                    ChangeState(toNodeId);
                }
            }
            else if (IsSubMachine(toNodeId))
            {
                bool needChange = ((null == _currentSubMachine) || (_currentSubMachine.NodeId != toNodeId));
                ChangeSubMachine(toNodeId);
            }
            else if (null != _stateExit && _stateExit.NodeId == toNodeId)
            {
                ChangeExit();
            }
        }

        public void ChangeState(int id)
        {
            HSMStateBase newState = null;
            if (!_stateDic.TryGetValue(id, out newState) || null == newState)
            {
                return;
            }

            if (null != _currentState)
            {
                _currentState.OnExit();
            }

            _currentState = newState;
            _currentState.OnEnter();

            if (null != _currentSubMachine)
            {
                _currentSubMachine.OnExit();
                _currentSubMachine = null;
            }
        }

        public void ChangeSubMachine(int id)
        {
            HSMSubStateMachine newSubMachine = null;
            if (!_subMachineDic.TryGetValue(id, out newSubMachine) || null == newSubMachine)
            {
                return;
            }

            if (null != _currentSubMachine)
            {
                _currentSubMachine.OnExit();
            }

            _currentSubMachine = newSubMachine;
            _currentSubMachine.OnEnter();

            if (null != _currentState)
            {
                _currentState.OnExit();
                _currentState = null;
            }
        }

        public void ChangeExit()
        {
            ExitState();
            _stateExit.OnEnter();
        }

        private void ExitState()
        {
            if (null != _currentState)
            {
                _currentState.OnExit();
                _currentState = null;
            }

            if (null != _currentSubMachine)
            {
                _currentSubMachine.OnExit();
                _currentSubMachine = null;
            }
        }

        public HSMStateBase GetState(int stateId)
        {
            HSMStateBase state = null;
            if (_stateDic.TryGetValue(stateId, out state))
            {
                return state;
            }

            return state;
        }

        public void AddNode(HSMStateBase node)
        {
            if (node.NodeType() == (int)HSM_NODE_TYPE.ENTRY)
            {
                _stateEntry = (HSMStateEntry)node;
            }
            else if (node.NodeType() == (int)HSM_NODE_TYPE.EXIT)
            {
                _stateExit = (HSMStateExit)node;
            }
            else if (node.NodeType() == (int)HSM_NODE_TYPE.STATE)
            {
                _stateDic[node.NodeId] = (HSMStateBase)node;
            }
            else if (node.NodeType() == (int)HSM_NODE_TYPE.SUB_STATE_MACHINE)
            {
                _subMachineDic[node.NodeId] = (HSMSubStateMachine)node;
            }
        }

        public bool IsState(int stateId)
        {
            return _stateDic.ContainsKey(stateId);
        }

        public bool IsSubMachine(int stateId)
        {
            return _subMachineDic.ContainsKey(stateId);
        }

        public void Clear()
        {
            foreach(var kv in _subMachineDic)
            {
                HSMStateBase node = kv.Value;
                HSMSubStateMachine subMachine = (HSMSubStateMachine)node;
                //UnityEngine.Debug.LogError("Clear:" + subMachine.NodeId);
                subMachine.Clear();
            }

            ExitState();
        }
    }
}

