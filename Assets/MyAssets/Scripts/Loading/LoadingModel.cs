using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

namespace natsumon
{
    public class LoadingModel
    {
        // ロード率
        private ReactiveProperty<float> loadingRatio = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<float> LoadingRatio { get { return loadingRatio; } }

        private string nextScene;

        // 次のシーンを一時保存してロード開始
        public void SetNextScene(MonoBehaviour mono, string scene)
        {
            nextScene = scene;
            mono.StartCoroutine(ProgressLoadingRatio());
        }

        // ロード率を進行させる
        private IEnumerator ProgressLoadingRatio()
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(nextScene);
            loadingOperation.allowSceneActivation = false;

            var accTime = 0f;
            while (Mathf.Min(accTime, loadingOperation.progress) < 0.9f)
            {
                accTime += Time.deltaTime / 15;
                loadingRatio.Value = Mathf.Min(accTime, loadingOperation.progress);
                yield return null;
            }

            loadingOperation.allowSceneActivation = true;
        }

        public void Dispose()
        {
            loadingRatio.Dispose();
        }
    }

}