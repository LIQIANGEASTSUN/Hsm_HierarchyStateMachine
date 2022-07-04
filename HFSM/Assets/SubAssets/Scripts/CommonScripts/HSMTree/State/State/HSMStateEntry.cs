
namespace HSMTree
{
    public class HSMStateEntry : HSMStateBase
    {
        public HSMStateEntry() :base()
        {
            _nodeType = HSM_NODE_TYPE.ENTRY;
        }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }
    }
}