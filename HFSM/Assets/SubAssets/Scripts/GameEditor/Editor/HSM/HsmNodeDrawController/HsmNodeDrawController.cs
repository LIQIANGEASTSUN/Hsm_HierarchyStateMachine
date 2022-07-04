using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using GraphicTree;

namespace HSMTree
{
    public class HsmNodeDrawController
    {
        public HsmNodeDrawModel _HSMDrawModel = null;
        private HsmNodeDrawView _HSMDrawView = null;

        public HsmNodeDrawController()
        {
            Init();
        }

        private void Init()
        {
            _HSMDrawModel = new HsmNodeDrawModel();
            _HSMDrawView = new HsmNodeDrawView();
            _HSMDrawView.Init(this, _HSMDrawModel);
        }

        public void OnDestroy() {    }

        public void OnGUI(Rect windowRect)
        {
            HsmConfigNode currentNode = _HSMDrawModel.GetCurrentSelectNode();
            List<HsmConfigNode> nodeList = new List<HsmConfigNode>();
            if (HsmDataController.Instance.CurrentOpenSubMachineId >= 0)
            {
                nodeList = HsmDataController.Instance.GetNodeChild(HsmDataController.Instance.CurrentOpenSubMachineId);
            }
            else
            {
                nodeList = _HSMDrawModel.GetBaseNode();
            }

            _HSMDrawView.Draw(windowRect, currentNode, nodeList);
        }
    }



}
