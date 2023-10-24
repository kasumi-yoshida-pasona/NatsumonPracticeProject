using UnityEngine;
using UniRx;
using System;

namespace natsumon
{
    public class DialogPresenter : MonoBehaviour
    {
        // 親のCanvas
        [SerializeField] Canvas parent;
        [SerializeField] TitleButtonPresenter titleButtonPresenter;
        [SerializeField] DialogButtonPresenter dialogButtonPresenter;
        [SerializeField] GameObject dialogPrefab;
        private DialogModel dialogModel;
        // private ButtonModel buttonModel;

        // 親Presenterに通知するためのSubject
        // private Subject<Unit> dialogDestroyed = new Subject<Unit>();
        // public IObservable<Unit> DialogDestroyed() => dialogDestroyed;


        private GameObject obj;


        private void Awake()
        {
            dialogModel = new DialogModel();
        }

        void OnDestroy()
        {
            // dialogDestroyed.OnNext(Unit.Default);
            dialogModel.Dispose();
        }

        public void OnCancelBtnPressed()
        {
            // ダイアログを壊してタイトルのボタンを初期化
            titleButtonPresenter.Init();
            Destroy(obj);
        }

        public void SetOnFinishBtnPressed()
        {
            obj = Instantiate(dialogPrefab, null);
            var dialogView = obj.GetComponent<DialogView>();
            // buttonModel = new ButtonModel();

            // obj.OnDestroyAsObservable().Subscribe(_ =>
            // {
            //     buttonModel.Dispose();
            // }).AddTo(this);

            // ゲーム終了確認ダイアログ表示
            dialogView.ShowDialog(parent, obj);
            // ダイアログが表示されたことをModelに通知
            dialogModel.StoreShowDialog(DialogType.ConfirmCloseGame);

            // ここでDialogButtonPresenterのDialogDestroy()をSubscribeしたかった
            dialogButtonPresenter.DialogDestroyed().Subscribe(_ =>
            {
                Debug.Log($"DialogDestroyed is called");
            }).AddTo(obj);


            // modelで選択されたボタン情報が変更されたときの処理
            // buttonModel.SelectedBtn.Subscribe(btn =>
            // {
            //     dialogView.cancelBtn.OnSelected(btn);
            //     dialogView.exitBtn.OnSelected(btn);
            // }).AddTo(obj);

            // // modelで押下されたボタン情報が変更されたときの処理
            // buttonModel.PushedBtn.Subscribe(btn =>
            // {
            //     if (btn == dialogView.cancelBtn.TargetBtn)
            //     {
            // // ダイアログを壊してタイトルのボタンを初期化
            // titleButtonPresenter.Init();
            // Destroy(obj);
            //     }
            //     else if (btn == dialogView.exitBtn.TargetBtn)
            //     {
            //         // ゲーム終了
            //         dialogView.exitBtn.EndGame();
            //     }
            // }).AddTo(obj);

            // // Modelに選択されたボタン格納
            // dialogView.cancelBtn.OnSelectAsObservable().Subscribe(btn =>
            // {
            //     buttonModel.StoreSelectedBtn(btn);
            // }).AddTo(obj);
            // dialogView.exitBtn.OnSelectAsObservable().Subscribe(btn =>
            // {
            //     buttonModel.StoreSelectedBtn(btn);
            // }).AddTo(obj);

            // // Modelに押下されたボタン格納
            // dialogView.cancelBtn.TargetBtn.OnClickAsObservable().Subscribe(_ =>
            // {
            //     buttonModel.StorePushedBtn(dialogView.cancelBtn.TargetBtn);
            // }).AddTo(obj);
            // dialogView.exitBtn.TargetBtn.OnClickAsObservable().Subscribe(_ =>
            // {
            //     buttonModel.StorePushedBtn(dialogView.exitBtn.TargetBtn);
            // }).AddTo(obj);

        }
    }

}
