using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

        // 選択されたボタン
        private ReactiveProperty<Button> selectedBtn = new ReactiveProperty<Button>();
        public IReadOnlyReactiveProperty<Button> SelectedBtn { get {return selectedBtn;}}

        // 押下されたボタン
        private ReactiveProperty<Button> pushedBtn = new ReactiveProperty<Button>();
        public IReadOnlyReactiveProperty<Button> PushedBtn { get {return pushedBtn;}}


        // ダイアログの状態を格納
        public void StoreShowDialog(DialogType Dialog)
        {
            ShowingDialogType.Value = Dialog;
        }

        // 押下したボタン情報を格納
        public void StorePushedBtn(Button Btn)
        {
            pushedBtn.Value = Btn;
        }

        // 選択したボタン情報を格納
        public void StoreSelectedBtn(Button Btn)
        {
            selectedBtn.Value = Btn;
        }

        public void Dispose()
        {
            ShowingDialogType.Dispose();
            selectedBtn.Dispose();
            pushedBtn.Dispose();
        }
    }
}
