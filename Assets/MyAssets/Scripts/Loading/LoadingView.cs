using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;


namespace natsumon
{
    public class LoadingView : MonoBehaviour
    {
        float loadingRatio = 0f;
        [SerializeField] private Image FireworkImage;
        [SerializeField] private GameObject loadingWords;
        private List<GameObject> WordsList = new List<GameObject>();


        void Start()
        {
            FireworkImage.fillAmount = loadingRatio;
            setLoadingWordsInList();
        }

        private void setLoadingWordsInList()
        {
            for (int i = 0; i < loadingWords.transform.childCount; i++)
            {
                // 子要素のGameObjectをリストに追加
                WordsList.Add(loadingWords.transform.GetChild(i).gameObject);
            }

            JumpingWords();
        }

        private void JumpingWords()
        {
            var sequence = DOTween.Sequence();
            foreach (var word in WordsList)
            {
                var position = word.transform.position;
                sequence.Append(word.transform.DOJump(position, 1.0f, 1, 1.0f));
                Debug.Log(position);
            }

            sequence.Play();
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

    }
}
