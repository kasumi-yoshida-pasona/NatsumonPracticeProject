using UnityEngine;
using UniRx;
using System;

namespace natsumon
{
    public class DialogPresenter : MonoBehaviour
    {
        // 親のCanvas
        [SerializeField] private Canvas parent;
        [SerializeField] TitleButtonPresenter buttonPresenter;
        [SerializeField] GameObject dialogPrefab;
        private DialogModel dialogModel;
        // 親Presenterに通知するためのSubject
        private Subject<Unit> dialogDestroyed = new Subject<Unit>();
        public IObservable<Unit> DialogDestroyed() => dialogDestroyed;


        private void Awake() {
            dialogModel = new DialogModel();
        }

        void OnDestroy()
        {
            dialogModel.Dispose();
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

            // Modelに押下されたボタン格納
            dialogView.cancelBtn.TargetBtn.OnClickAsObservable()
                .Subscribe(_ => {
                    buttonPresenter.StorePushedBtnToModel(dialogView.cancelBtn.TargetBtn);
                });

            dialogView.exitBtn.TargetBtn.OnClickAsObservable()
                .Subscribe(_ => {
                    buttonPresenter.StorePushedBtnToModel(dialogView.exitBtn.TargetBtn);
                });

            // modelで押下されたボタン情報が変更されたときの処理
            buttonPresenter.ButtonModel.PushedBtn.Subscribe(pressedBtn => {
                if (!pressedBtn) return;
                if (pressedBtn == dialogView.cancelBtn.TargetBtn)
                {
                    // dialogが非表示になったことを通知してdialog壊す
                    dialogModel.StoreShowDialog(DialogType.None);
                    Destroy(obj);
                    // destroyしたことをtitleSceneに通知
                    dialogDestroyed.OnNext(Unit.Default);
                } else if (pressedBtn == dialogView.exitBtn.TargetBtn)
                {
                    dialogView.exitBtn.EndGame();
                }
            });


            // ゲーム終了確認ダイアログ表示
            dialogView.ShowDialog(parent, obj);
            // ダイアログが表示されたことをModelに通知
            dialogModel.StoreShowDialog(DialogType.ConfirmCloseGame);
        }
    }

}
