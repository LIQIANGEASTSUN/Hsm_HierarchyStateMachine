using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HSMTree
{
    public class HSMStateExit : HSMStateBase
    {
        public HSMStateExit() : base()
        {
            _nodeType = HSM_NODE_TYPE.EXIT;
        }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }
    }
}