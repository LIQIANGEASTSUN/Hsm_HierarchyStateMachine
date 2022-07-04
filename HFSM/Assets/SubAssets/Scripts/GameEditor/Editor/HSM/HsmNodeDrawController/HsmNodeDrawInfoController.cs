using GraphicTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    public class Node_Draw_Info
    {
        public string _nodeTypeName;
        public List<KeyValuePair<string, Node_Draw_Info_Item>> _nodeArr = new List<KeyValuePair<string, Node_Draw_Info_Item>>();

        public Node_Draw_Info(string name)
        {
            _nodeTypeName = name;
        }

        public void AddNodeType(HSM_NODE_TYPE nodeType)
        {
            Node_Draw_Info_Item item = new Node_Draw_Info_Item(nodeType);
            item.GetTypeName();
            string name = string.Format("{0}/{1}", _nodeTypeName, item._nodeName);
            KeyValuePair<string, Node_Draw_Info_Item> kv = new KeyValuePair<string, Node_Draw_Info_Item>(name, item);
            _nodeArr.Add(kv);
        }

        public void AddNodeType(HSM_NODE_TYPE nodeType, string nodeName, string identification)
        {
            Node_Draw_Info_Item item = new Node_Draw_Info_Item(nodeType);
            item.SetName(nodeName);
            item.SetIdentification(identification);
            string name = string.Format("{0}/{1}", _nodeTypeName, nodeName);
            KeyValuePair<string, Node_Draw_Info_Item> kv = new KeyValuePair<string, Node_Draw_Info_Item>(name, item);
            _nodeArr.Add(kv);
        }
    }

    public class Node_Draw_Info_Item
    {
        public string _nodeName = string.Empty;
        public HSM_NODE_TYPE _nodeType;
        public string _identification = string.Empty;

        public Node_Draw_Info_Item(HSM_NODE_TYPE nodeType)
        {
            _nodeType = nodeType;
        }

        public void GetTypeName()
        {
            int index = EnumNames.GetEnumIndex<HSM_NODE_TYPE>(_nodeType);
            _nodeName = EnumNames.GetEnumName<HSM_NODE_TYPE>(index);
        }

        public void SetName(string name)
        {
            _nodeName = name;
        }

        public void SetIdentification(string identification)
        {
            _identification = identification;
        }
    }

    public class HsmNodeDrawInfoController
    {
        private static HsmNodeDrawInfoController Instance;
        private List<Node_Draw_Info> infoList = new List<Node_Draw_Info>();

        public static HsmNodeDrawInfoController GetInstance()
        {
            if (null == Instance)
            {
                Instance = new HsmNodeDrawInfoController();
            }
            return Instance;
        }

        public HsmNodeDrawInfoController()
        {
            Init();
        }

        private void Init()
        {
            SetInfoList();
        }

        private void SetInfoList()
        {
            // 状态节点
            Node_Draw_Info stateDrawInfo = new Node_Draw_Info("CreateState");
            infoList.Add(stateDrawInfo);

            // 子状态机节点
            Node_Draw_Info subMachineDrawInfo = new Node_Draw_Info("Create-SubStateMachine");
            infoList.Add(subMachineDrawInfo);

            Dictionary<string, CustomIdentification> nodeDic = HsmConfigBehaviorNode.Instance.GetNodeDic();
            foreach (var kv in nodeDic)
            {
                CustomIdentification customIdentification = kv.Value;
                if (customIdentification.NodeType == (int)HSM_NODE_TYPE.STATE)
                {
                    stateDrawInfo.AddNodeType(HSM_NODE_TYPE.STATE, customIdentification.Name, customIdentification.IdentificationName);
                }
                else if (customIdentification.NodeType == (int)HSM_NODE_TYPE.SUB_STATE_MACHINE)
                {
                    subMachineDrawInfo.AddNodeType(HSM_NODE_TYPE.SUB_STATE_MACHINE, customIdentification.Name, customIdentification.IdentificationName);
                }
            }
        }

        public List<Node_Draw_Info> InfoList
        {
            get
            {
                return infoList;
            }
        }

    }

}
