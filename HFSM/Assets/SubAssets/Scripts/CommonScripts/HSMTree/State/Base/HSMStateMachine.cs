using System.Collections.Generic;

namespace HSMTree
{
    public class HSMStateMachine
    {
        private Dictionary<int, HSMStateBase> _allNodeDic = new Dictionary<int, HSMStateBase>();
        private HSMStateMachineTransition _iStateMachineTransition;
        private int _entityId;

        public HSMStateMachine()
        {
            _iStateMachineTransition = new HSMStateMachineTransition();
        }

        public void Execute()
        {
            _iStateMachineTransition.Execute();
        }

        public void AddAllNode(Dictionary<int, HSMStateBase> abstractNodeDic)
        {
            _allNodeDic = abstractNodeDic;
        }

        public void AddChildNode(HSMStateBase node)
        {
            _iStateMachineTransition.AddNode(node);
        }

        public void SetDefaultStateId(int id)
        {
            //_defaultStateId = id;
        }

        public void Clear()
        {
            _iStateMachineTransition.Clear();
        }

        public HSMStateMachineTransition StateMachineTransition
        {
            get { return _iStateMachineTransition; }
        }

        public HSMStateBase GetNode(int nodeId)
        {
            HSMStateBase node = null;
            if (_allNodeDic.TryGetValue(nodeId, out node))
            {
                return node;
            }

            return node;
        }

        public Dictionary<int, HSMStateBase> AllNode()
        {
            return _allNodeDic;
        }

        public int EntityId
        {
            get { return _entityId; }
            set { _entityId = value; }
        }

    }
}
