using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace HSMTree
{
    public class ParameterTool
    {

        public static void AddParameter(List<NodeParameter> parameterList, NodeParameter parameter)
        {
            if (string.IsNullOrEmpty(parameter.parameterName))
            {
                string meg = string.Format("条件参数不能为空", parameter.parameterName);
                HSMNodeWindow.window.ShowNotification(meg);
                return;
            }

            bool enableAdd = true;
            for (int i = 0; i < parameterList.Count; ++i)
            {
                NodeParameter tempParameter = parameterList[i];
                if (tempParameter.parameterName.CompareTo(parameter.parameterName) == 0)
                {
                    enableAdd = false;
                    string meg = string.Format("条件参数:{0} 已存在", parameter.parameterName);
                    HSMNodeWindow.window.ShowNotification(meg);
                    break;
                }
            }

            if (enableAdd)
            {
                NodeParameter newParameter = parameter.Clone();
                parameterList.Add(newParameter);
            }

            for (int i = 0; i < parameterList.Count; ++i)
            {
                parameterList[i].index = i;
            }
        }

        public static void DelParameter(List<NodeParameter> parameterList, NodeParameter parameter)
        {
            for (int i = 0; i < parameterList.Count; ++i)
            {
                NodeParameter tempParameter = parameterList[i];
                if (tempParameter.parameterName.CompareTo(parameter.parameterName) == 0)
                {
                    parameterList.RemoveAt(i);
                    break;
                }
            }

            for (int i = 0; i < parameterList.Count; ++i)
            {
                parameterList[i].index = i;
            }
        }
    }
}