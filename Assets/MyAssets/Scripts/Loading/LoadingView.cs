using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace natsumon
{
    public class LoadingView : MonoBehaviour
    {
        float loadingRatio = 0f;
        [SerializeField] private Image FireworkImage;
        [SerializeField] private GameObject loadingWords;
        private List<RectTransform> WordsList = new List<RectTransform>();
        private bool isLoopLoading = true;


        void Start()
        {
            FireworkImage.fillAmount = loadingRatio;
            // RectTransformのレイアウトを即座に更新
            LayoutRebuilder.ForceRebuildLayoutImmediate(loadingWords.GetComponent<RectTransform>());

            setLoadingWordsInList();
        }

        private void setLoadingWordsInList()
        {
            for (int i = 0; i < loadingWords.transform.childCount; i++)
            {
                // 子要素のGameObjectをリストに追加
                WordsList.Add(loadingWords.transform.GetChild(i).gameObject.GetComponent<RectTransform>());
            }

            JumpingWords();
        }

        private void JumpingWords()
        {
            var sequence = DOTween.Sequence();
            foreach (var word in WordsList)
            {
                var position = word.transform.position;
                float jumpPower = Random.Range(5.0f, 25.0f);
                sequence.Append(word.DOJump(position, jumpPower, 1, 0.8f));
            }

            // sequence.Play().OnComplete(() => sequence.Kill());
            sequence.OnComplete(() =>
            {
                if (isLoopLoading)
                {
                    sequence.Restart(); // 最初から再スタート
                }
                else
                {
                    sequence.Kill(); // ループ終了時にSequenceを破棄
                }
            });
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

        public void StopLoopLoading()
        {
            isLoopLoading = false;
        }

        // 呼ばれていない？
        void OnDestroy()
        {
            StopLoopLoading();
        }

    }
}
