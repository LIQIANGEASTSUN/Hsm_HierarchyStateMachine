using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HSMTree
{
    public class NodeHandleStateNormal : NodeHandleStateBase
    {

        public NodeHandleStateNormal() : base(NodeHandlerState.Normal)
        {

        }

        public override void OnExecute(HsmConfigNode currentNode, List<HsmConfigNode> nodeList)
        {
            base.OnExecute(currentNode, nodeList);

            Event _event = Event.current;
            if (_event.type != EventType.MouseDown)
            {
                return;
            }

            _mousePosition = _event.mousePosition;
            if (_event.button == 0) // 鼠标左键
            {
                HsmConfigNode nodeValue = GetMouseInNode(_nodeList);
                ClickNode(nodeValue);
            }
            else if (_event.button == 1) // 鼠标右键
            {
                HsmConfigNode nodeValue = GetMouseInNode(_nodeList);
                ShowMenu(currentNode, nodeValue);
            }
        }

        private int lastClickNodeTime = 0;
        private void ClickNode(HsmConfigNode nodeValue)
        {
            if (null == nodeValue)
            {
                return;
            }
            int nodeId = (null != nodeValue) ? nodeValue.id : -1;
            HsmDataController.Instance.CurrentSelectId = nodeId;

            if (nodeValue.nodeType == (int)HSM_NODE_TYPE.SUB_STATE_MACHINE)
            {
                int currentTime = (int)(Time.realtimeSinceStartup * 1000);
                if (currentTime - lastClickNodeTime <= 200)
                {
                    HsmDataController.Instance.CurrentOpenSubMachineId = nodeId;
                }
                lastClickNodeTime = (int)(Time.realtimeSinceStartup * 1000);
            }
        }

        private void ShowMenu(HsmConfigNode currentNode, HsmConfigNode nodeValue)
        {
            if (null == nodeValue)
            {
                MouseRightDownEmptyNode();
            }
            else
            {
                MouseRightDownOnNode(currentNode, nodeValue);
            }
            Event.current.Use();
        }

        private void MouseRightDownEmptyNode()
        {
            GenericMenu menu = new GenericMenu();
            List<Node_Draw_Info> nodeList = HsmNodeDrawInfoController.GetInstance().InfoList;
            for (int i = 0; i < nodeList.Count; ++i)
            {
                Node_Draw_Info draw_Info = nodeList[i];
                for (int j = 0; j < draw_Info._nodeArr.Count; ++j)
                {
                    KeyValuePair<string, Node_Draw_Info_Item> kv = draw_Info._nodeArr[j];
                    string itemName = string.Format("{0}", kv.Key);
                    menu.AddItem(new GUIContent(itemName), false, AddNodeCallBack, kv.Value);
                }
            }
            menu.ShowAsContext();
        }

        private void AddNodeCallBack(object userData)
        {
            int subMachineId = -1;
            if (HsmDataController.Instance.CurrentOpenSubMachineId >= 0)
            {
                subMachineId = HsmDataController.Instance.CurrentOpenSubMachineId;
            }
            DataAddNodeHandle dataAddNodeHandle = new DataAddNodeHandle();
            dataAddNodeHandle.AddNode((Node_Draw_Info_Item)userData, _mousePosition, subMachineId);
        }

        private void MouseRightDownOnNode(HsmConfigNode currentNode, HsmConfigNode nodeValue)
        {
            GenericMenu menu = new GenericMenu();
            if (null != currentNode && nodeValue.id == currentNode.id && nodeValue.nodeType != (int)HSM_NODE_TYPE.EXIT)
            {
                // 连线子节点
                menu.AddItem(new GUIContent("Make Transition"), false, MakeTransition);
                menu.AddSeparator("");
            }

            if (null != currentNode && (currentNode.nodeType != (int)HSM_NODE_TYPE.ENTRY && (currentNode.nodeType != (int)HSM_NODE_TYPE.EXIT)))
                // 删除节点
                menu.AddItem(new GUIContent("Delete State"), false, DeleteNode);

            if (nodeValue.nodeType != (int)HSM_NODE_TYPE.ENTRY && nodeValue.nodeType != (int)HSM_NODE_TYPE.EXIT)
            {
                // 设置默认节点
                menu.AddItem(new GUIContent("Set Default State"), false, SetDefaultState);
            }
            menu.ShowAsContext();
        }

        // 连线子节点
        private void MakeTransition()
        {
            _changeState = NodeHandlerState.MakeTransition;
        }

        // 删除节点
        private void DeleteNode()
        {
            HsmConfigNode nodeValue = GetMouseInNode(_nodeList);

            if (!EditorUtility.DisplayDialog("提示", "确定要删除节点吗", "Yes", "No"))
            {
                return;
            }

            DataDeleteNodeHandle dataDeleteNodeHandle = new DataDeleteNodeHandle();
            dataDeleteNodeHandle.DeleteNode(nodeValue.id);
        }

        private void SetDefaultState()
        {
            HsmConfigNode nodeValue = GetMouseInNode(_nodeList);
            if (null == nodeValue)
            {
                return;
            }

            HsmDataController.Instance.SetDefaultState(nodeValue.id);
        }
    }
}
