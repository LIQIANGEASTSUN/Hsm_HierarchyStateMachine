using GraphicTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    public class NodeTransitionHandle
    {

        public void NodeAddTransition(int fromId, int toId)
        {
            HsmConfigNode fromNode = HsmDataController.Instance.GetNode(fromId);
            HsmConfigNode toNode = HsmDataController.Instance.GetNode(toId);
            if (null == fromNode || null == toNode)
            {
                Debug.LogError("node is null");
                return;
            }

            for (int i = 0; i < fromNode.transitionList.Count; ++i)
            {
                HsmConfigTransition temp = fromNode.transitionList[i];
                if (temp.toStateId == toNode.id)
                {
                    return;
                }
            }

            int transitionId = fromNode.transitionList.Count;
            for (int i = 0; i < fromNode.transitionList.Count; ++i)
            {
                HsmConfigTransition temp = fromNode.transitionList.Find((a) => {
                    return i == a.transitionId;
                });

                if (null == temp)
                {
                    transitionId = i;
                    break;
                }
            }

            HsmConfigTransition transition = new HsmConfigTransition();
            transition.transitionId = transitionId;
            transition.toStateId = toNode.id;
            fromNode.transitionList.Add(transition);
        }

        public void NodeDelTransition(int fromId, int toId)
        {
            HsmConfigNode fromNode = HsmDataController.Instance.GetNode(fromId);
            HsmConfigNode toNode = HsmDataController.Instance.GetNode(toId);

            if (null == fromNode || null == toNode)
            {
                Debug.LogError("node is null");
                return;
            }

            for (int i = 0; i < fromNode.transitionList.Count; ++i)
            {
                HsmConfigTransition temp = fromNode.transitionList[i];
                if (temp.toStateId == toNode.id)
                {
                    fromNode.transitionList.RemoveAt(i);
                    break;
                }
            }
        }

        private HsmConfigTransition GetTransition(int stateId, int transitionId)
        {
            HsmConfigTransition transition = null;
            HsmConfigNode nodeValue = HsmDataController.Instance.GetNode(stateId);
            if (null == nodeValue)
            {
                return transition;
            }

            for (int i = 0; i < nodeValue.transitionList.Count; ++i)
            {
                if (nodeValue.transitionList[i].transitionId == transitionId)
                {
                    transition = nodeValue.transitionList[i];
                    break;
                }
            }

            return transition;
        }

        public void NodeTransitionAddGroup(int stateId, int transitionId)
        {
            HsmConfigNode nodeValue = HsmDataController.Instance.GetNode(stateId);
            if (null == nodeValue)
            {
                return;
            }

            HsmConfigTransition transition = GetTransition(stateId, transitionId);
            if (null == transition)
            {
                return;
            }

            int groupId = transition.groupList.Count;
            for (int i = 0; i < transition.groupList.Count; ++i)
            {
                ConditionGroup temp = transition.groupList.Find(a => a.index == i);
                if (null == temp)
                {
                    groupId = i;
                    break;
                }
            }

            ConditionGroup transitionGroup = new ConditionGroup();
            transitionGroup.index = groupId;
            transition.groupList.Add(transitionGroup);
        }

        public void NodeTransitionDelGroup(int stateId, int transitionId, int groupId)
        {
            HsmConfigNode nodeValue = HsmDataController.Instance.GetNode(stateId);
            if (null == nodeValue)
            {
                return;
            }

            for (int i = 0; i < nodeValue.transitionList.Count; ++i)
            {
                HsmConfigTransition transition = null;
                if (nodeValue.transitionList[i].transitionId != transitionId)
                {
                    continue;
                }

                transition = nodeValue.transitionList[i];
                for (int j = 0; j < transition.groupList.Count; ++j)
                {
                    if (transition.groupList[j].index == groupId)
                    {
                        transition.groupList.RemoveAt(j);
                    }
                }
            }
        }

    }

}
