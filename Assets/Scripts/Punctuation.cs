using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para gerenciar cenas

public class Punctuation : MonoBehaviour
{
    public static Punctuation instance;

    // As referências agora podem ser privadas, pois serão encontradas via código
    private TextMeshProUGUI scoreText;
    private GameObject bananaObject;

    public float totalScore = 0f;
    public float specialBanana = 0f;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Assina o evento quando este objeto é habilitado
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Cancela a assinatura para evitar erros
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Este método é chamado automaticamente sempre que uma cena é carregada
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Encontra os objetos na nova cena usando suas tags
        // e obtém suas referências novamente.
        GameObject scoreTextGO = GameObject.FindWithTag("ScoreText");
        if (scoreTextGO != null)
        {
            scoreText = scoreTextGO.GetComponent<TextMeshProUGUI>();
        } else {
            Debug.LogWarning("Objeto com a tag 'ScoreText' não encontrado na cena nova.");
        }


        bananaObject = GameObject.FindWithTag("BananaUI");
        if (bananaObject != null)
        {
            // Garante que a UI da banana especial seja atualizada ao carregar a cena
            UpdateSpecialBananaUI();
        } else {
             Debug.LogWarning("Objeto com a tag 'BananaUI' não encontrado na cena nova.");
        }
    }

    public void AddScore()
    {
        totalScore += 1f;
        // Se você quiser que o texto seja atualizado aqui:
        if (scoreText != null) scoreText.text = totalScore.ToString();
    }

    public void IncreaseSpecialBanana()
    {
        specialBanana++;
        GameController.instance.increaseBanana();
        UpdateSpecialBananaUI();
    }

    public void DecreaseSpecialBanana()
    {
        if (specialBanana > 0)
        {
            specialBanana--;
        }
        UpdateSpecialBananaUI();
    }

    // Criei um método separado para manter a lógica de UI organizada
    private void UpdateSpecialBananaUI()
    {
        if (bananaObject == null) return; // Retorna se o objeto não foi encontrado

        if (specialBanana > 0)
        {
            bananaObject.SetActive(true);
            bananaObject.GetComponentInChildren<TextMeshProUGUI>().text = specialBanana < 10 ? $"0{specialBanana}" : specialBanana.ToString();
        }
        else
        {
            bananaObject.SetActive(false);
        }
    }
}