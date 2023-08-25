using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace TitleScene.Models
{
    public class TitleButtonModel : MonoBehaviour
    {
        [SerializeField] private Button startBtn;
        [SerializeField] private Button finishBtn;

        /// <summary>
        /// それぞれのボタンが処理中かどうかのフラグを保持
        /// </summary>
        private bool isStartBtnProcessing = false;
        private bool isFinishBtnProcessing = false;

        /// <summary>
        /// ボタンの処理
        /// ボタン押下時に処理中へステータスを変更する
        /// 変更したらPresenterを通してViewへ処理中の情報を渡す
        /// </summary>
        private void ChangeBtnStatusToProcessing(string btn)
        {

        }
    }
}
