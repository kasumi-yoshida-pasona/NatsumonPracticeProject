using UnityEngine;
using UniRx;
using System;
using UnityEngine.SceneManagement;

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
            var dialogView = obj.GetComponent<LoadingView>();

            // Loading画面表示
            dialogView.ShowLoading(parent, obj);
            // 次のシーンをModelに保存
            loadingModel.SetNextScene(this, scene);

            loadingModel.LoadingRatio.Subscribe(loadingRatio =>
            {
                Debug.Log(loadingRatio);
            }).AddTo(obj);

        }
    }
}
