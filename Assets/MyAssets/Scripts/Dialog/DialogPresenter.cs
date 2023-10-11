using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

namespace natsumon
{
    public class DialogPresenter : MonoBehaviour
    {
        // 親のCanvas
        [SerializeField] private Canvas parent;
        // 表示するダイアログ
        [SerializeField] private DialogView dialog;
        private DialogModel dialogModel;
        private ButtonModel buttonModel;

        // Button
        private ButtonView exitBtn;
        private ButtonView cancelBtn;

        private void Awake() {
            dialogModel = new DialogModel();
            buttonModel = new ButtonModel();
            exitBtn = dialog.exitBtn;
            cancelBtn = dialog.cancelBtn;
        }

        void OnDestroy()
        {
            dialogModel.Dispose();
        }

        private void Start() {
            exitBtn.OnSelectAsObservable()
                .Subscribe(targetBtn => {
                    buttonModel.StoreSelectedBtn(targetBtn);
                })
                .AddTo(this);
        }

        public void SetOnFinishBtnPressed()
        {
            // // ゲーム終了確認ダイアログ表示
            dialog.ShowDialog(parent);
            // ダイアログが表示されたことをModelに通知
            dialogModel.StoreShowDialog(DialogType.ConfirmCloseGame);
        }
    }

}
