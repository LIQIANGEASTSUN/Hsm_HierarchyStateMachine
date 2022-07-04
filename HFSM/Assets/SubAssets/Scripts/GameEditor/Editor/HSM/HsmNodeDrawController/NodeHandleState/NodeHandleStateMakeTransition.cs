using GraphicTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    public class NodeHandleStateMakeTransition : NodeHandleStateBase
    {

        public NodeHandleStateMakeTransition() : base(NodeHandlerState.MakeTransition)
        {

        }

        public override void OnExecute(HsmConfigNode currentNode, List<HsmConfigNode> nodeList)
        {
            base.OnExecute(currentNode, nodeList);

            Event _event = Event.current;
            _mousePosition = _event.mousePosition;

            if (_event.type == EventType.MouseDown && (_event.button == 0)) // 鼠标左键
            {
                HsmConfigNode nodeValue = GetMouseInNode(_nodeList);
                // 如果按下鼠标时，选中了一个节点，则将 新选中根节点 添加为 selectNode 的子节点
                bool nodeTypeValid = CheckTranshtion(currentNode, nodeValue);
                if (null != nodeValue && currentNode.id != nodeValue.id && nodeTypeValid)
                {
                    NodeTransitionHandle nodeTransitionHandle = new NodeTransitionHandle();
                    nodeTransitionHandle.NodeAddTransition(currentNode.id, nodeValue.id);
                }
                _changeState = NodeHandlerState.Normal;
            }

            if (null != currentNode)
            {
                RectT mouseRect = new RectT();
                mouseRect.x = _mousePosition.x;
                mouseRect.y = _mousePosition.y;
                mouseRect.width = 10;
                mouseRect.height = 10;
                DrawNodeCurveTools.DrawNodeCurve(currentNode.position, mouseRect, Color.black);
            }
        }

        private bool CheckTranshtion(HsmConfigNode fromNode, HsmConfigNode toNode)
        {
            if (null == toNode)
            {
                return false;
            }
            if (toNode.nodeType == (int)HSM_NODE_TYPE.ENTRY)
            {
                return false;
            }

            if (fromNode.nodeType == (int)HSM_NODE_TYPE.ENTRY && toNode.nodeType == (int)HSM_NODE_TYPE.EXIT)
            {
                return false;
            }

            return true;
        }
    }
}