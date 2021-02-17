using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_VxText = null;
    [SerializeField]
    private TextMeshProUGUI m_VyText = null;
    [SerializeField]
    private TextMeshProUGUI m_VzText = null;

    public void OnRequestUpdateUI(float fx,float fy,float fz)
    {
        UpdateParams(fx,fy,fz);
    }
    private void UpdateParams(float fx, float fy, float fz)
    {
        m_VxText.text = fx + " m/s";
        m_VyText.text = fy + " m/s";
        m_VzText.text = fz + " m/s";
    }
}
