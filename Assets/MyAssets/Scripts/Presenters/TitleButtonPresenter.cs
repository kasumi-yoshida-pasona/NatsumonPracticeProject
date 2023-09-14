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
        [SerializeField] private Button startBtn;
        [SerializeField] private Button finishBtn;

        // View
        [SerializeField] private TitleButtonView _view;
        // Model
        [SerializeField] private TitleButtonModel _model;

        void Awake()
        {
            _model = new TitleButtonModel();
            _model.ChangeBtnStatusToProcessing("btn");
        }
    }

}
