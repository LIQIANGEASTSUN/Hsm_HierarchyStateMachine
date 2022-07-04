using GraphicTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    public class DataAddNodeHandle
    {
        public DataAddNodeHandle()
        {

        }

        // 添加节点
        public void AddNode(Node_Draw_Info_Item info, Vector3 mousePosition, int subMachineId)
        {
            AddNode(info._nodeName, info._identification, info._nodeType, mousePosition, subMachineId);
        }

        public void AddNode(string nodeName, string identification, HSM_NODE_TYPE nodeType, Vector3 position, int subMachineId)
        {
            HsmConfigNode newNodeValue = new HsmConfigNode();
            newNodeValue.id = GetNewstateId();

            newNodeValue.nodeName = nodeName;
            newNodeValue.identification = identification;
            newNodeValue.nodeType = (int)nodeType;

            Rect rect = new Rect(position.x, position.y, 150, 50);
            newNodeValue.position = RectTool.RectToRectT(rect);

            if (subMachineId >= 0)
            {
                newNodeValue.parentId = subMachineId;

                HsmConfigNode subMachineNode = HsmDataController.Instance.GetNode(subMachineId);
                if (subMachineNode == null || subMachineNode.nodeType != (int)HSM_NODE_TYPE.SUB_STATE_MACHINE)
                {
                    Debug.LogError("Node is not SubMachine:" + subMachineId);
                    return;
                }

                subMachineNode.childIdList.Add(newNodeValue.id);
            }

            HsmDataController.Instance.NodeList.Add(newNodeValue);

            if (HsmDataController.Instance.HSMTreeData.defaultStateId < 0)
            {
                HsmDataController.Instance.SetDefaultState(newNodeValue);
            }

            if (nodeType == HSM_NODE_TYPE.SUB_STATE_MACHINE)
            {
                AddNode("Entry", typeof(HSMStateEntry).Name, HSM_NODE_TYPE.ENTRY, new Vector3(900, 150, 0), newNodeValue.id);
                AddNode("Exit", typeof(HSMStateExit).Name, HSM_NODE_TYPE.EXIT, new Vector3(900, 550, 0), newNodeValue.id);
            }
        }

        private int GetNewstateId()
        {
            int id = -1;
            int index = -1;
            while (id == -1)
            {
                ++index;
                id = index;
                for (int i = 0; i < HsmDataController.Instance.NodeList.Count; ++i)
                {
                    if (HsmDataController.Instance.NodeList[i].id == index)
                    {
                        id = -1;
                    }
                }
            }

            return id;
        }



    }

}
