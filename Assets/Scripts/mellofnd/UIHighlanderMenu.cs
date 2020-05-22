using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIHighlanderMenu : MonoBehaviour
{
    public bool IsVisible;

    public static List<UIHighlanderMenu> Menus = new List<UIHighlanderMenu>(); 
    
    public int Tier = 0;

    private void Awake()
    {
        Menus.Add(this);
    }

    private void OnDestroy()
    {
        Menus.Remove(this);
    }

    public void SetShow(bool value)
    {
        if (value)
        {
            foreach (var menu in Menus.Where(menu => menu != this && menu.IsVisible && menu.Tier >= Tier))
            {
                menu.SetShow(false);
            }
        }
        
        IsVisible = value;
        gameObject.SetActive(IsVisible);
    }

    public void Toggle()
    {
        SetShow(!IsVisible);   
    }
}