using System.Collections;
using System.Collections.Generic;
using LitJson;
using GraphicTree;

namespace HSMTree
{
    public class HSMReadWrite
    {
        #region HSMTreeData
        public bool WriteJson(HsmConfigTreeData data, string filePath)
        {
            string content = LitJson.JsonMapper.ToJson(data);
            bool value = FileReadWrite.Write(filePath, content);

            if (value)
            {
                //Debug.Log("Write Sucess:" + filePath);
            }
            else
            {
                //Debug.LogError("Write Fail:" + filePath);
            }

            return value;
        }

        public HsmConfigTreeData ReadJson(string filePath)
        {
            //Debug.Log("Read:" + filePath);
            HsmConfigTreeData HSMData = new HsmConfigTreeData();

            string content = FileReadWrite.Read(filePath);
            if (string.IsNullOrEmpty(content))
            {
                return HSMData;
            }

            JsonData jsonData = JsonMapper.ToObject(content);

            if (((IDictionary)jsonData).Contains("fileName"))
            {
                HSMData.fileName = jsonData["fileName"].ToString();
            }
            HSMData.defaultStateId = int.Parse(jsonData["defaultStateId"].ToString());

            if (((IDictionary)jsonData).Contains("nodeList"))
            {
                JsonData nodeList = jsonData["nodeList"];
                List<HsmConfigNode> dataList = GetNodeList(nodeList);
                HSMData.nodeList.AddRange(dataList);
            }

            if (((IDictionary)jsonData).Contains("parameterList"))
            {
                JsonData parameterList = jsonData["parameterList"];
                List<NodeParameter> dataList = GetParameterList(parameterList);
                HSMData.parameterList.AddRange(dataList);
            }

            HSMData.descript = jsonData["descript"].ToString();

            return HSMData;
        }

        private List<HsmConfigNode> GetNodeList(JsonData data)
        {
            List<HsmConfigNode> nodeList = new List<HsmConfigNode>();

            foreach (JsonData item in data)
            {
                HsmConfigNode nodeValue = new HsmConfigNode();
                nodeValue.id = int.Parse(item["id"].ToString());
                nodeValue.nodeType = int.Parse(item["nodeType"].ToString());
                nodeValue.nodeName = item["nodeName"].ToString();
                nodeValue.identification = item["identification"].ToString();
                nodeValue.descript = item["descript"].ToString();

                if (((IDictionary)item).Contains("parameterList"))
                {
                    JsonData parameterList = item["parameterList"];
                    List<NodeParameter> dataList = GetParameterList(parameterList);
                    nodeValue.parameterList.AddRange(dataList);
                }

                if (((IDictionary)item).Contains("transitionList"))
                {
                    JsonData transitionList = item["transitionList"];
                    List<HsmConfigTransition> dataList = GetTransitionList(transitionList);
                    nodeValue.transitionList.AddRange(dataList);
                }

                if (((IDictionary)item).Contains("position"))
                {
                    JsonData position = item["position"];
                    nodeValue.position = GetPosition(position);
                }

                if (((IDictionary)item).Contains("childIdList"))
                {
                    JsonData childIdList = item["childIdList"];
                    nodeValue.childIdList.AddRange(GetIntList(childIdList));
                }

                if (((IDictionary)item).Contains("parentId"))
                {
                    nodeValue.parentId = int.Parse(item["parentId"].ToString());
                }

                nodeList.Add(nodeValue);
            }

            return nodeList;
        }

        private List<HsmConfigTransition> GetTransitionList(JsonData jsonData)
        {
            List<HsmConfigTransition> transitionList = new List<HsmConfigTransition>();
            foreach (JsonData item in jsonData)
            {
                HsmConfigTransition transition = new HsmConfigTransition();
                transition.transitionId = int.Parse(item["transitionId"].ToString());
                transition.toStateId = int.Parse(item["toStateId"].ToString());
                List<NodeParameter> dataList = GetParameterList(item["parameterList"]);
                transition.parameterList.AddRange(dataList);

                List<ConditionGroup> groupList = GetTransitionGroup(item["groupList"]);
                transition.groupList.AddRange(groupList);

                transitionList.Add(transition);
            }

            return transitionList;
        }

        private List<ConditionGroup> GetTransitionGroup(JsonData jsonData)
        {
            List<ConditionGroup> groupList = new List<ConditionGroup>();
            foreach (JsonData item in jsonData)
            {
                ConditionGroup group = new ConditionGroup();
                group.index = int.Parse(item["index"].ToString());

                List<string> parameterList = GetStringList(item["parameterList"]);
                group.parameterList.AddRange(parameterList);

                groupList.Add(group);
            }

            return groupList;
        }

        private List<int> GetIntList(JsonData jsonData)
        {
            List<int> valueList = new List<int>();

            for (int i = 0; i < jsonData.Count; ++i)
            {
                int value = int.Parse(jsonData[i].ToString());
                valueList.Add(value);
            }

            return valueList;
        }

        private List<string> GetStringList(JsonData jsonData)
        {
            List<string> stringList = new List<string>();

            for (int i = 0; i < jsonData.Count; ++i)
            {
                string str = jsonData[i].ToString();
                stringList.Add(str);
            }

            return stringList;
        }

        private RectT GetPosition(JsonData data)
        {
            RectT position = new RectT();

            position.x = int.Parse(data["x"].ToString());
            position.y = int.Parse(data["y"].ToString());
            position.width = int.Parse(data["width"].ToString());
            position.height = int.Parse(data["height"].ToString());

            return position;
        }

        private List<NodeParameter> GetParameterList(JsonData data)
        {
            List<NodeParameter> dataList = new List<NodeParameter>();
            foreach (JsonData item in data)
            {
                NodeParameter parameter = new NodeParameter();
                parameter.parameterType = int.Parse(item["parameterType"].ToString());
                parameter.parameterName = item["parameterName"].ToString();
                parameter.CNName = item["CNName"].ToString();
                parameter.intValue = int.Parse(item["intValue"].ToString());
                parameter.floatValue = float.Parse(item["floatValue"].ToString());
                parameter.longValue = long.Parse(item["longValue"].ToString());
                parameter.boolValue = bool.Parse(item["boolValue"].ToString());
                parameter.stringValue = item["stringValue"].ToString();
                parameter.compare = int.Parse(item["compare"].ToString());
                parameter.index = int.Parse(item["index"].ToString());


                dataList.Add(parameter);
            }

            return dataList;
        }
        #endregion
    }

}
