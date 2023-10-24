using UnityEngine;
using UniRx;
using System;

namespace natsumon
{
    public class DialogButtonPresenter : MonoBehaviour
    {
        [SerializeField] DialogView dialogView;
        // [SerializeField] DialogPresenter dialogPresenter;
        private CommonButtonPresenter commonButtonPresenter;
        private ButtonModel buttonModel;
        // 親Presenterに通知するためのSubject
        private Subject<Unit> dialogDestroyed = new Subject<Unit>();
        public IObservable<Unit> DialogDestroyed() => dialogDestroyed;

        // dialogのbutton
        private ButtonView cancelBtn;
        private ButtonView exitBtn;


        private void Awake()
        {
            buttonModel = new ButtonModel();
            commonButtonPresenter = new CommonButtonPresenter();
        }

        void OnDestroy()
        {
            buttonModel.Dispose();
        }

        public void Start()
        {
            cancelBtn = dialogView.cancelBtn;
            exitBtn = dialogView.exitBtn;

            // modelで選択されたボタン情報が変更されたときの処理
            buttonModel.SelectedBtn.Subscribe(btn =>
            {
                cancelBtn.OnSelected(btn);
                exitBtn.OnSelected(btn);
            }).AddTo(this);

            // modelで押下されたボタン情報が変更されたときの処理
            buttonModel.PushedBtn.Subscribe(btn =>
            {
                if (btn == cancelBtn.TargetBtn)
                {
                    // ダイアログを壊してタイトルのボタンを初期化
                    // titleScenePresenter.Init();
                    // Destroy(this);
                    // ここまでOK
                    // これって購読されないですか？
                    dialogDestroyed.OnNext(Unit.Default);
                }
                else if (btn == exitBtn.TargetBtn)
                {
                    // ゲーム終了
                    exitBtn.EndGame();
                }
            }).AddTo(this);

            // Modelに選択されたボタン格納
            commonButtonPresenter.StoreSelectedBtnToModel(cancelBtn, buttonModel);
            commonButtonPresenter.StoreSelectedBtnToModel(dialogView.exitBtn, buttonModel);

            // Modelに押下されたボタン格納
            commonButtonPresenter.StorePushedBtnToModel(cancelBtn.TargetBtn, buttonModel);
            commonButtonPresenter.StorePushedBtnToModel(exitBtn.TargetBtn, buttonModel);

        }
    }

}
