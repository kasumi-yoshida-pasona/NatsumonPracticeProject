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
        // [SerializeField] private DialogView dialog;
        [SerializeField] TitleButtonPresenter buttonPresenter;
        [SerializeField] GameObject dialogPrefab;
        private DialogModel dialogModel;
        // private ButtonModel buttonModel;

        // Button
        private ButtonView exitBtn;
        private ButtonView cancelBtn;

        private void Awake() {
            dialogModel = new DialogModel();
            // buttonModel = new ButtonModel();
            // exitBtn = dialog.exitBtn;
            // cancelBtn = dialog.cancelBtn;
        }

        void OnDestroy()
        {
            dialogModel.Dispose();
            // buttonModel.Dispose();
        }

        private void Start() {

            // cancelBtn.OnSelectAsObservable()
            //     .Subscribe(targetBtn => {
            //         dialogModel.StoreSelectedBtn(targetBtn);
            //         Debug.Log(targetBtn);
            //     })
            //     .AddTo(this);

            // exitBtn.OnSelectAsObservable()
            //     .Subscribe(targetBtn => {
            //         dialogModel.StoreSelectedBtn(targetBtn);
            //     })
            //     .AddTo(this);

            // modelで選択されたボタン情報が変更されたときの処理
            dialogModel.SelectedBtn.Subscribe(selectedBtn => {
                // cancelBtn.OnSelected(selectedBtn);
                // exitBtn.OnSelected(selectedBtn);
                Debug.Log(selectedBtn);
            }).AddTo(this);
        }

        public void SetOnFinishBtnPressed()
        {
            var obj = Instantiate(dialogPrefab, null);
            var dialogView = obj.GetComponent<DialogView>();
            dialogView.cancelBtn.OnSelectAsObservable()
                .Subscribe(b => {
                Debug.Log(b);
                buttonPresenter.StoreSelectedBtnToModel(dialogView.cancelBtn);
            }).AddTo(obj);

            // // ゲーム終了確認ダイアログ表示
            dialogView.ShowDialog(parent);
            // ダイアログが表示されたことをModelに通知
            dialogModel.StoreShowDialog(DialogType.ConfirmCloseGame);
        }
    }

}
