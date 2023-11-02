using UnityEngine;
using UniRx;
using System;

namespace natsumon
{
    public class LoadingPresenter : MonoBehaviour
    {
        // 親のCanvas
        [SerializeField] Canvas parent;
        [SerializeField] GameObject loadingPrefab;
        private LoadingModel loadingModel;

        // 親Presenterに通知するためのSubject
        private Subject<Unit> initTitleSceneButton = new Subject<Unit>();
        public IObservable<Unit> InitTitleSceneButton() => initTitleSceneButton;

        private GameObject obj;

        private float loadingProgressCorrectionFactor = 1f / 0.9f;


        private void Awake()
        {
            loadingModel = new LoadingModel();
        }

        void OnDestroy()
        {
            loadingModel.Dispose();
        }

        public void StartLoading(string scene)
        {
            obj = Instantiate(loadingPrefab, null);
            var loadingView = obj.GetComponent<LoadingView>();

            // Loading画面表示
            loadingView.ShowLoadingPanel(parent, obj);
            // 次のシーンをModelに保存
            loadingModel.SetNextScene(this, scene);

            loadingModel.LoadingRatio.Subscribe(loadingRatio =>
            {
                // そのままだと0.9までしかいかないので最大1にするための値を掛ける
                loadingView.UnveilFireworksByLoadingRatio(loadingRatio * loadingProgressCorrectionFactor);
            }).AddTo(obj);

        }
    }
}
