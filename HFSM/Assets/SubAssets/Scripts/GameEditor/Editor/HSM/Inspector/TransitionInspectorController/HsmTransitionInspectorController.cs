using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HSMTree
{
    public class HsmTransitionInspectorController
    {
        private HsmTransitionInspectorModel _transitionInspectorModel;
        private HsmTransitionInspectorView _transitionInspectorView;

        public HsmTransitionInspectorController()
        {
            Init();
        }

        public void Init()
        {
            _transitionInspectorModel = new HsmTransitionInspectorModel();
            _transitionInspectorView = new HsmTransitionInspectorView();
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            HsmConfigNode nodeValue = null;
            HsmConfigTransition transition = _transitionInspectorModel.GetCurrentSelectTransition(ref nodeValue);
            _transitionInspectorView.Draw(nodeValue, transition);
        }

    }
}

