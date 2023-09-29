using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

namespace natsumon
{
    [RequireComponent(typeof(Button))]
    public class ButtonView : MonoBehaviour, ISelectHandler, IPointerEnterHandler
    {
        [SerializeField] Button targetBtn;
        public Button TargetBtn => targetBtn;
        private Subject<Button> onSelectSubject = new Subject<Button>();
        public IObservable<Button> OnSelectAsObservable() => onSelectSubject;

        // ISelectHandlerのインターフェイスを実装
        public void OnSelect(BaseEventData eventData)
        {
            onSelectSubject.OnNext(targetBtn);
        }

        // IPointerEnterHandlerのインターフェイスを実装
        public void OnPointerEnter(PointerEventData eventData)
        {
            onSelectSubject.OnNext(targetBtn);
            // EventSystem.current.SetSelectedGameObject(targetBtn.gameObject);
        }

        public void OnSelected(Button Btn)
        {
            if (Btn != targetBtn)
            {
                return;
            }
            if (EventSystem.current.alreadySelecting)
            {
                return;
            }
            EventSystem.current.SetSelectedGameObject(Btn.gameObject);
        }

        // ボタンを押下不可に設定
        public void ChangeDisabledBtn()
        {
            targetBtn.interactable = false;
        }

        //ゲーム終了
        public void EndGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

    }
}
