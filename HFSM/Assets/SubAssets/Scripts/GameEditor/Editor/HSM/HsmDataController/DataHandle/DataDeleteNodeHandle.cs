using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    public class DataDeleteNodeHandle
    {

        // 删除节点
        public void DeleteNode(int stateId)
        {
            HsmConfigNode delNode = HsmDataController.Instance.GetNode(stateId);
            if (null == delNode)
            {
                return;
            }

            List<HsmConfigNode> delList = new List<HsmConfigNode>() { };
            HsmDataController.Instance.GetNodeAllChild(stateId, delList);
            delList.Add(delNode);
            HashSet<int> delHash = new HashSet<int>();
            for (int i = 0; i < delList.Count; ++i)
            {
                delHash.Add(delList[i].id);
                //Debug.LogError(delList[i].Id);
            }

            for (int i = HsmDataController.Instance.NodeList.Count - 1; i >= 0; --i)
            {
                HsmConfigNode nodeValue = HsmDataController.Instance.NodeList[i];
                for (int j = 0; j < nodeValue.transitionList.Count; ++j)
                {
                    HsmConfigTransition transition = nodeValue.transitionList[j];
                    if (delHash.Contains(transition.toStateId))
                    {
                        nodeValue.transitionList.RemoveAt(j);
                    }
                }

                for (int j = nodeValue.childIdList.Count - 1; j >= 0; --j)
                {
                    int childId = nodeValue.childIdList[j];
                    if (delHash.Contains(childId))
                    {
                        nodeValue.childIdList.RemoveAt(j);
                    }
                }

                if (delHash.Contains(nodeValue.id))
                {
                    HsmDataController.Instance.NodeList.RemoveAt(i);
                    //Debug.LogError("Remove:" + nodeValue.Id);
                }
            }
        }

    }
}
