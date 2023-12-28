using UnityEngine;
using UnityEngine.UI;

public class GlobalButtonClickHandler : MonoBehaviour
{
    void Start()
    {
        // �� �� ��� ��ư�� ã�ƿɴϴ�.
        Button[] buttons = FindObjectsOfType<Button>();

        // �� ��ư�� ���� Ŭ�� �̺�Ʈ ó�� �Լ��� �����մϴ�.
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => HandleButtonClick());
        }
    }

    void HandleButtonClick()
    {
        SoundManager.instance.SfxPlaySound(3, transform.position);
    }
}
