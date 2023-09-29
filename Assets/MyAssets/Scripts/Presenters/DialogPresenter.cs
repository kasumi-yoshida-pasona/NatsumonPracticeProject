using natsumon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

namespace natsumon
{
    public class DialogPresenter : MonoBehaviour
    {
        [SerializeField] private TitleScenePresenter titleScenePresenter;

        private void Start() {
            // titleScenePresenterで登録しているfinishBtnPressedを購読
            // titleScenePresenter.finishBtnPressed().Subscribe(_ =>)
            titleScenePresenter.SetOnFinishBtnPressed(() => {

            });
        }
    }

}
