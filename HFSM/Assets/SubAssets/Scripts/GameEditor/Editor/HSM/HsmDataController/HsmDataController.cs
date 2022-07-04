using System.Collections.Generic;
using GraphicTree;
using UnityEngine;

namespace HSMTree
{
    public delegate void HSMRuntimePlay(HSMPlayType state);

    public class HsmDataController
    {
        public static HsmDataController Instance;
        private string _fileName = string.Empty;

        private HsmConfigTreeData _HSMTreeData;
        // 当前选择的节点
        private int _currentSelectId = -1;
        private int _currentTransitionId = -1;
        private int _currentOpenSubMachineId = -1;
        private HSMPlayType _playState = HSMPlayType.STOP;

        public static HsmConfigTransition copyTransition;
        public static HSMRuntimePlay hSMRuntimePlay;

        public HsmDataController()
        {
        }

        public void Init()
        {
            hSMRuntimePlay += RuntimePlay;
            CreateData();
        }

        private void CreateData()
        {
            HSMTreeData = new HsmConfigTreeData();
            DataAddNodeHandle dataAddNodeHandle = new DataAddNodeHandle();
            dataAddNodeHandle.AddNode("Entry", typeof(HSMStateEntry).Name, HSM_NODE_TYPE.ENTRY, new Vector3(500, 150, 0), -1);
            dataAddNodeHandle.AddNode("Exit", typeof(HSMStateExit).Name, HSM_NODE_TYPE.EXIT, new Vector3(500, 550, 0), -1);
        }

        public void Destroy()
        {
            _fileName = string.Empty;
            _playState = HSMPlayType.STOP;
            hSMRuntimePlay -= RuntimePlay;
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public string FilePath
        {
            get { return CommonUtils.FileUtils.CombinePath("Assets", "SubAssets", "GameData", "HSM"); }// "Assets/SubAssets/GameData/HSM/";
        }

        public string GetFilePath(string fileName)
        {
            string path = CommonUtils.FileUtils.CombinePath(FilePath, string.Format("{0}.txt", fileName)); //string.Format("{0}{1}.txt", FilePath, fileName);
            return path;
        }

        public int CurrentSelectId
        {
            get { return _currentSelectId; }
            set { _currentSelectId = value;
                int nodeId = NodeTransitionIDToNode(CurrentTransitionId);
                if (nodeId != value)
                {
                    CurrentTransitionId = -1;
                }
            }
        }

        public int CurrentTransitionId
        {
            get { return _currentTransitionId; }
            set { _currentTransitionId = value; }
        }

        public int CurrentOpenSubMachineId
        {
            get { return _currentOpenSubMachineId; }
            set { _currentOpenSubMachineId = value; }
        }

        public HsmConfigTreeData HSMTreeData
        {
            get { return _HSMTreeData; }
            set { _HSMTreeData = value; }
        }

        public HSMPlayType PlayType
        {
            get { return _playState; }
            set { _playState = value; }
        }

        private void RuntimePlay(HSMPlayType state)
        {
            NodeNotify.SetPlayState((int)state);
            PlayType = state;
        }

        public HsmConfigNode CurrentNode
        {
            get
            {
                return GetNode(_currentSelectId);
            }
        }

        public int DefaultStateId
        {
            get { return HSMTreeData.defaultStateId; }
        }

        public void SetDefaultState(HsmConfigNode nodeValue)
        {
            if (nodeValue.nodeType != (int)HSM_NODE_TYPE.ENTRY && nodeValue.nodeType != (int)HSM_NODE_TYPE.EXIT)
            {
                SetDefaultState(nodeValue.id);
            }
        }

        // 设置默认状态
        public void SetDefaultState(int stateId)
        {
            HSMTreeData.defaultStateId = stateId;
        }

        public List<HsmConfigNode> NodeList
        {
            get
            {
                return HSMTreeData.nodeList;
            }
        }

        public HsmConfigNode GetNode(int stateId)
        {
            for (int i = 0; i < NodeList.Count; ++i)
            {
                HsmConfigNode nodeValue = NodeList[i];
                if (nodeValue.id == stateId)
                {
                    return nodeValue;
                }
            }

            return null;
        }

        public void GetNodeAllChild(int nodeId, List<HsmConfigNode> dataList)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(nodeId);
            while (queue.Count > 0)
            {
                nodeId = queue.Dequeue();
                List<HsmConfigNode> childList = GetNodeChild(nodeId);
                if (childList.Count > 0)
                {
                    dataList.AddRange(childList);
                }
                foreach(var node in childList)
                {
                    queue.Enqueue(node.id);
                }
            }
        }

        public List<HsmConfigNode> GetNodeChild(int nodeId)
        {
            List<HsmConfigNode> dataList = new List<HsmConfigNode>();
            HsmConfigNode nodeValue = GetNode(nodeId);
            if (null == nodeValue)
            {
                return dataList;
            }

            for (int i = 0; i < nodeValue.childIdList.Count; ++i)
            {
                int childId = nodeValue.childIdList[i];
                HsmConfigNode childNode = GetNode(childId);
                if (null != childNode)
                {
                    dataList.Add(childNode);
                }
            }

            return dataList;
        }

        public NodeParameter GetDataTreeParameter(string parameterName)
        {
            NodeParameter parameter = null;
            for (int i = 0; i < HSMTreeData.parameterList.Count; ++i)
            {
                NodeParameter temp = HSMTreeData.parameterList[i];
                if (temp.parameterName.CompareTo(parameterName) == 0)
                {
                    parameter = temp;
                    break;
                }
            }
            return parameter;
        }

        private const int _coefficient = 1000;
        public int NodeTransitionID(int nodeId, int transitionId)
        {
            int id = nodeId * _coefficient + transitionId;
            return id;
        }

        public int NodeTransitionIDToNode(int nodeTransitionId)
        {
            if (nodeTransitionId < 0)
            {
                return -1;
            }
            int nodeId = nodeTransitionId / _coefficient;
            return nodeId;
        }

        public int NodeTransitionIDToTransitionId(int nodeTransitionId)
        {
            if (nodeTransitionId < 0)
            {
                return -1;
            }
            int transitionId = nodeTransitionId % _coefficient;
            return transitionId;
        }

    }
}
