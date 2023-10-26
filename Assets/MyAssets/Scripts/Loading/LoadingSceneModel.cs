using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

namespace natsumon
{
    public class SceneManagerModel : MonoBehaviour
    {
        // 選択されたボタン
        private ReactiveProperty<float> loadingRatio = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<float> LoadingRatio { get {return loadingRatio;}}
        /// <summary>
        ///  LoadSceneAsyncを行い、Loadの％をPresenterへ通知
        /// 即時実行をfalseにするような実装
        /// </summary>
        public void LoadLoadingScene()
        {
            AsyncOperation async = SceneManager.LoadSceneAsync("LoadingScene");

            while (!async.isDone)
            {
                loadingRatio.Value = async.progress;
                yield return null;
            }
        }

        /// <summary>
        ///  ロード処理が完全に終わるとロード画面のまま破棄されて新しいシーン再生
        /// </summary>
    }

}