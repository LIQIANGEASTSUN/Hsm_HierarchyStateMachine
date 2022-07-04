using UnityEngine;

namespace HSMTree
{
    public class HSMDrawProperty
    {
        private HSMFileHandleController _fileHandleController;
        private HSMPlayController _playController;
        private HSMPropertyOption _propertyOption;
        private HSMDescriptController _descriptController;
        private HSMNodeInspectorController _nodeInspectorController;
        private HsmTransitionInspectorController _transitionInspectorController;
        private HSMParameterController _parameterController;
        private HSMRuntimeParameterController _runtimeParameterController;

        public HSMDrawProperty()
        {
            Init();
        }

        private void Init()
        {
            _fileHandleController = new HSMFileHandleController();
            _playController = new HSMPlayController();
            _propertyOption = new HSMPropertyOption();
            _descriptController = new HSMDescriptController();
            _nodeInspectorController = new HSMNodeInspectorController();
            _transitionInspectorController = new HsmTransitionInspectorController();

            _parameterController = new HSMParameterController();
            _runtimeParameterController = new HSMRuntimeParameterController();
        }

        public void OnDestroy()
        {
            _fileHandleController.OnDestroy();
            _playController.OnDestroy();
            _propertyOption.OnDestroy();
            _descriptController.OnDestroy();
            _nodeInspectorController.OnDestroy();
            _transitionInspectorController.OnDestroy();
            _parameterController.OnDestroy();
            _runtimeParameterController.OnDestroy();
        }

        public void OnGUI()
        {
            _fileHandleController.OnGUI();
            GUILayout.Space(3);

            _playController.OnGUI();
            GUILayout.Space(3);

            OptionEnum option = _propertyOption.OnGUI();
            if (option == OptionEnum.Descript)
            {
                _descriptController.OnGUI();
            }
            else if (option == OptionEnum.NodeInspector)
            {
                _nodeInspectorController.OnGUI();
            }
            else if (option == OptionEnum.TransitionInspector)
            {
                _transitionInspectorController.OnGUI();
            }
            else if (option == OptionEnum.Parameter)
            {
                if (HsmDataController.Instance.PlayType == HSMPlayType.PLAY
                    || HsmDataController.Instance.PlayType == HSMPlayType.PAUSE)
                {
                    _runtimeParameterController.OnGUI();
                }
                else
                {
                    _parameterController.OnGUI();
                }
            }
        }
    }

}