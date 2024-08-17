using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();

    }

    private void OnEnable()
    {
        CharacterEvents.CharacterDamaged += CharacterTookDamage;
        CharacterEvents.CharacterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.CharacterDamaged -= CharacterTookDamage;
        CharacterEvents.CharacterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tmpText.text = healthRestored.ToString();
    }

    public void OnExit(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.Log($"{name}:{GetType()}:{System.Reflection.MethodBase.GetCurrentMethod().Name}");
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit():
#endif
    }
}
