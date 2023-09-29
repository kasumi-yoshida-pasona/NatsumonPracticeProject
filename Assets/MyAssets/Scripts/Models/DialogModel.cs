using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace natsumon
{
    public enum DialogType
    {
        None,
        ConfirmCloseGame
    }

    public class DialogModel
    {
        // Dialogの状態
        public ReactiveProperty<DialogType> ShowingDialogType = new ReactiveProperty<DialogType>();


        // ダイアログの状態を格納
        public void StoreShowDialog(DialogType Dialog)
        {
            ShowingDialogType.Value = Dialog;
            Debug.Log(ShowingDialogType);
        }
    }
}
