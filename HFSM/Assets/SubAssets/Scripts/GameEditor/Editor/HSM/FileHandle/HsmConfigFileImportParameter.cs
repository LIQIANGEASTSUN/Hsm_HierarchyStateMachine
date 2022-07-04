using System.Collections.Generic;
using UnityEngine;
using GraphicTree;

namespace HSMTree
{
    public class HsmConfigFileImportParameter
    {

        public static void ImportParameter()
        {
            string fileName = "Hsm";
            HsmConfigTreeData hsmData = HsmDataController.Instance.HSMTreeData;
            hsmData = ImportParameter(hsmData, fileName);
        }

        private static HsmConfigTreeData ImportParameter(HsmConfigTreeData hsmData, string fileName)
        {
            TableRead.Instance.Init();
            string csvPath = CommonUtils.FileUtils.CombinePath(Application.dataPath, "StreamingAssets", "CSV"); //string.Format("{0}/StreamingAssets/CSV/", Application.dataPath);
            TableRead.Instance.ReadCustomPath(csvPath);

            List<int> keyList = TableRead.Instance.GetKeyList(fileName);

            Dictionary<string, NodeParameter> parameterDic = new Dictionary<string, NodeParameter>();
            for (int i = 0; i < hsmData.parameterList.Count; ++i)
            {
                NodeParameter parameter = hsmData.parameterList[i];
                parameterDic[parameter.parameterName] = parameter;
            }

            for (int i = 0; i < keyList.Count; ++i)
            {
                int key = keyList[i];
                string EnName = TableRead.Instance.GetData(fileName, key, "EnName");
                string cnName = TableRead.Instance.GetData(fileName, key, "CnName");
                string typeName = TableRead.Instance.GetData(fileName, key, "Type");
                int type = int.Parse(typeName);

                string floatContent = TableRead.Instance.GetData(fileName, key, "FloatValue");
                float floatValue = float.Parse(floatContent);

                string intContent = TableRead.Instance.GetData(fileName, key, "IntValue");
                int intValue = int.Parse(intContent);

                string boolContent = TableRead.Instance.GetData(fileName, key, "BoolValue");
                bool boolValue = (int.Parse(boolContent) == 1);

                if (parameterDic.ContainsKey(EnName))
                {
                    if (parameterDic[EnName].parameterType != type)
                    {
                        Debug.LogError("已经存在参数:" + EnName + "   type:" + (ParameterType)parameterDic[EnName].parameterType + "   newType:" + (ParameterType)type);
                    }
                    else
                    {
                        Debug.LogError("已经存在参数:" + EnName);
                    }
                    parameterDic.Remove(EnName);

                    for (int j = 0; j < hsmData.parameterList.Count; ++j)
                    {
                        NodeParameter cacheParameter = hsmData.parameterList[j];
                        if (cacheParameter.parameterName == EnName)
                        {
                            hsmData.parameterList.RemoveAt(j);
                            break;
                        }
                    }

                    //continue;
                }

                //Debug.LogError(EnName + "    " +cnName + "    " + typeName);

                NodeParameter parameter = new NodeParameter();
                parameter.parameterName = EnName;
                parameter.CNName = cnName;
                parameter.compare = (int)ParameterCompare.EQUALS;
                parameter.parameterType = type;
                parameter.boolValue = false;

                if (type == (int)ParameterType.Float)
                {
                    parameter.floatValue = floatValue;
                }

                if (type == (int)ParameterType.Int)
                {
                    parameter.intValue = intValue;
                }

                if (type == (int)ParameterType.Float)
                {
                    parameter.boolValue = boolValue;
                }

                hsmData.parameterList.Add(parameter);
            }

            foreach (var kv in parameterDic)
            {
                Debug.LogError("==========缺失的参数:" + kv.Key);
            }

            return hsmData;
        }

    }

}