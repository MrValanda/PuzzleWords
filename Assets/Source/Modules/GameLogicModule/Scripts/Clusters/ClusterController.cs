using System;
using Source.Modules.GameLogicModule.Scripts.Clusters.ClusterItem;
using Source.Modules.GameLogicModule.Scripts.Words;
using Source.Modules.GameLogicModule.Scripts.Words.WordCells;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Source.Modules.GameLogicModule.Scripts.Clusters
{
    public class ClusterController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<ClusterController> BeginDrag;
        public event Action<ClusterController> EndDrag;
        public event Action<ClusterController> Drag;

        [SerializeField] private ClusterView _clusterView;
        [SerializeField] private ClusterItemSpawner _clusterItemSpawner;

        private ClusterModel _clusterModel;
        private Canvas _canvas;
        private ClusterSpawner _clusterSpawner;
        private WordController _previousWordController;
        private SoundPlayer _soundPlayer;
      
        [Inject]
        private void Construct(Canvas canvas, ClusterSpawner clusterSpawner,SoundPlayer soundPlayer)
        {
            _canvas = canvas;
            _clusterSpawner = clusterSpawner;
            _soundPlayer = soundPlayer;
        }
        
        public void Init(ClusterModel clusterModel)
        {
            _clusterModel = clusterModel;
            _clusterItemSpawner.SpawnClusterItems(clusterModel.Cluster);
        }

        public Cluster<char> GetCluster()
        {
            return _clusterModel.Cluster;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDrag?.Invoke(this);
            _clusterModel.SetParent(_canvas.transform);
            _clusterView.BeginDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Drag?.Invoke(this);
            
            _clusterModel.MoveCluster(eventData.delta/ _canvas.scaleFactor);
            
            GameObject currentGameObject = eventData.pointerCurrentRaycast.gameObject;
            
            if (currentGameObject != null &&
                currentGameObject.TryGetComponent(out WordCellController wordCellController))
            {
                if (_previousWordController != null) _previousWordController.StopAnimateCells();

                if (wordCellController.CanAddCluster(this))
                {
                    wordCellController.WordController.StopAnimateCells();
                    wordCellController.WordController.AnimateWordCells(_clusterModel.Cluster,
                        wordCellController.GetCellIndex());
                }
                else
                {
                    wordCellController.WordController.StopAnimateCells();
                }

                _previousWordController = wordCellController.WordController;
            }
            else if (_previousWordController != null) 
            {
               _previousWordController.StopAnimateCells();
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDrag?.Invoke(this);
            GameObject currentGameObject = eventData.pointerCurrentRaycast.gameObject;
            if (currentGameObject != null &&
                currentGameObject.TryGetComponent(out WordCellController wordCellController) &&
                wordCellController.CanAddCluster(this))
            {
                wordCellController.AddCluster(this);
                _clusterModel.UpdatePosition(wordCellController.transform.position);
                _clusterModel.SetParent(_clusterSpawner.DraggedClustersParent);
                _clusterView.SetViewInCell();
                _soundPlayer.PlaySound(SoundType.SetClusterInCellSound);
            }
            else
            {
                _clusterModel.SetParent(_clusterSpawner.ClustersContainerParent);
                _clusterView.EndDrag();
            }

        }
    }
}