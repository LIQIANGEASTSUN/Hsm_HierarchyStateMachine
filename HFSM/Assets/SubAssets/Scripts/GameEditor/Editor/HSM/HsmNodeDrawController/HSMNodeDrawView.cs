using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HSMTree
{
    public class HsmNodeDrawView
    {
        private HsmNodeDrawController _drawController = null;
        private HsmNodeDrawModel _drawModel = null;
        private HsmNodeDraw _hsmNodeDraw = null;
        private NodeHandleStateMachine _nodeHandleStateMachine;

        private Vector3 scrollPos = Vector2.zero;
        private Rect scrollRect = new Rect(0, 0, 1500, 1000);
        private Rect contentRect = new Rect(0, 0, 3000, 2000);

        public void Init(HsmNodeDrawController drawController, HsmNodeDrawModel drawModel)
        {
            _drawController = drawController;
            _drawModel = drawModel;
            _hsmNodeDraw = new HsmNodeDraw();
            _nodeHandleStateMachine = new NodeHandleStateMachine();
        }

        public void Draw(Rect windowsPosition, HsmConfigNode currentNode, List<HsmConfigNode> nodeList)
        {
            DrawTitle();

            Rect rect = GUILayoutUtility.GetRect(0f, 0, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            scrollRect = rect;
            contentRect.x = rect.x;
            contentRect.y = rect.y;

            _hsmNodeDraw.SetNodeList(nodeList);
            //创建 scrollView  窗口  
            scrollPos = GUI.BeginScrollView(scrollRect, scrollPos, contentRect);
            {
                _nodeHandleStateMachine.OnExecute(currentNode, nodeList);
                HSMNodeWindow._drawWindowCallBack(_hsmNodeDraw.NodeDrawCallBack);
                HsmSelectTransitionTool.SelectTransition(nodeList);
                ResetScrollPos(nodeList);
            }
            GUI.EndScrollView();  //结束 ScrollView 窗口  
        }

        private void DrawTitle()
        {
            int selectIndex = 0;
            List<int> idList = new List<int>();
            string[] optionArr = _drawModel.GetOptionArr(ref selectIndex, ref idList);
            int option = selectIndex;

            option = GUILayout.Toolbar(option, optionArr, EditorStyles.toolbarButton, GUILayout.Width(optionArr.Length * 200));
            if (option != selectIndex)
            {
                int nodeId = idList[option];
                HsmDataController.Instance.CurrentOpenSubMachineId = nodeId;
            }
        }

        private void ResetScrollPos(List<HsmConfigNode> nodeList)
        {
            float maxRight = 0;
            float maxBottom = 0;
            for (int i = 0; i < nodeList.Count; ++i)
            {
                HsmConfigNode nodeValue = nodeList[i];
                float right = nodeValue.position.x + nodeValue.position.width + 50;
                float bottom = nodeValue.position.y + nodeValue.position.height + 50;
                if (right > maxRight)
                {
                    maxRight = right;
                    contentRect.width = maxRight;
                }
                if (bottom > maxBottom)
                {
                    maxBottom = bottom;
                    contentRect.height = maxBottom;
                }
            }
        }
    }
}
