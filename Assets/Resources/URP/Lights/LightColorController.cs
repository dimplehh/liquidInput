using UnityEngine;

public interface ColorSetterInterface
{
    void Refresh();

    void SetColor(float time);
}

[ExecuteInEditMode]
public class LightColorController : MonoBehaviour
{
    [SerializeField] [Range(0,1)] float time;
    [SerializeField] GameObject environMap;
    private ColorSetterInterface[] setters;
    private float currentTime = 0;

    public float timeValue => currentTime;

    public void GetSetters()
    {
        setters = GetComponentsInChildren<ColorSetterInterface>();
        foreach (var setter in setters)
            setter.Refresh();
    }

    private void OnEnable()
    {
        time = environMap.transform.position.x / 200;
        GetSetters();
        UpdateSetters();
    }

    private void OnDisable()
    {
        time = 0;
        UpdateSetters();
    }

    private void LateUpdate() //부하 심할까...?
    {
        if (environMap.transform.position.x < 0)
            time = 0;
        else if (environMap.transform.position.x > 200)
            time = 1;
        else if(0 <= environMap.transform.position.x && environMap.transform.position.x <= 200)
            time = environMap.transform.position.x / 200;
        if (currentTime != time)
            UpdateSetters();
    }

    public void UpdateSetters()
    {
        currentTime = time;

        foreach (var setter in setters)
            setter.SetColor(time);
    }
}
