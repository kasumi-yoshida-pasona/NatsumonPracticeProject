using TitleScene.Models;
using TitleScene.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace TitleScene.Presenters
{
    /// <summary>
    /// タイトル画面のボタンの処理
    /// </summary>
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

            // Modelで選択されたボタン情報が変更されたらそのボタン選択状態にする
            model.SelectedBtn.Subscribe(selectedBtn => {
                if (startBtnView.TargetBtn == selectedBtn)
                {
                    startBtnView.ChangeSelectedBtn();
                }
                else if (finishBtnView.TargetBtn == selectedBtn)
                {
                    finishBtnView.ChangeSelectedBtn();
                }
            }).AddTo(this);
        }
    }

}
