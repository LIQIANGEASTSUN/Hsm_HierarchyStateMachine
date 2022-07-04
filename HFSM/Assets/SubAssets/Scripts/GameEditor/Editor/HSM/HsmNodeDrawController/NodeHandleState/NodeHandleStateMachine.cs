using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    public enum NodeHandlerState
    {
        None,
        Normal,
        MakeTransition,
    }

    public class NodeHandleStateMachine
    {
        private Dictionary<NodeHandlerState, NodeHandleStateBase> _stateDic = new Dictionary<NodeHandlerState, NodeHandleStateBase>();
        private NodeHandleStateBase _currentState;
        public NodeHandleStateMachine()
        {
            _stateDic[NodeHandlerState.Normal] = new NodeHandleStateNormal();
            _stateDic[NodeHandlerState.MakeTransition] = new NodeHandleStateMakeTransition();

            ChangeState(NodeHandlerState.Normal);
        }

        public void OnExecute(HsmConfigNode currentNode, List<HsmConfigNode> nodeList)
        {
            if (null != _currentState)
            {
                _currentState.OnExecute(currentNode, nodeList);
                NodeHandlerState toState = _currentState.ChangeToState();
                ChangeState(toState);    
            }
        }

        private void ChangeState(NodeHandlerState state)
        {
            if (!_stateDic.ContainsKey(state))
            {
                return;
            }

            if (null != _currentState)
            {
                _currentState.OnExit();
            }
            _currentState = _stateDic[state];
            _currentState.OnEnter();
        }
    }
}