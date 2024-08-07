using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Modules.DialogModule.Scripts
{
    public abstract class Dialog : MonoBehaviour
    {
        [SerializeField] private float _scaleDuration;
        [SerializeField] private Button _closeButton;

        protected virtual void Start()
        {
            if(_closeButton == null) return;
            _closeButton.onClick.AddListener(Hide);
        }

        protected virtual void OnDestroy()
        {
            if(_closeButton == null) return;
            _closeButton.onClick.RemoveListener(Hide);
        }

        public virtual void Show()
        {
            transform.DOScale(Vector3.one, _scaleDuration);
        }

        public virtual void Hide()
        {
            Destroy(gameObject);
        }
    }
}