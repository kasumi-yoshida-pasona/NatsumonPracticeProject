using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using UnityEngine.EventSystems;


namespace natsumon
{
    public class LoadingView : MonoBehaviour
    {

        public void ShowLoading(Canvas parent, GameObject loadingPanel)
        {
            loadingPanel.transform.SetParent(parent.transform, false);
        }
    }
}
