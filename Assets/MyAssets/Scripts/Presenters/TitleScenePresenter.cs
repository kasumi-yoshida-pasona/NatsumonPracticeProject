using UnityEngine;
using UniRx;
using System;

namespace natsumon
{
    public class TitleScenePresenter : MonoBehaviour
    {
        // 子Presenter
        [SerializeField] private TitleButtonPresenter titleButtonPresenter;
        [SerializeField] private DialogPresenter dialogPresenter;

        void Start()
        {
            titleButtonPresenter.startBtnPressed().Subscribe(_ =>
            {
                // // ローディングシーンへ移動
                //     onStartBtnPressed?.Invoke();
            });

            titleButtonPresenter.finishBtnPressed().Subscribe(_ =>
            {
                dialogPresenter.SetOnFinishBtnPressed();
            });

            // ダイアログが破壊されたらタイトルボタンを初期化する
            dialogPresenter.DialogDestroyed().Subscribe(_ =>
            {
                Debug.Log($"Hello");
                titleButtonPresenter.Init();
            });

        }

    }

}
