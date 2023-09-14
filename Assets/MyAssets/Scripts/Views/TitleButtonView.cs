using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace TitleScene.Views
{
    [RequireComponent(typeof(Button))]
    public class TitleButtonView : MonoBehaviour, ISelectHandler, IDeselectHandler
    {

        [SerializeField] Button targetBtn;

        /// <summary>
        /// 処理中であれば待機（ぐるぐる OR disabled）表示にする
        /// </summary>
        // GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        private Subject<Unit> onSelectSubject = new Subject<Unit>();
        private Subject<Unit> onDeselectSubject = new Subject<Unit>();

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            // ボタンが選択されたときの処理
            onSelectSubject.Subscribe(_ =>
            {
                Debug.Log(gameObject.name + " が選択された");
            }).AddTo(this);

            // ボタンの選択が解除されたときの処理
            onDeselectSubject.Subscribe(_ =>
            {
                Debug.Log(gameObject.name + " の選択が解除された");
            }).AddTo(this);
        }

        // ISelectHandlerのインターフェース
        public void OnSelect(BaseEventData eventData)
        {
            Debug.Log("Hello");
            onSelectSubject.OnNext(Unit.Default);
        }

        // IDeselectHandlerのインターフェース
        public void OnDeselect(BaseEventData eventData)
        {
            onDeselectSubject.OnNext(Unit.Default);
        }
    }
}
