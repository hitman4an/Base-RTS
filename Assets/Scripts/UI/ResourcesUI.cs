using TMPro;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    private const string InitialText = "Ресурсы:";

    [SerializeField] private TextMeshProUGUI _text;

    private ResourceCounter _counter;    

    public void ActivateBaseGUI(Base unitBase)
    {
        if (_counter != null)
        {
            _counter.CountChanged -= UpdateCount;
        }

        _counter = unitBase.GetCounter();
        _counter.CountChanged += UpdateCount;
        UpdateCount(_counter.Count);
        gameObject.SetActive(true);
    }

    public void DeactivateBaseGUI()
    {
        gameObject.SetActive(false);
        _counter.CountChanged -= UpdateCount;
        _counter = null;
    }

    private void UpdateCount(int value)
    {
        _text.text = $"{InitialText} {value}";
    }
}
