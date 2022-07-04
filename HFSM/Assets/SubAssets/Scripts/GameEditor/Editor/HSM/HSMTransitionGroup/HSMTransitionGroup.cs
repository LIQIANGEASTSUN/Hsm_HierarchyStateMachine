using UnityEngine;
using UnityEditor;
using GraphicTree;

namespace HSMTree
{

    public static class HSMTransitionGroup
    {
        private static int nodeId = -1;
        private static int selectIndex = -1;
        public static ConditionGroup DrawTransitionGroup(HsmConfigNode nodeData, HsmConfigTransition transition)
        {
            if (null == transition)
            {
                return null;
            }

            ConditionGroup group = null;
            for (int i = 0; i < transition.groupList.Count; ++i)
            {
                ConditionGroup transitionGroup = transition.groupList[i];
                ConditionGroup temp = DrawGroup(nodeData, transition, transitionGroup);
                if (null != temp)
                {
                    group = temp;
                }
            }

            return group;
        }

        private static ConditionGroup DrawGroup(HsmConfigNode nodeData, HsmConfigTransition transition, ConditionGroup group)
        {
            Rect area = GUILayoutUtility.GetRect(0f, 1, GUILayout.ExpandWidth(true));
            bool select = (selectIndex == group.index);

            EditorGUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true));
            {
                if (selectIndex < 0 || nodeId < 0 || nodeId != nodeData.id)
                {
                    nodeId = nodeData.id;
                    selectIndex = group.index;
                }

                Rect rect = new Rect(area.x, area.y, area.width, 30);
                GUI.backgroundColor = select ? new Color(0, 1, 0, 1) : Color.white;// 
                GUI.Box(rect, string.Empty);
                GUI.backgroundColor = Color.white;

                if (GUILayout.Button("选择", GUILayout.Width(50)))
                {
                    selectIndex = group.index;
                }

                for (int i = group.parameterList.Count - 1; i >= 0; --i)
                {
                    string parameter = group.parameterList[i];
                    NodeParameter hSMParameter = transition.parameterList.Find(a => (a.parameterName.CompareTo(parameter) == 0));
                    if (null == hSMParameter)
                    {
                        group.parameterList.RemoveAt(i);
                    }
                }

                GUI.enabled = select;
                for (int i = 0; i < transition.parameterList.Count; ++i)
                {
                    NodeParameter parameter = transition.parameterList[i];
                    string name = group.parameterList.Find(a => (a.CompareTo(parameter.parameterName) == 0));

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(10));
                        bool value = !string.IsNullOrEmpty(name);
                        bool oldValue = value;
                        value = EditorGUILayout.Toggle(value, GUILayout.Width(10));
                        if (value)
                        {
                            if (!oldValue)
                            {
                                group.parameterList.Add(parameter.parameterName);
                                break;
                            }
                        }
                        else
                        {
                            if (oldValue)
                            {
                                group.parameterList.Remove(parameter.parameterName);
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(10);
                }
                GUI.enabled = true;

                if (GUILayout.Button("删除"))
                {
                    NodeTransitionHandle nodeTransitionHandle = new NodeTransitionHandle();
                    nodeTransitionHandle.NodeTransitionDelGroup(nodeData.id, transition.transitionId, group.index);
                }
            }
            EditorGUILayout.EndHorizontal();

            if (select)
            {
                return group;
            }
            return null;
        }

    }
}
