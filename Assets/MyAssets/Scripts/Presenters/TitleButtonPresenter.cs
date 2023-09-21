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
        [SerializeField] private TitleButtonView startBtn;
        [SerializeField] private TitleButtonView finishBtn;

        // Model
        [SerializeField] private TitleButtonModel _model;

        private string START_BTN = "START_BTN";

        void Awake()
        {
            _model = new TitleButtonModel();
            // _model.ChangeBtnStatusToSelect(startBtn);

            // // ボタンが選択されたときの処理
            // _view.OnSelectAsObservable()
            //     .Subscribe(_ =>
            //     {
            //         Debug.Log(" 選択された");
            //         // Debug.Log(gameObject.name + " が選択された");
            //     }).AddTo(this);
        }

        void Start()
        {

            // ボタンが選択されたときの処理
            // _view.OnSelectAsObservable()
            //     .Subscribe(_ =>
            //     {
            //         Debug.Log(" 選択された");
            //         // Debug.Log(gameObject.name + " が選択された");
            //     }).AddTo(this);


            // ボタンが選択されたときの処理
            startBtn.OnSelectAsObservable()
            .Subscribe(_ => _model.ChangeBtnStatusToSelect(START_BTN))
            .AddTo(this);

            finishBtn.OnSelectAsObservable()
            .Subscribe(_ => Debug.Log("finishBtn Selected!"))
            .AddTo(this);
        }
    }

}
