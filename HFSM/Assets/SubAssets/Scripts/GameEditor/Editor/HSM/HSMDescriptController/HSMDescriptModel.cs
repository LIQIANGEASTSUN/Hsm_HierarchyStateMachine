
namespace HSMTree
{
    public class HSMDescriptModel
    {
        public HsmConfigTreeData GetData()
        {
            return HsmDataController.Instance.HSMTreeData;
        }
    }
}