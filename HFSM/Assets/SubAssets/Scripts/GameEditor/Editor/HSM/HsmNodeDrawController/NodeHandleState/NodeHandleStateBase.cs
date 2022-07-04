using GraphicTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    public abstract class NodeHandleStateBase
    {
        protected NodeHandlerState _state;
        protected Vector2 _mousePosition;
        protected List<HsmConfigNode> _nodeList;
        protected NodeHandlerState _changeState = NodeHandlerState.None;
        public NodeHandleStateBase(NodeHandlerState state)
        {
            _state = state;
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExecute(HsmConfigNode currentNode, List<HsmConfigNode> nodeList)
        {
            _nodeList = nodeList;
        }

        public virtual void OnExit()
        {
            _changeState = NodeHandlerState.None;
        }

        // 获取鼠标所在位置的节点
        protected HsmConfigNode GetMouseInNode(List<HsmConfigNode> nodeList)
        {
            HsmConfigNode selectNode = null;
            for (int i = 0; i < nodeList.Count; i++)
            {
                HsmConfigNode nodeValue = nodeList[i];
                // 如果鼠标位置 包含在 节点的 Rect 范围，则视为可以选择的节点
                if (RectTool.RectTToRect(nodeValue.position).Contains(_mousePosition))
                {
                    selectNode = nodeValue;
                    break;
                }
            }

            return selectNode;
        }

        public NodeHandlerState ChangeToState()
        {
            return _changeState;
        }

    }
}

