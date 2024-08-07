using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Clusters.ClusterItem
{
    public class ClusterItemCompositeRoot : MonoBehaviour
    {
        [SerializeField] private ClusterItemView _clusterItemView;
        
        public void Init(ClusterItem<char> clusterItem)
        {
            _clusterItemView.SetClusterItemValue(clusterItem.Value);
        }
    }
}