using TitleScene.Models;
using TitleScene.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace TitleScene.Presenters
{
    public class TitleScenePresenter : MonoBehaviour
    {
        // Button
        [SerializeField] private TitleButtonView startBtnView;
        [SerializeField] private TitleButtonView finishBtnView;

        // Model
        [SerializeField] private TitleButtonModel model;

        void Awake()
        {
            model = new TitleButtonModel();
        }

        void Start()
        {
            // ボタンが選択されたときの処理
            startBtnView.OnSelectAsObservable()
                .Subscribe(targetBtn => {
                    model.ChangeBtnStatusToSelect(targetBtn);
                })
                .AddTo(this);

            finishBtnView.OnSelectAsObservable()
                .Subscribe(targetBtn => {
                    model.ChangeBtnStatusToSelect(targetBtn);
                })
                .AddTo(this);

            // ボタンが押下されたときの処理
            startBtnView.TargetBtn.OnClickAsObservable()
                .Subscribe(_ => {
                    model.ChangeBtnStatusToPushed(startBtnView.TargetBtn);
                })
                .AddTo(this);

            finishBtnView.TargetBtn.OnClickAsObservable()
                .Subscribe(_ => {
                    model.ChangeBtnStatusToPushed(finishBtnView.TargetBtn);
                })
                .AddTo(this);

            // Modelで選択されたボタン情報が変更されたらそのボタン選択状態にする
            model.SelectedBtn
                .Subscribe(selectedBtn => {
                    if (startBtnView.TargetBtn == selectedBtn)
                    {
                        startBtnView.ChangeSelectedBtn();
                    }
                    else if (finishBtnView.TargetBtn == selectedBtn)
                    {
                        finishBtnView.ChangeSelectedBtn();
                    }
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
    }

}
