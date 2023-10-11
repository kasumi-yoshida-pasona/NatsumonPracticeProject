using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Collections.Generic;

namespace natsumon
{
    public class ButtonPresenter : MonoBehaviour
    {
        // Button
        [SerializeField] private ButtonView startBtnView;
        [SerializeField] private ButtonView finishBtnView;
        private List<ButtonView> buttonList = new List<ButtonView>();

        // Model
        private ButtonModel buttonModel;


        void Awake()
        {
            buttonModel = new ButtonModel();
            buttonList.Add(startBtnView);
            buttonList.Add(finishBtnView);
        }

        void OnDestroy()
        {
            buttonModel.Dispose();
        }

        void Start()
        {
            foreach (var btn in buttonList)
            {
                // ボタンが選択されたときの処理
                StoreSelectedBtnToModel(btn);
                // ボタンが押下されたときの処理
                StorePushedBtnToModel(btn.TargetBtn);
            }

            // modelで選択されたボタン情報が変更されたときの処理
            buttonModel.SelectedBtn.Subscribe(selectedBtn => {
                foreach (var btn in buttonList)
                {
                    btn.OnSelected(selectedBtn);
                }
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
        private void StoreSelectedBtnToModel(ButtonView btn)
        {
            btn.OnSelectAsObservable()
                .Subscribe(targetBtn => {
                    buttonModel.StoreSelectedBtn(targetBtn);
                })
                .AddTo(this);
        }

        // Modelに押下されたボタン格納
        private void StorePushedBtnToModel(Button btn)
        {
            btn.OnClickAsObservable()
                .Subscribe(_ => {
                    buttonModel.StorePushedBtn(btn);
                })
                .AddTo(this);
        }
    }

}
