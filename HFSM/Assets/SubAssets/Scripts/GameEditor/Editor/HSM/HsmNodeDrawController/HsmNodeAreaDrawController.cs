using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    // 节点编辑窗口区域
    public class HsmNodeAreaDrawController
    {
        public HsmNodeAreaDrawModel _hsmNodeAreaDrawModel = null;
        private HsmNodeAreaDrawView _hsmNodeAreaDrawView = null;

        public HsmNodeAreaDrawController()
        {
            Init();
        }

        private void Init()
        {
            _hsmNodeAreaDrawModel = new HsmNodeAreaDrawModel();
            _hsmNodeAreaDrawView = new HsmNodeAreaDrawView();
            _hsmNodeAreaDrawView.Init(_hsmNodeAreaDrawModel);
        }

        public void OnDestroy() {    }

        public void OnGUI(Rect windowRect)
        {
            HsmConfigNode currentNode = _hsmNodeAreaDrawModel.GetCurrentSelectNode();
            List<HsmConfigNode> nodeList = new List<HsmConfigNode>();
            if (HsmDataController.Instance.CurrentOpenSubMachineId >= 0)
            {
                nodeList = HsmDataController.Instance.GetNodeChild(HsmDataController.Instance.CurrentOpenSubMachineId);
            }
            else
            {
                nodeList = _hsmNodeAreaDrawModel.GetBaseNode();
            }

            _hsmNodeAreaDrawView.Draw(windowRect, currentNode, nodeList);
        }
    }



}
