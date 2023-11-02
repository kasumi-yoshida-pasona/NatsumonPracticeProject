using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace natsumon
{
    public class LoadingView : MonoBehaviour
    {
        float loadingRatio = 0f;
        [SerializeField] private Image FireworkImage;

        void Start()
        {
            FireworkImage.fillAmount = loadingRatio;
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
