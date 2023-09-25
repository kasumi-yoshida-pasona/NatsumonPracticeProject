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
    /// ボタン押下時に処理中状態であることをViewに反映するPresenter
    /// </summary>
    public class TitleScenePresenter : MonoBehaviour
    {
        // Button
        [SerializeField] private TitleButtonView startBtnView;
        [SerializeField] private TitleButtonView finishBtnView;
        [SerializeField] private Button startBtn;
        [SerializeField] private Button finishBtn;

        // Model
        [SerializeField] private TitleButtonModel _model;

        void Awake()
        {
            _model = new TitleButtonModel();
        }

        void Start()
        {
            // ボタンが選択されたときの処理
            startBtnView.OnSelectAsObservable()
            .Subscribe(targetBtn => {
                _model.ChangeBtnStatusToSelect(targetBtn);
            })
            .AddTo(this);

            finishBtnView.OnSelectAsObservable()
            .Subscribe(targetBtn => {
                _model.ChangeBtnStatusToSelect(targetBtn);
            })
            .AddTo(this);

            _model.SelectedBtn.Subscribe(targetBtn => {
                switch (targetBtn)
                {
                    case startBtn:
                        Debug.Log("StartBtn is called");
                        break;
                    default:
                        Debug.Log("other is called");
                        break;
                }
            }).AddTo(this);
        }
    }

}
