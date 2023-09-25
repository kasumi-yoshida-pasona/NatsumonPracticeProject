using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

namespace TitleScene.Views
{
    [RequireComponent(typeof(Button))]
    public class TitleButtonView : MonoBehaviour, ISelectHandler
    {

        [SerializeField] Button targetBtn;

        /// <summary>
        /// 処理中であれば待機（ぐるぐる OR disabled）表示にする
        /// </summary>

        private Subject<Button> onSelectSubject = new Subject<Button>();
        public IObservable<Button> OnSelectAsObservable() => onSelectSubject;


        void Awake()
        {
            // // ボタンが選択されたときの処理
            // onSelectSubject.Subscribe(_ =>
            // {
            //     Debug.Log(gameObject.name + " が選択された");
            // }).AddTo(this);

            // targetBtn.OnSelectAsObservable()
            //     .Subscribe(_ => onSelectSubject.OnNext(Button.Default))
            //     .AddTo(this);
        }

        // ISelectHandlerのインターフェイスを実装
        public void OnSelect(BaseEventData eventData)
        {
            onSelectSubject.OnNext(targetBtn);
        }

    }
}
