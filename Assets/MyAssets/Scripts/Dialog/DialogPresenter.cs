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
        [SerializeField] TitleButtonPresenter buttonPresenter;
        [SerializeField] GameObject dialogPrefab;
        private DialogModel dialogModel;
        private ButtonModel buttonModel;


        private void Awake() {
            dialogModel = new DialogModel();
            buttonModel = new ButtonModel();
        }

        void OnDestroy()
        {
            dialogModel.Dispose();
            buttonModel.Dispose();
        }

        public void SetOnFinishBtnPressed()
        {
            var obj = Instantiate(dialogPrefab, null);
            var dialogView = obj.GetComponent<DialogView>();

            // Modelに選択されたボタン格納
            {
            dialogView.cancelBtn.OnSelectAsObservable()
                .Subscribe(b => {
                    buttonPresenter.StoreSelectedBtnToModel(dialogView.cancelBtn);
                }).AddTo(obj);

            dialogView.exitBtn.OnSelectAsObservable()
                .Subscribe(b => {
                    buttonPresenter.StoreSelectedBtnToModel(dialogView.exitBtn);
                }).AddTo(obj);
            }

            // modelで選択されたボタン情報が変更されたときの処理
            buttonPresenter.ButtonModel.SelectedBtn.Subscribe(b => {
                dialogView.cancelBtn.OnSelected(b);
                dialogView.exitBtn.OnSelected(b);
            }).AddTo(obj);



            // ゲーム終了確認ダイアログ表示
            dialogView.ShowDialog(parent, obj);
            // ダイアログが表示されたことをModelに通知
            dialogModel.StoreShowDialog(DialogType.ConfirmCloseGame);
        }
    }

}
