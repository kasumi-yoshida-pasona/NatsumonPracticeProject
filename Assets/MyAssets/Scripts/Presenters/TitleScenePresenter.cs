using natsumon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using System;

namespace natsumon
{
    public class TitleScenePresenter : MonoBehaviour
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

            buttonModel.SelectedBtn.Subscribe(selectedBtn => {
                startBtnView.OnSelected(selectedBtn);
                finishBtnView.OnSelected(selectedBtn);
            }).AddTo(this);

            // Modelで押下されたボタン情報が変更されたらすべてのボタンをdisabledにする
            buttonModel.PushedBtn
                .Subscribe(PushedBtn => {
                    if (startBtnView.TargetBtn == PushedBtn)
                    {
                        // ローディングシーンへ移動
                        onStartBtnPressed?.Invoke();
                    } else if (finishBtnView.TargetBtn == PushedBtn)
                    {
                        // ボタンが発火したときに発火した処理をする、押されたときに何をするかダイアログ側でわかる
                        onFinishBtnPressed?.Invoke();
                    }
                }).AddTo(this);

        }

        private Action onStartBtnPressed = null;
        public void SetOnStartBtnPressed(Action a)
        {
            onStartBtnPressed = a;
        }

        private Action onFinishBtnPressed = null;
        public void SetOnFinishBtnPressed(Action a)
        {
            onFinishBtnPressed = a;
        }

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
