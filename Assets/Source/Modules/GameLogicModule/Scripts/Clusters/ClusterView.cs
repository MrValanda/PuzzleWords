using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Modules.GameLogicModule.Scripts.Clusters
{
    public class ClusterView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Vector2 _beginDragScale;
        [SerializeField] private Vector2 _endDragScale;
        [SerializeField] private Vector2 _inCellScale;

        private void Start()
        {
            transform.DOScale(_endDragScale, 0);
        }

        public void BeginDrag()
        {
            _image.raycastTarget = false;
            transform.DOScale(_beginDragScale, 0.3f);
        }

        public void SetViewInCell()
        {
            transform.DOScale(_inCellScale, 0.3f);
            _image.raycastTarget = true;
        }
        
        public void EndDrag()
        {
            _image.raycastTarget = true;
            transform.DOScale(_endDragScale, 0.3f);
        }
    }
}