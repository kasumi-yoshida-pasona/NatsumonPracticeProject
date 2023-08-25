using TitleScene.Models;
using TitleScene.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace TitleScene.Presenters
{
    /// <summary>
    /// ボタン押下時に処理中状態であることをViewに反映するPresenter
    /// </summary>
    public class TitleScenePresenter : MonoBehaviour
    {
        // Model
        [SerializeField] private TitleButtonModel _button;

        // View
        [SerializeField] private TitleLoadingView _loading;

        void Start()
        {

        }
    }

}
