using UnityEngine;
using UnityEngine.UI;
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
        private List<ButtonView> buttonList = new List<ButtonView>();

        // Model
        private ButtonModel buttonModel;
        public ButtonModel ButtonModel => buttonModel;

        // スタートボタン押下時に親のTitlePresenterへ通知
        private Subject<Unit> StartBtnPressed = new Subject<Unit>();
        public IObservable<Unit> startBtnPressed() => StartBtnPressed;

        // 終了ボタン押下時に親のTitlePresenterへ通知
        private Subject<Unit> FinishBtnPressed = new Subject<Unit>();
        public IObservable<Unit> finishBtnPressed() => FinishBtnPressed;


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

        // タイトルボタンの初期化
        public void Init()
        {
                foreach (var btn in buttonList)
                {
                    btn.ActivateBtn();
                }
                buttonModel.StoreSelectedBtn(startBtnView.TargetBtn);
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

            // modelで押下されたボタン情報が変更されたときの処理
            buttonModel.PushedBtn.Subscribe(pressedBtn => {
                if (!pressedBtn)
                {
                    return;
                }

                if (pressedBtn == startBtnView.TargetBtn)
                {
                    StartBtnPressed.OnNext(Unit.Default);
                } else if (pressedBtn == finishBtnView.TargetBtn) {
                    FinishBtnPressed.OnNext(Unit.Default);
                }

                foreach (var btn in buttonList)
                {
                    btn.ChangeToDisabledBtn();
                }
            });
        }

        // Modelに選択されたボタン格納
        public void StoreSelectedBtnToModel(ButtonView btn)
        {
            btn.OnSelectAsObservable()
                .Subscribe(targetBtn => {
                    buttonModel.StoreSelectedBtn(targetBtn);
                });
        }

        // Modelに押下されたボタン格納
        public void StorePushedBtnToModel(Button btn)
        {
            btn.OnClickAsObservable()
                .Subscribe(_ => {
                    buttonModel.StorePushedBtn(btn);
                });
        }
    }

}
