using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    public class HsmSelectTransitionTool
    {

        public static void SelectTransition(List<HsmConfigNode> nodeList)
        {
            Event _event = Event.current;
            if (_event.type != EventType.MouseDown || (_event.button != 0)) // 鼠标左键
            {
                return;
            }

            Vector3 mousePos = _event.mousePosition;
            for (int i = 0; i < nodeList.Count; i++)
            {
                HsmConfigNode nodeValue = nodeList[i];
                if( TraverseTransition(nodeValue, mousePos))
                {
                    return;
                }
            }

            HsmDataController.Instance.CurrentTransitionId = -1;
        }

        private static bool TraverseTransition(HsmConfigNode nodeValue, Vector3 mousePos)
        {
            for (int j = 0; j < nodeValue.transitionList.Count; ++j)
            {
                HsmConfigTransition transition = nodeValue.transitionList[j];
                if(ChcekTransition(nodeValue, transition, mousePos))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ChcekTransition(HsmConfigNode nodeValue, HsmConfigTransition transition, Vector3 mousePos)
        {
            bool select = false;
            int toId = transition.toStateId;
            HsmConfigNode toNode = HsmDataController.Instance.GetNode(toId);
            if (null == toNode)
            {
                return select;
            }

            int transitionId = HsmDataController.Instance.NodeTransitionID(nodeValue.id, transition.transitionId);
            Vector3 startPos = Vector3.zero;
            Vector3 endPos = Vector3.zero;
            DrawNodeCurveTools.CalculateTranstion(nodeValue.position, toNode.position, ref startPos, ref endPos);

            Vector3 AB = endPos - startPos;
            Vector3 AP = mousePos - startPos;
            Vector3 BP = mousePos - endPos;
            float dotAP_AB = Vector3.Dot(AP, AB.normalized);
            float dotBP_BA = Vector3.Dot(BP, (AB * -1).normalized);
            if (dotAP_AB < 0 || dotBP_BA < 0)
            {
                return select;
            }

            float distance = Vector3.Cross(AB, AP).magnitude / AB.magnitude;
            select = (distance < 10) && (Mathf.Abs(dotAP_AB) < AB.magnitude);
            if (select)
            {
                int id = HsmDataController.Instance.NodeTransitionID(nodeValue.id, transition.transitionId);
                HsmDataController.Instance.CurrentTransitionId = id;
                HsmDataController.Instance.CurrentSelectId = nodeValue.id;
            }
            return select;
        }

    }
}

