using UnityEngine;
using UnityEngine.UI;

public class RobotDeleterView : MonoBehaviour
{
    [SerializeField] private Button _deleteButton;

    private IRobotDeleter _deleter;

    private void Awake()
    {
        _deleter = GetComponentInParent<IRobot>().Deleter;
    }

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
        _deleter.Delete();
    }
}