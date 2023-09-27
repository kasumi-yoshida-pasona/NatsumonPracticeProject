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
    public class TitleButtonView : MonoBehaviour, ISelectHandler, IPointerEnterHandler
    {

        [SerializeField] Button targetBtn;
        public Button TargetBtn => targetBtn;

        private Subject<Button> onSelectSubject = new Subject<Button>();
        public IObservable<Button> OnSelectAsObservable() => onSelectSubject;

        // ISelectHandlerのインターフェイスを実装
        public void OnSelect(BaseEventData eventData)
        {
            if (EventSystem.current.currentSelectedGameObject != targetBtn.gameObject)
            {
                onSelectSubject.OnNext(targetBtn);
            }
        }

        // IPointerEnterHandlerのインターフェイスを実装
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (EventSystem.current.currentSelectedGameObject != targetBtn.gameObject)
            {
                onSelectSubject.OnNext(targetBtn);
            }
        }

        // ボタンを選択状態に設定
        public void ChangeSelectedBtn()
        {
            EventSystem.current.SetSelectedGameObject(targetBtn.gameObject);
        }

        // ボタンを押下不可に設定
        public void ChangeDisabledBtn()
        {
            targetBtn.interactable = false;
        }

    }
}
