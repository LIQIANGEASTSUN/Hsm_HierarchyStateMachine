using GraphicTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{

    public class HsmNodeDraw
    {
        private List<HsmConfigNode> _nodeList;

        public HsmNodeDraw()
        {

        }

        public void SetNodeList(List<HsmConfigNode> nodeList)
        {
            _nodeList = nodeList;
        }

        public void NodeDrawCallBack()
        {
            for (int i = 0; i < _nodeList.Count; i++)
            {
                HsmConfigNode nodeValue = _nodeList[i];
                int index = EnumNames.GetEnumIndex<HSM_NODE_TYPE>((HSM_NODE_TYPE)nodeValue.nodeType);
                string name = EnumNames.GetEnumName<HSM_NODE_TYPE>(index);
                name = string.Format("{0}_{1}", name, nodeValue.id);
                Rect rect = GUI.Window(i, RectTool.RectTToRect(nodeValue.position), DrawNodeWindow, name);
                nodeValue.position = RectTool.RectToRectT(rect);
                DrawToChildCurve(nodeValue);
            }
        }

        private void DrawNodeWindow(int id)
        {
            if (id >= _nodeList.Count)
            {
                return;
            }
            HsmConfigNode nodeValue = _nodeList[id];
            HSMNodeEditor.Draw(nodeValue, HsmDataController.Instance.CurrentSelectId, HsmDataController.Instance.DefaultStateId);
            GUI.DragWindow();
        }


        /// 每帧绘制从 节点到所有子节点的连线
        private void DrawToChildCurve(HsmConfigNode nodeValue)
        {
            for (int i = nodeValue.transitionList.Count - 1; i >= 0; --i)
            {
                int toId = nodeValue.transitionList[i].toStateId;
                HsmConfigNode toNode = HsmDataController.Instance.GetNode(toId);

                int transitionId = HsmDataController.Instance.NodeTransitionID(nodeValue.id, nodeValue.transitionList[i].transitionId);
                Color color = (transitionId == HsmDataController.Instance.CurrentTransitionId) ? (Color)(new Color32(90, 210, 111, 255)) : Color.black;
                DrawNodeCurveTools.DrawNodeCurve(nodeValue.position, toNode.position, color);
            }
        }
    }

}
