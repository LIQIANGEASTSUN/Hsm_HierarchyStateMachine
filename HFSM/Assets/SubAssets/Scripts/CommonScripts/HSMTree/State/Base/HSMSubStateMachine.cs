
namespace HSMTree
{
    public class HSMSubStateMachine : HSMStateBase
    {
        private HSMStateMachineTransition _iStateMachineTransition;

        public HSMSubStateMachine() : base()
        {
            _iStateMachineTransition = new HSMStateMachineTransition();
            _nodeType = HSM_NODE_TYPE.SUB_STATE_MACHINE;
        }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {
            _iStateMachineTransition.ChangeExit();
        }

        public override void AddChildNode(HSMStateBase node)
        {
            _iStateMachineTransition.AddNode(node);
        }

        public void SetDefaultStateId(int id)
        {
            //_defaultStateId = id;
        }

        public virtual void Clear()
        {
            _iStateMachineTransition.Clear();
        }

        public override void SetParentSubMachine(HSMStateBase node)
        {
            _parentSubMachine = (HSMSubStateMachine)node;
        }

        public HSMStateMachineTransition StateMachineTransition
        {
            get { return _iStateMachineTransition; }
        }

    }
}

