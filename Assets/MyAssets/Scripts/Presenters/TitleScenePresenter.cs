using UnityEngine;
using UniRx;
using System;
using UnityEngine.SceneManagement;

namespace natsumon
{
    public class TitleScenePresenter : MonoBehaviour
    {
        // 子Presenter
        [SerializeField] private TitleButtonPresenter titleButtonPresenter;
        [SerializeField] private DialogPresenter dialogPresenter;
        [SerializeField] private LoadingPresenter loadingPresenter;
        private string nextScene;

        void Start()
        {
            titleButtonPresenter.startBtnPressed().Subscribe(_ =>
            {
                nextScene = "PlayScene";
                // ローディングを表示
                loadingPresenter.StartLoading(nextScene);
            });

            titleButtonPresenter.finishBtnPressed().Subscribe(_ =>
            {
                dialogPresenter.SetOnFinishBtnPressed();
            });

            // ダイアログが破壊されたらタイトルボタンを初期化する
            dialogPresenter.InitTitleSceneButton().Subscribe(_ =>
            {
                titleButtonPresenter.Init();
            });

        }

    }

}
