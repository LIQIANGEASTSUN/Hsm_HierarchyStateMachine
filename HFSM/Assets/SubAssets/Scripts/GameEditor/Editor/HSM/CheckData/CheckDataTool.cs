using GraphicTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    public class CheckDataTool
    {
        public static void CheckData(HsmConfigTreeData data)
        {
            for (int i = 0; i < data.nodeList.Count; ++i)
            {
                HsmConfigNode nodeData = data.nodeList[i];
                for (int j = 0; j < nodeData.transitionList.Count; ++j)
                {
                    HsmConfigTransition transition = nodeData.transitionList[j];
                    CheckTransition(transition);
                }
            }
        }

        private static void CheckTransition(HsmConfigTransition transition)
        {
            for (int i = transition.groupList.Count - 1; i >= 0; --i)
            {
                ConditionGroup group = transition.groupList[i];
                bool validGroup = false;
                for (int j = group.parameterList.Count - 1; j >= 0; --j)
                {
                    string parameter = group.parameterList[j];
                    NodeParameter hSMParameter = transition.parameterList.Find(a => (a.parameterName.CompareTo(parameter) == 0));
                    if (null == hSMParameter)
                    {
                        group.parameterList.RemoveAt(j);
                        Debug.LogError("group ParameterList remove :" + parameter);
                    }
                    else
                    {
                        validGroup = true;
                    }
                }

                if (!validGroup)
                {
                    transition.groupList.RemoveAt(i);
                    Debug.LogError("RemoveGroup :" + group.index);
                }
            }
        }
    }
}
