using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;


namespace natsumon
{
    public class LoadingView : MonoBehaviour
    {
        float loadingRatio = 0f;
        [SerializeField] private Image FireworkImage;
        [SerializeField] private GameObject loadingWords;
        private List<RectTransform> WordsList = new List<RectTransform>();
        private Sequence sequence;
        private ReactiveProperty<bool> isReadyForSettingWords = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> IsReadyForSettingWords { get { return isReadyForSettingWords; } }


        void Start()
        {
            FireworkImage.fillAmount = loadingRatio;
            // RectTransformのレイアウトを即座に更新
            LayoutRebuilder.ForceRebuildLayoutImmediate(loadingWords.GetComponent<RectTransform>());
            setLoadingWordsInList();
        }

        // Loading中の文字をListに格納
        private void setLoadingWordsInList()
        {
            for (int i = 0; i < loadingWords.transform.childCount; i++)
            {
                // 子要素のGameObjectをリストに追加
                WordsList.Add(loadingWords.transform.GetChild(i).gameObject.GetComponent<RectTransform>());
            }

            isReadyForSettingWords.Value = true;
        }

        // 各文字のアニメーションを設定してループ再生
        public void JumpingWords()
        {
            sequence = DOTween.Sequence();
            foreach (var word in WordsList)
            {
                var position = word.transform.position;
                float jumpPower = Random.Range(50.0f, 150.0f);
                sequence.Append(word.DOJump(position, jumpPower, 1, 0.8f));
            }

            // ループ再生部分
            sequence.OnComplete(() => sequence.Restart());
        }

        // ローディングパネルの表示
        public void ShowLoadingPanel(Canvas parent, GameObject loadingPanel)
        {
            loadingPanel.transform.SetParent(parent.transform, false);
        }

        // ロード率に準じて花火の画像を出現
        public void UnveilFireworksByLoadingRatio(float ratio)
        {
            loadingRatio = ratio;
            FireworkImage.fillAmount = loadingRatio;
        }

        // 自動で止まらないためロード率が100%になった時にKillする
        public void StopLoopLoading()
        {
            sequence.Kill();
        }

    }
}
