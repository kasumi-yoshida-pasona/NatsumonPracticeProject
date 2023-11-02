using UnityEngine;
using UnityEngine.UI;
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

        public void SetNextScene(MonoBehaviour mono,string scene)
        {
            nextScene = scene;
            mono.StartCoroutine(ProgressLoadingRatio());
        }

        private IEnumerator ProgressLoadingRatio()
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(nextScene);
            loadingOperation.allowSceneActivation = false;

            var accTime = 0f;
            while (Mathf.Min(accTime, loadingOperation.progress) < 0.9f)
            {
                accTime += Time.deltaTime / 5;
                loadingRatio.Value = Mathf.Min(accTime, loadingOperation.progress);
                yield return null;
            }
        }

        public void Dispose()
        {
            loadingRatio.Dispose();
        }
    }

}