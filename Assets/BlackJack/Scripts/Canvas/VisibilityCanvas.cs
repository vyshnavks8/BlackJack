using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisibilityCanvas : MonoBehaviour
{
   
    [SerializeField] private bool visibility;
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button toggleButton;

    private void OnValidate()
    {
        if (toggleButton == null)
        {
            GetComponent<Button>();
        }
    }

    private void Awake()
    {
        inputField.contentType = TMP_InputField.ContentType.Custom;
        SetIData(visibility);
    }

    private void SetIData(bool visible)
    {
        toggleButton.image.sprite = visible ? on : off;
        inputField.inputType = visibility ? TMP_InputField.InputType.Standard : TMP_InputField.InputType.Password;
        inputField.ForceLabelUpdate();
    }

    private void OnEnable()
    {
        toggleButton.onClick.AddListener(OnToggleClick);
    }

    private void OnDisable()
    {
        toggleButton.onClick.RemoveListener(OnToggleClick);
    }

    private void OnToggleClick()
    {
        visibility = !visibility;
        SetIData(visibility);
    }
}