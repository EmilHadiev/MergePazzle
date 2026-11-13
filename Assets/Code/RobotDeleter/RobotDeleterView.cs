using UnityEngine;
using UnityEngine.UI;

public class RobotDeleterView : MonoBehaviour
{
    [SerializeField] private Button _deleteButton;

    private void OnEnable()
    {
        _deleteButton.onClick.AddListener(Delete);
    }

    private void OnDisable()
    {
        _deleteButton.onClick.RemoveListener(Delete);
    }

    private void Delete()
    {

    }
}