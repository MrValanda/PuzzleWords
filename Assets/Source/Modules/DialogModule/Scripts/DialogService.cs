using UnityEngine;
using Zenject;

namespace Source.Modules.DialogModule.Scripts
{
    public class DialogService
    {
        private readonly Transform _dialogParent;
        private readonly string _dialogsPath;

        public DialogService(Transform dialogParent, string dialogsPath)
        {
            _dialogParent = dialogParent;
            _dialogsPath = dialogsPath;
        }

        public void ShowDialog<T>() where T : Dialog
        {
            T dialog = Resources.Load<T>(_dialogsPath +"/"+typeof(T).Name);
            T instantiate = Object.FindObjectOfType<SceneContext>().Container
                    .InstantiatePrefab(dialog.gameObject, _dialogParent).GetComponent<T>();
             
            instantiate.transform.localScale = Vector3.zero;
            instantiate.Show();
        }
    }
}