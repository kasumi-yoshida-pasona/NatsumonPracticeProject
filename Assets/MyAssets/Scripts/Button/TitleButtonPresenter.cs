using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;

namespace natsumon
{
    public class TitleButtonPresenter : MonoBehaviour
    {

        // Button
        [SerializeField] private ButtonView startBtnView;
        [SerializeField] private ButtonView finishBtnView;
        private List<ButtonView> titleButtonList = new List<ButtonView>();

        // ButtonPresenterに共通
        private CommonButtonPresenter commonButton;


        // Model
        private ButtonModel buttonModel;

        // スタートボタン押下時に親のTitlePresenterへ通知
        private Subject<Unit> StartBtnPressed = new Subject<Unit>();
        public IObservable<Unit> startBtnPressed() => StartBtnPressed;

        // 終了ボタン押下時に親のTitlePresenterへ通知
        private Subject<Unit> FinishBtnPressed = new Subject<Unit>();
        public IObservable<Unit> finishBtnPressed() => FinishBtnPressed;


        void Awake()
        {
            buttonModel = new ButtonModel();
            commonButton = new CommonButtonPresenter();
            titleButtonList.Add(startBtnView);
            titleButtonList.Add(finishBtnView);
        }

        void OnDestroy()
        {
            buttonModel.Dispose();
        }

        // タイトルボタンの初期化
        public void Init()
        {
            foreach (var btn in titleButtonList)
            {
                btn.ActivateBtn();
            }
            buttonModel.StoreSelectedBtn(startBtnView.TargetBtn);
        }

        void Start()
        {
            foreach (var btn in titleButtonList)
            {
                // ボタンが選択されたときの処理
                commonButton.StoreSelectedBtnToModel(btn, buttonModel);
                // ボタンが押下されたときの処理
                commonButton.StorePushedBtnToModel(btn.TargetBtn, buttonModel);
            }

            // modelで選択されたボタン情報が変更されたときの処理
            buttonModel.SelectedBtn.Subscribe(selectedBtn =>
            {
                foreach (var btn in titleButtonList)
                {
                    btn.OnSelected(selectedBtn);
                }
            }).AddTo(this);

            // modelで押下されたボタン情報が変更されたときの処理
            buttonModel.PushedBtn.Subscribe(pressedBtn =>
            {
                if (!pressedBtn) return;

                if (pressedBtn == startBtnView.TargetBtn)
                {
                    StartBtnPressed.OnNext(Unit.Default);
                }
                else if (pressedBtn == finishBtnView.TargetBtn)
                {
                    FinishBtnPressed.OnNext(Unit.Default);
                }

                commonButton.deactivateAllBtn(titleButtonList);
            });
        }
    }

}
