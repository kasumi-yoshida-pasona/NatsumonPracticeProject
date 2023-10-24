using UnityEngine.UI;
using UniRx;
using System;
using System.Collections.Generic;

namespace natsumon
{
    public class CommonButtonPresenter
    {
        // Modelに選択されたボタン格納
        public void StoreSelectedBtnToModel(ButtonView btn, ButtonModel buttonModel)
        {
            btn.OnSelectAsObservable()
                .Subscribe(targetBtn =>
                {
                    buttonModel.StoreSelectedBtn(targetBtn);
                });
        }

        // Modelに押下されたボタン格納
        public void StorePushedBtnToModel(Button btn, ButtonModel buttonModel)
        {
            btn.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    buttonModel.StorePushedBtn(btn);
                });
        }

        // 全てのボタンを非活性にする
        public void deactivateAllBtn(List<ButtonView> titleButtonList)
        {
            foreach (var btn in titleButtonList)
            {
                btn.ChangeToDisabledBtn();
            }
        }
    }

}
