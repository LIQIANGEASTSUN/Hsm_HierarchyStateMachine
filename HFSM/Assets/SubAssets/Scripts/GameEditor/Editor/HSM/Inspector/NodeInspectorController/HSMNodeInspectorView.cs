using System.Collections.Generic;
using UnityEngine;
using GraphicTree;
using UnityEditor;
using System;

namespace HSMTree
{
    public class HSMNodeInspectorView
    {
        private HsmDrawNodeParameter hsmDrawNodeParameter;
        private HsmConfigNode _nodeValue;
        public HSMNodeInspectorView()
        {
            hsmDrawNodeParameter = new HsmDrawNodeParameter();
            hsmDrawNodeParameter.SetDelCallBack(DelParameter);
        }

        public void Draw(HsmConfigNode nodeValue)
        {
            if (null == nodeValue)
            {
                EditorGUILayout.LabelField("未选择节点");
                return;
            }
            _nodeValue = nodeValue;

            DrawProperty(nodeValue);
            DrawNodeParameter(nodeValue);
        }

        private void DrawProperty(HsmConfigNode nodeValue)
        {
            EditorGUILayout.BeginVertical("box");
            {
                int index = EnumNames.GetEnumIndex<HSM_NODE_TYPE>((HSM_NODE_TYPE)nodeValue.nodeType);
                string name = EnumNames.GetEnumName<HSM_NODE_TYPE>(index);
                name = string.Format("节点id:{0}  类型:{1}", nodeValue.id, name);
                EditorGUILayout.LabelField(name);

                string identification = string.Format("Identification:{0}", nodeValue.identification);
                EditorGUILayout.LabelField(identification);

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("节点名:", GUILayout.Width(50));
                    nodeValue.nodeName = EditorGUILayout.TextField(nodeValue.nodeName, GUILayout.ExpandWidth(true));
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField("节点描述:");
                nodeValue.descript = EditorGUILayout.TextArea(nodeValue.descript, GUILayout.Height(30));
                GUILayout.Space(5);
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawNodeParameter(HsmConfigNode nodeData)
        {
            EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
            {
                DrawParameter(nodeData, nodeData.parameterList);
                GUILayout.Space(10);
                DrawNodeAddParameter(nodeData);
            }
            EditorGUILayout.EndVertical();
        }

        private Vector2 scrollPos = Vector2.zero;
        private void DrawParameter(HsmConfigNode nodeData, List<NodeParameter> parametersList)
        {
            int height = parametersList.Count * 50;
            height = height <= 500 ? height : 500;
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(height));
            {
                for (int i = 0; i < parametersList.Count; ++i)
                {
                    NodeParameter hSMParameter = parametersList[i];
                    DrawParameter(nodeData, hSMParameter);
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private void DrawParameter(HsmConfigNode nodeData, NodeParameter hSMParameter)
        {
            string parameterName = hSMParameter.parameterName;
            hsmDrawNodeParameter.Draw(hSMParameter);
            GUILayout.Space(5);
            if (parameterName.CompareTo(hSMParameter.parameterName) != 0)
            {
                ChangeParameter(nodeData, hSMParameter.parameterName);
            }
        }

        private void DelParameter(NodeParameter parameter)
        {
            NodeParameterHandle nodeParameterHandle = new NodeParameterHandle();
            nodeParameterHandle.NodeDelParameter(_nodeValue.id, parameter);
        }

        private void ChangeParameter(HsmConfigNode nodeData, string parameterName)
        {
            NodeParameterHandle nodeParameterHandle = new NodeParameterHandle();
            nodeParameterHandle.NodeChangeParameter(nodeData.id, parameterName);
        }

        private void DrawNodeAddParameter(HsmConfigNode nodeValue)
        {
            if (GUILayout.Button("添加条件"))
            {
                NodeParameterHandle nodeParameterHandle = new NodeParameterHandle();
                nodeParameterHandle.NodeAddParameter(nodeValue.id);
            }
        }
    }

}




/*
 
    using System.Collections.Generic;
using UnityEngine;
using GraphicTree;
using UnityEditor;
using System;

namespace HSMTree
{
    public class HSMNodeInspectorView
    {
        private HsmDrawNodeParameter hsmDrawNodeParameter;
        public HSMNodeInspectorView()
        {
            hsmDrawNodeParameter = new HsmDrawNodeParameter();
        }

        public void Draw(HsmConfigNode nodeValue)
        {
            if (null == nodeValue)
            {
                EditorGUILayout.LabelField("未选择节点");
                return;
            }

            EditorGUILayout.BeginVertical("box");
            {
                int index = EnumNames.GetEnumIndex<HSM_NODE_TYPE>((HSM_NODE_TYPE)nodeValue.nodeType);
                string name = EnumNames.GetEnumName<HSM_NODE_TYPE>(index);
                name = string.Format("节点id:{0}  类型:{1}", nodeValue.id, name);
                EditorGUILayout.LabelField(name);

                string identification = string.Format("Identification:{0}", nodeValue.identification);
                EditorGUILayout.LabelField(identification);
                GUILayout.Space(5);

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("节点名:", GUILayout.Width(50));
                    nodeValue.nodeName = EditorGUILayout.TextField(nodeValue.nodeName, GUILayout.ExpandWidth(true));
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField("节点描述:");
                nodeValue.descript = EditorGUILayout.TextArea(nodeValue.descript, GUILayout.Height(30));
                GUILayout.Space(5);
            }
            EditorGUILayout.EndVertical();

            DrawNode(nodeValue);
        }

        private Dictionary<int, int> _groupColorDic = new Dictionary<int, int>(); // 
        private Color32[] colorArr = new Color32[] { new Color32(178, 226, 221, 255), new Color32(220, 226, 178, 255), new Color32(209, 178, 226, 255), new Color32(178, 185, 226, 255) };
        private bool selectNodeParameter = false;
        private Vector2 scrollPos = Vector2.zero;
        private void DrawNode(HsmConfigNode nodeData)
        {
            string selectTitle = string.Empty;

            int selectTransitionId = -1;
            bool selectTransition = SelectTransition(nodeData, ref selectTransitionId);

            selectNodeParameter = !selectTransition;
            GUI.backgroundColor = selectNodeParameter ? new Color(0, 1, 0, 1) : Color.white;// 
            EditorGUILayout.BeginVertical("box");
            {
                bool oldValue = selectNodeParameter;
                selectNodeParameter = EditorGUILayout.Toggle(new GUIContent("节点参数"), selectNodeParameter);
                if ((!oldValue && selectNodeParameter) && HsmDataController.Instance.CurrentTransitionId >= 0)
                {
                    HsmDataController.Instance.CurrentTransitionId = -1;
                }
            }
            EditorGUILayout.EndVertical();
            GUI.backgroundColor = Color.white;

            HsmConfigTransition transition = null;
            EditorGUILayout.BeginVertical("box");
            {
                EditorGUILayout.LabelField("选择转换连线查看/添加/删除参数");
                for (int i = 0; i < nodeData.transitionList.Count; ++i)
                {
                    HsmConfigTransition temp = nodeData.transitionList[i];
                    string name = string.Format("转换条件参数:连线ID_{0}", temp.toStateId);
                    bool lastValue = (selectTransitionId == temp.transitionId);
                    if (lastValue)
                    {
                        transition = temp;
                        selectTitle = name;//string.Format("{0} 参数", name);
                    }

                    GUI.backgroundColor = lastValue ? new Color(0, 1, 0, 1) : Color.white;// 
                    EditorGUILayout.BeginHorizontal("box");
                    {
                        GUIStyle guiStyle = new GUIStyle();
                        guiStyle.normal.textColor = lastValue ? Color.green : Color.black;
                        bool value = EditorGUILayout.Toggle(new GUIContent(name), lastValue);
                        if (value && !lastValue)
                        {
                            int id = nodeData.id * 1000 + temp.transitionId;
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
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    GUI.backgroundColor = Color.white;
                }
            }
            EditorGUILayout.EndVertical();

            if (null != transition)
            {
                DrawTransitionParameter(nodeData, transition, selectTitle);
            }
            if (selectNodeParameter)
            {
                DrawNodeParameter(nodeData, selectTitle);
            }
        }

        private bool SelectTransition(HsmConfigNode nodeData, ref int id)
        {
            bool selectTransition = false;
            for (int i = 0; i < nodeData.transitionList.Count; ++i)
            {
                HsmConfigTransition temp = nodeData.transitionList[i];
                selectTransition = (nodeData.id * 1000 + temp.transitionId) == HsmDataController.Instance.CurrentTransitionId;
                if (selectTransition)
                {
                    id = temp.transitionId;
                    break;
                }
            }

            return selectTransition;
        }

        private void DrawTransitionParameter(HsmConfigNode nodeData, HsmConfigTransition transition, string title)
        {
            EditorGUILayout.BeginVertical("box");
            {
                if (null != transition)
                {
                    HsmConfigNode toNode = HsmDataController.Instance.GetNode(transition.toStateId);
                    string toNodeDescript = (null != toNode) ? toNode.descript : string.Empty;
                    string msg = string.Format("节点{0} -> 节点{1}", nodeData.id, transition.toStateId);
                    EditorGUILayout.LabelField(msg);
                }
            }
            EditorGUILayout.EndVertical();

            int transitionId = (null != transition) ? transition.transitionId : -1;
            List<NodeParameter> parametersList = (null != transition) ? transition.parameterList : new List<NodeParameter>();

            Action<NodeParameter> DelCallBack = (hSMParameter) =>
            {
                NodeTransitionParaemeterHandle transitionParaemeterHandle = new NodeTransitionParaemeterHandle();
                transitionParaemeterHandle.TransitionDelParameter(nodeData.id, transitionId, hSMParameter);
            };

            Action<string> ChangeParameter = (parameterName) =>
            {
                NodeTransitionParaemeterHandle nodeTransitionParaemeterHandle = new NodeTransitionParaemeterHandle();
                nodeTransitionParaemeterHandle.TransitionChangeParameter(nodeData.id, transitionId, parameterName);
            };

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
            DrawParameter(nodeData, parametersList, DelCallBack, ChangeParameter, group);

            GUILayout.Space(10);
            EditorGUILayout.BeginVertical("box");
            {
                DrawTransitionAddParameter(nodeData, transitionId);
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawNodeParameter(HsmConfigNode nodeData, string title)
        {
            EditorGUILayout.LabelField(title);
            List<NodeParameter> parametersList = nodeData.parameterList;

            Action<NodeParameter> DelCallBack = (hSMParameter) =>
            {
                NodeParameterHandle nodeParameterHandle = new NodeParameterHandle();
                nodeParameterHandle.NodeDelParameter(nodeData.id, hSMParameter);
            };

            Action<string> ChangeParameter = (parameterName) =>
            {
                NodeParameterHandle nodeParameterHandle = new NodeParameterHandle();
                nodeParameterHandle.NodeChangeParameter(nodeData.id, parameterName);
            };

            DrawParameter(nodeData, parametersList, DelCallBack, ChangeParameter, null);

            GUILayout.Space(10);
            EditorGUILayout.BeginVertical("box");
            {
                DrawNodeAddParameter(nodeData);
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawParameter(HsmConfigNode nodeData, List<NodeParameter> parametersList, Action<NodeParameter> delCallBack, Action<string> ChangeParameter, ConditionGroup group)
        {
            EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
            {
                int height = 0;
                for (int i = 0; i < parametersList.Count; ++i)
                {
                    NodeParameter parameter = parametersList[i];
                    height += 50;
                }

                height = height <= 300 ? height : 300;
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(height));
                {
                    for (int i = 0; i < parametersList.Count; ++i)
                    {
                        NodeParameter hSMParameter = parametersList[i];
                        Color color = Color.white;
                        if (null != group)
                        {
                            string name = group.parameterList.Find(a => (a.CompareTo(hSMParameter.parameterName) == 0));
                            if (!string.IsNullOrEmpty(name))
                            {
                                color = colorArr[0];
                            }
                        }

                        GUI.backgroundColor = color; // new Color(0.85f, 0.85f, 0.85f, 1f);
                        EditorGUILayout.BeginVertical("box");
                        {
                            string parameterName = hSMParameter.parameterName;
                            NodeParameter tempParameter = hSMParameter.Clone();
                            hsmDrawNodeParameter.SetDelCallBack(delCallBack);
                            hsmDrawNodeParameter.Draw(hSMParameter);
                            tempParameter = hSMParameter;
                            if (parameterName.CompareTo(hSMParameter.parameterName) != 0)
                            {
                                ChangeParameter(hSMParameter.parameterName);
                            }
                            else
                            {
                                hSMParameter.CloneFrom(tempParameter);
                            }
                        }
                        EditorGUILayout.EndVertical();
                        GUILayout.Space(5);
                        GUI.backgroundColor = Color.white;
                    }
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
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

        private void DrawNodeAddParameter(HsmConfigNode nodeValue)
        {
            if (GUILayout.Button("添加条件"))
            {
                NodeParameterHandle nodeParameterHandle = new NodeParameterHandle();
                nodeParameterHandle.NodeAddParameter(nodeValue.id);
            }
        }

    }

}


*/
