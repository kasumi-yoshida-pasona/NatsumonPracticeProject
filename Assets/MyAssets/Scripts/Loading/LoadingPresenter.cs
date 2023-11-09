using UnityEngine;
using UniRx;

namespace natsumon
{
    public class LoadingPresenter : MonoBehaviour
    {
        // 親のCanvas
        [SerializeField] Canvas parent;
        // Loading画面
        [SerializeField] GameObject loadingPrefab;
        private LoadingModel loadingModel;

        private GameObject obj;
        // Modelから渡ってくるロード率が0.9までなので、0.9の時に1になる用の値
        private float loadingProgressCorrectionFactor;


        private void Awake()
        {
            loadingModel = new LoadingModel();
            loadingProgressCorrectionFactor = 1f / 0.9f;
        }

        void OnDestroy()
        {
            loadingModel.Dispose();
        }

        public void StartLoading(string scene)
        {
            obj = Instantiate(loadingPrefab, parent.transform);
            var loadingView = obj.GetComponent<LoadingView>();

            // Loading画面表示
            loadingView.ShowLoadingPanel(parent, obj);
            // 次のシーンをModelに保存
            loadingModel.SetNextScene(this, scene);

            loadingView.IsReadyForSettingWords.Subscribe(isReady =>
            {
                if (isReady)
                {
                    loadingView.JumpingWords();
                }
            }).AddTo(obj);

            loadingModel.LoadingRatio.Subscribe(loadingRatio =>
            {
                // そのままだと0.9までしかいかないので最大1にするための値を掛ける
                loadingView.UnveilFireworksByLoadingRatio(loadingRatio * loadingProgressCorrectionFactor);
                if (loadingRatio == 0.9f)
                {
                    loadingView.StopLoopLoading();
                }
            }).AddTo(obj);

        }
    }
}
