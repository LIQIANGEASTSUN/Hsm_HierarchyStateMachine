using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{

    public class HsmTransitionInspectorModel
    {
        public HsmConfigTransition GetCurrentSelectTransition(ref HsmConfigNode nodeValue)
        {
            HsmConfigTransition transition = null;
            int nodeTransitionId = HsmDataController.Instance.CurrentTransitionId;
            int nodeId = HsmDataController.Instance.NodeTransitionIDToNode(nodeTransitionId);
            int transitionId = HsmDataController.Instance.NodeTransitionIDToTransitionId(nodeTransitionId);

            nodeValue = HsmDataController.Instance.GetNode(nodeId);
            if (null == nodeValue)
            {
                return transition;
            }

            foreach (var temp in nodeValue.transitionList)
            {
                if (temp.transitionId == transitionId)
                {
                    transition = temp;
                    break;
                }
            }

            return transition;
        }
    }
}