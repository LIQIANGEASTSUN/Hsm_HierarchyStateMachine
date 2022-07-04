using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GraphicTree;

namespace HSMTree
{
    public class HsmTransitionInspectorView
    {
        private HsmDrawNodeParameter hsmDrawNodeParameter;
        private HsmConfigNode _nodeData;
        private HsmConfigTransition _transition;
        public HsmTransitionInspectorView()
        {
            hsmDrawNodeParameter = new HsmDrawNodeParameter();
        }

        public void Draw(HsmConfigNode nodeData, HsmConfigTransition transition)
        {
            if (null == nodeData)
            {
                EditorGUILayout.LabelField("未选择节点");
                return;
            }

            if (null == transition)
            {
                EditorGUILayout.LabelField("未选择转换连线");
                return;
            }

            _nodeData = nodeData;
            _transition = transition;

            EditorGUILayout.BeginVertical("box");
            {
                DrawNode(nodeData);
                DrawTransition(nodeData, transition);
                DrawTransitionParameter(nodeData, transition);
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawNode(HsmConfigNode nodeData)
        {
            string name = string.Format("节点id:{0}  {1}", nodeData.id, nodeData.nodeName);
            EditorGUILayout.LabelField(name);
        }

        private Dictionary<int, int> _groupColorDic = new Dictionary<int, int>(); // 
        private Color32[] colorArr = new Color32[] { new Color32(178, 226, 221, 255), new Color32(220, 226, 178, 255), new Color32(209, 178, 226, 255), new Color32(178, 185, 226, 255) };
        private Vector2 scrollPos = Vector2.zero;
        private void DrawTransition(HsmConfigNode nodeData, HsmConfigTransition transition)
        {
            EditorGUILayout.LabelField("选择转换连线");
            for (int i = 0; i < nodeData.transitionList.Count; ++i)
            {
                HsmConfigTransition temp = nodeData.transitionList[i];
                bool select = temp.transitionId == transition.transitionId;
                string name = string.Format("节点:{0} -> 节点:{1}", nodeData.id, temp.toStateId);
                GUI.backgroundColor = select ? new Color(0, 1, 0, 1) : Color.white;// 
                EditorGUILayout.BeginHorizontal("box");
                {
                    GUIStyle guiStyle = new GUIStyle();
                    guiStyle.normal.textColor = select ? Color.green : Color.black;
                    bool value = EditorGUILayout.Toggle(new GUIContent(name), select);
                    if (value && !select)
                    {
                        int id = HsmDataController.Instance.NodeTransitionID(nodeData.id, temp.transitionId);
                        HsmDataController.Instance.CurrentTransitionId = id;
                    }

                    if (GUILayout.Button("复制"))
                    {
                        HsmDataController.copyTransition = temp.Clone();
                    }

                    if (GUILayout.Button("粘贴"))
                    {
                        if (null != HsmDataController.copyTransition)
                        {
                            HsmDataController.copyTransition.transitionId = temp.transitionId;
                            HsmDataController.copyTransition.toStateId = temp.toStateId;
                            temp = HsmDataController.copyTransition.Clone();
                            nodeData.transitionList[i] = temp;
                            HsmDataController.copyTransition = null;
                        }
                    }

                    if (GUILayout.Button("删除"))
                    {
                        NodeTransitionHandle nodeTransitionHandle = new NodeTransitionHandle();
                        nodeTransitionHandle.NodeDelTransition(nodeData.id, temp.toStateId);

                        if (transition.transitionId == temp.transitionId)
                        {
                            HsmDataController.Instance.CurrentTransitionId = -1;
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
                GUI.backgroundColor = Color.white;
            }
        }

        private void DrawTransitionParameter(HsmConfigNode nodeData, HsmConfigTransition transition)
        {
            EditorGUILayout.BeginVertical("box");
            {
                HsmConfigNode toNode = HsmDataController.Instance.GetNode(transition.toStateId);
                string toNodeDescript = (null != toNode) ? toNode.descript : string.Empty;
                string msg = string.Format("当前选择:节点{0} -> 节点{1}", nodeData.id, transition.toStateId);
                EditorGUILayout.LabelField(msg);
            }
            EditorGUILayout.EndVertical();

            int transitionId = transition.transitionId;
            List<NodeParameter> parametersList = transition.parameterList;
            ConditionGroup group = null;
            EditorGUILayout.BeginVertical("box");
            {
                group = HSMTransitionGroup.DrawTransitionGroup(nodeData, transition);
                DrawTransitionAddGroup(nodeData, transitionId);
            }
            EditorGUILayout.EndVertical();

            for (int i = 0; i < parametersList.Count; ++i)
            {
                parametersList[i].index = i;
            }
            DrawParameter(nodeData, parametersList, group);

            GUILayout.Space(10);
            EditorGUILayout.BeginVertical("box");
            {
                DrawTransitionAddParameter(nodeData, transitionId);
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawParameter(HsmConfigNode nodeData, List<NodeParameter> parametersList, ConditionGroup group)
        {
            int height = parametersList.Count * 50;
            height = height <= 500 ? height : 500;
            EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
            {
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(height));
                {
                    for (int i = 0; i < parametersList.Count; ++i)
                    {
                        NodeParameter hSMParameter = parametersList[i];
                        DrawParameter(nodeData, hSMParameter, group);
                    }
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawParameter(HsmConfigNode nodeData, NodeParameter hSMParameter, ConditionGroup group)
        {
            Color color = Color.white;
            if (null != group)
            {
                string name = group.parameterList.Find(a => (a.CompareTo(hSMParameter.parameterName) == 0));
                if (!string.IsNullOrEmpty(name))
                {
                    color = colorArr[0];
                }
            }

            GUI.backgroundColor = color;
            EditorGUILayout.BeginVertical("box");
            {
                string parameterName = hSMParameter.parameterName;
                hsmDrawNodeParameter.SetDelCallBack(DelParameter);
                hsmDrawNodeParameter.Draw(hSMParameter);
                if (parameterName.CompareTo(hSMParameter.parameterName) != 0)
                {
                    ChangeParameter(hSMParameter.parameterName);
                }
            }
            EditorGUILayout.EndVertical();
            GUI.backgroundColor = Color.white;
        }

        private void DelParameter(NodeParameter hSMParameter)
        {
            NodeTransitionParaemeterHandle transitionParaemeterHandle = new NodeTransitionParaemeterHandle();
            transitionParaemeterHandle.TransitionDelParameter(_nodeData.id, _transition.transitionId, hSMParameter);
        }

        private void ChangeParameter(string parameterName)
        {
            NodeTransitionParaemeterHandle nodeTransitionParaemeterHandle = new NodeTransitionParaemeterHandle();
            nodeTransitionParaemeterHandle.TransitionChangeParameter(_nodeData.id, _transition.transitionId, parameterName);
        }

        private void DrawTransitionAddParameter(HsmConfigNode nodeValue, int transitionId)
        {
            GUI.enabled = (transitionId >= 0);
            if (GUILayout.Button("添加条件"))
            {
                NodeTransitionParaemeterHandle transitionParaemeterHandle = new NodeTransitionParaemeterHandle();
                transitionParaemeterHandle.TransitionAddParemeter(nodeValue.id, transitionId);
            }
            GUI.enabled = true;
        }

        private void DrawTransitionAddGroup(HsmConfigNode nodeValue, int transitionId)
        {
            if (transitionId < 0)
            {
                return;
            }

            if (GUILayout.Button("添加组"))
            {
                NodeTransitionHandle nodeTransitionHandle = new NodeTransitionHandle();
                nodeTransitionHandle.NodeTransitionAddGroup(nodeValue.id, transitionId);
            }
        }
    }
}