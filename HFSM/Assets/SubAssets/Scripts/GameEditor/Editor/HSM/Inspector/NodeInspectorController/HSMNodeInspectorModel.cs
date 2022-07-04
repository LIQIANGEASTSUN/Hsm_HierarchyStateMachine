
namespace HSMTree
{
    public class HSMNodeInspectorModel
    {
        public HsmConfigNode GetCurrentSelectNode()
        {
            return HsmDataController.Instance.CurrentNode;
        }
    }
}
