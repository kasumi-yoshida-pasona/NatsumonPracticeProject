using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace TitleScene.Models
{
    public class TitleButtonModel
    {

        /// <summary>
        /// ボタンの状態
        /// status: select / unselect = null / processing
        /// </summary>
        private string startBtnStatus = "select";
        private string finishBtnStatus;

        /// <summary>
        /// ボタンの処理
        /// ボタン押下時に処理中へステータスを変更する
        /// 変更したらPresenterを通してViewへ処理中の情報を渡す
        /// </summary>
        public void ChangeBtnStatusToProcessing(string Btn)
        {
            Debug.Log(Btn);
        }


        /// <summary>
        /// ボタンの処理
        /// ボタン押下時に処理中へステータスを変更する
        /// 変更したらPresenterを通してViewへ処理中の情報を渡す
        /// </summary>
        public void ChangeBtnStatusToSelect(string Btn)
        {
            Debug.Log("ChangeBtnStatusToProcessing is called");
            Debug.Log(Btn);
            if (Btn == "START_BTN")
            {
                startBtnStatus = "select";
                finishBtnStatus = null;
            }
            else
            {
                startBtnStatus = null;
                finishBtnStatus = "select";
            }
            // Debug.Log(Btn.colors.normalColor);
        }
    }
}
