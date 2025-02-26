using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    [field: SerializeField] public Button backButton { get; private set; }

    public void BackButtonPressed()
    {
        Destroy(gameObject);
    }
}
