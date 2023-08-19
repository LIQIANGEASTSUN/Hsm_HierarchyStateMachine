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
            if (GUILayout.Button("添加参数"))
            {
                NodeParameterHandle nodeParameterHandle = new NodeParameterHandle();
                nodeParameterHandle.NodeAddParameter(nodeValue.id);
            }
        }
    }
}