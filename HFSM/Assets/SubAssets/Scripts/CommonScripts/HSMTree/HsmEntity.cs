using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace HSMTree
{
    public class HsmEntity
    {
        private HSMStateMachine _hsmStateMachine = null;
        private IConditionCheck  _iconditionCheck = null;
        private int _entityId;

        public HsmEntity(HsmConfigTreeData data)
        {
            _iconditionCheck = new ConditionCheck();
            HSMAnalysis analysis = new HSMAnalysis();
            analysis.Analysis(data, _iconditionCheck, ref _hsmStateMachine);

            _entityId = _hsmStateMachine.EntityId;
        }

        public IConditionCheck ConditionCheck
        {
            get { return (ConditionCheck)_iconditionCheck; }
        }

        public HSMStateMachine HsmStateMachine
        {
            get { return _hsmStateMachine; }
        }

        public void Execute()
        {
            if (null != _hsmStateMachine)
            {
                _hsmStateMachine.Execute();
            }
        }

        public void Clear()
        {
            ConditionCheck.InitParmeter();
        }

        public int EntityId
        {
            get { return _entityId; }
        }

    }
}


