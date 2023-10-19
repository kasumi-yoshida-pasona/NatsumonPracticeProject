using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using UnityEngine.EventSystems;


namespace natsumon
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField] ButtonView ExitBtn;
        public ButtonView exitBtn => ExitBtn;
        [SerializeField] ButtonView CancelBtn;
        public ButtonView cancelBtn => CancelBtn;

        private void Awake() {
            // 初期選択をCancelする
            EventSystem.current.SetSelectedGameObject(cancelBtn.gameObject);
        }

        public void ShowDialog(Canvas parent, GameObject dialog)
        {
            dialog.transform.SetParent(parent.transform, false);
        }
    }
}
