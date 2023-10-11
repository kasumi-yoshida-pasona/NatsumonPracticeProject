using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace natsumon
{
    public class TitleScenePresenter : MonoBehaviour
    {
        [SerializeField] private ButtonPresenter buttonPresenter;

        void Start()
        {
            buttonPresenter.startBtnPressed().Subscribe(_ => {
            // ローディングシーンへ移動
                onStartBtnPressed?.Invoke();
            });

            buttonPresenter.finishBtnPressed().Subscribe(_ => {
                // ボタンが発火したときに発火した処理をする、押されたときに何をするかダイアログ側でわかる
                onFinishBtnPressed?.Invoke();
            });

        }

        private Action onStartBtnPressed = null;
        public void SetOnStartBtnPressed(Action a)
        {
            onStartBtnPressed = a;
        }

        private Action onFinishBtnPressed = null;
        public void SetOnFinishBtnPressed(Action a)
        {
            onFinishBtnPressed = a;
        }
    }

}
