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

        private int NextSceneNumber = 0;

        public void SetNextScene(Scene scene)
        {
            NextSceneNumber = scene.buildIndex;
            ProgressLoadingRatio();
        }

        private IEnumerator ProgressLoadingRatio()
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(NextSceneNumber);
            loadingOperation.allowSceneActivation = false;

            while (!loadingOperation.isDone)
            {
                loadingRatio.Value = loadingOperation.progress;
                Debug.Log(loadingRatio.Value);
                yield return null;
            }
        }

        public void Dispose()
        {
            loadingRatio.Dispose();
        }
    }

}