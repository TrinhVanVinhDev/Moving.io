using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : UICanvas
{
    public void ResumeButton()
    {
        Time.timeScale = 1;
        UIManager.Ins.OpenUI<UIGamePlay>();
        Close(0);
    }
}
