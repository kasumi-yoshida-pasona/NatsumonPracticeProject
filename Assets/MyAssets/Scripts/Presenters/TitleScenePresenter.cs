using TitleScene.Models;
using TitleScene.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace TitleScene.Presenters
{
    public class TitleScenePresenter : MonoBehaviour
    {
        // Button
        [SerializeField] private TitleButtonView startBtnView;
        [SerializeField] private TitleButtonView finishBtnView;

        // Model
        [SerializeField] private TitleSceneModel model;

        void Awake()
        {
            model = new TitleSceneModel();
        }

        void Start()
        {
            // ボタンが選択されたときの処理
            StoreSelectedBtnToModel(startBtnView);
            StoreSelectedBtnToModel(finishBtnView);

            // ボタンが押下されたときの処理
            StorePushedBtnToModel(startBtnView.TargetBtn);
            StorePushedBtnToModel(finishBtnView.TargetBtn);

            // Modelで選択されたボタン情報変更されたこと通知
            model.SelectedBtn
                .Subscribe(selectedBtn => {
                    Debug.Log($"selected is changed to {selectedBtn}");
                }).AddTo(this);

            // Modelで押下されたボタン情報が変更されたらすべてのボタンをdisabledにする
            model.PushedBtn
                .Subscribe(PushedBtn => {
                    if (startBtnView.TargetBtn == PushedBtn)
                    {
                        // ローディングシーンへ移動
                    } else if (finishBtnView.TargetBtn == PushedBtn)
                    {
                        // ゲーム終了確認ダイアログ表示
                    }
                }).AddTo(this);
        }

        // Modelに選択されたボタン格納
        private void StoreSelectedBtnToModel(TitleButtonView Btn)
        {
            Btn.OnSelectAsObservable()
                .Subscribe(targetBtn => {
                    model.StoreSelectedBtn(targetBtn);
                })
                .AddTo(this);
        }

        // Modelに押下されたボタン格納
        private void StorePushedBtnToModel(Button Btn)
        {
            Btn.OnClickAsObservable()
                .Subscribe(_ => {
                    model.StorePushedBtn(Btn);
                })
                .AddTo(this);
        }
    }

}
