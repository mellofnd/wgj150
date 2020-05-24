using System;
using TMPro;
using UnityEngine;

public class UIVersion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_copyrightText;

    [SerializeField] private TextMeshProUGUI m_versionText;

    private static string s_copyright;

    private static string s_version;

    private static string s_date;

    private void Awake()
    {
        if (Application.isEditor)
        {
            s_copyright = $"(c){DateTime.Today.Year} {Application.companyName}";
            s_version = $"v.{Application.version}";
            s_date = $"{DateTime.Today:yyyy-MM-dd}";
        }

        if (m_copyrightText)
            m_copyrightText.text = s_copyright;

        if (m_versionText)
            m_versionText.text = $"{s_version} ({s_date})";
    }
}