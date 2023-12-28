using UnityEngine;
using UnityEngine.UI;

public class GlobalButtonClickHandler : MonoBehaviour
{
    void Start()
    {
        // 씬 내 모든 버튼을 찾아옵니다.
        Button[] buttons = FindObjectsOfType<Button>();

        // 각 버튼에 대해 클릭 이벤트 처리 함수를 연결합니다.
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
