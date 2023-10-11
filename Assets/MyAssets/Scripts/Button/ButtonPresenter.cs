using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace natsumon
{
    public class ButtonPresenter : MonoBehaviour
    {
        // Button
        [SerializeField] private ButtonView startBtnView;
        [SerializeField] private ButtonView finishBtnView;

        // Model
        private ButtonModel buttonModel;


        void Awake()
        {
            buttonModel = new ButtonModel();
        }

        void OnDestroy()
        {
            buttonModel.Dispose();
        }

        void Start()
        {
            // ボタンが選択されたときの処理
            StoreSelectedBtnToModel(startBtnView);
            StoreSelectedBtnToModel(finishBtnView);

            // ボタンが押下されたときの処理
            StorePushedBtnToModel(startBtnView.TargetBtn);
            StorePushedBtnToModel(finishBtnView.TargetBtn);

            // modelで選択されたボタン情報が変更されたときの処理
            buttonModel.SelectedBtn.Subscribe(selectedBtn => {
                startBtnView.OnSelected(selectedBtn);
                finishBtnView.OnSelected(selectedBtn);
            }).AddTo(this);

        }

        // スタートボタン押下時に親のTitlePresenterへ通知
        public IObservable<Unit> startBtnPressed() =>
        buttonModel.PushedBtn
        .Skip(1)  // 初期値をスキップ
        .Where(btn => btn == startBtnView.TargetBtn)  // "startBtn" のときだけ通知
        .Select(_ => Unit.Default);


        // 終了ボタン押下時に親のTitlePresenterへ通知
        public IObservable<Unit> finishBtnPressed() =>
        buttonModel.PushedBtn
        .Skip(1)  // 初期値をスキップ
        .Where(btn => btn == finishBtnView.TargetBtn)  // "finishBtn" のときだけ通知
        .Select(_ => Unit.Default);

        // Modelに選択されたボタン格納
        private void StoreSelectedBtnToModel(ButtonView Btn)
        {
            Btn.OnSelectAsObservable()
                .Subscribe(targetBtn => {
                    buttonModel.StoreSelectedBtn(targetBtn);
                })
                .AddTo(this);
        }

        // Modelに押下されたボタン格納
        private void StorePushedBtnToModel(Button Btn)
        {
            Btn.OnClickAsObservable()
                .Subscribe(_ => {
                    buttonModel.StorePushedBtn(Btn);
                })
                .AddTo(this);
        }
    }

}
