using Source.Modules.GameLogicModule.Scripts.Clusters;
using Source.Modules.GameLogicModule.Scripts.Clusters.ClusterItem;
using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Words.WordCells
{
    public class WordCellController : MonoBehaviour
    {
        [SerializeField] private WordCellView _wordCellView; 
        public WordController WordController { get; private set; }
   
        private WordCellModel _wordCellModel;
        private ClusterController _linkedClusterController;

        public void Initialize(WordController wordController)
        {
            _wordCellModel = new WordCellModel();
            WordController = wordController;
        }
        
        public bool IsEmptyCell() =>
            _wordCellModel.WorldCellItem == null;
        
        public void StartAnimate()
        {
            _wordCellView.Active();
        }

        public void StopAnimate()
        {
            _wordCellView.DeActive();
        }

        public void SetCellIndex(int index)
        {
            _wordCellModel.SetCellIndex(index);
        }

        public int GetCellIndex() => _wordCellModel.CellIndex;
        
        public void UpdateCellItem(ClusterItem<char> clusterItem)
        {
            _wordCellModel.UpdateCellItem(clusterItem);
        }

        public bool CanAddCluster(ClusterController clusterController)
        {
            return WordController.CanAddCluster(clusterController.GetCluster(), _wordCellModel.CellIndex);
        }
        
        public void AddCluster(ClusterController clusterController)
        {
            if (CanAddCluster(clusterController) == false)
            {
                return;
            }
            
            _linkedClusterController = clusterController;
            _linkedClusterController.BeginDrag += OnLinkedClusterBeginDrag;
            WordController.AddCluster(clusterController.GetCluster(), _wordCellModel.CellIndex);
        }

        private void RemoveCluster(ClusterController clusterController)
        {
            _linkedClusterController.BeginDrag -= OnLinkedClusterBeginDrag;
            _linkedClusterController = null;
            WordController.RemoveCluster(clusterController.GetCluster());
        }

        private void OnLinkedClusterBeginDrag(ClusterController obj)
        {
            RemoveCluster(_linkedClusterController);
        }
    }
}