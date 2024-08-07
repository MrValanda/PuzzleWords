using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Clusters
{
    public class ClusterCompositeRoot : MonoBehaviour
    {
        [SerializeField] private ClusterController _clusterController;
        public void Init(Cluster<char> cluster)
        {
            _clusterController.Init(new ClusterModel(cluster, _clusterController.GetComponent<RectTransform>()));
        }
    }
}