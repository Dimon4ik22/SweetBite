using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAd();

    public GameObject adPanel; // ������ �������
    public TMP_Text timerText; // ����� �������

    private void Start()
    {
        StartCoroutine(ShowAdRoutine());
    }

    private IEnumerator ShowAdRoutine()
    {
        while (true)
        {
            // �������� 2 ����� - 3 ������� (120 ������ - 3 ������� = 117 ������)
            yield return new WaitForSeconds(177);

            // ��������� ������� � ������ 3 ������
            adPanel.SetActive(true);
            timerText.gameObject.SetActive(true);
            for (int i = 3; i > 0; i--)
            {
                timerText.text = $"������� �������� ����� {i}...";
                yield return new WaitForSeconds(1);
            }
            // ����� �������
            timerText.gameObject.SetActive(false);
            adPanel.SetActive(true);

            ShowAd();

            // ������������ ������� � ������� ���������� �������, ����� 5 ������
            yield return new WaitForSeconds(2);

            // ������� ������� � ������� � ���������� �����
            adPanel.SetActive(false);
        }
    }
}
