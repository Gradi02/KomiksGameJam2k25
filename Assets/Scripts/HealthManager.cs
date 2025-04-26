using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject deathEffectPrefab;

    public HealthBar healthBar;
    [SerializeField] private Image screenDimmerImage;
    [SerializeField][Range(0f, 1f)] private float maxDimAlpha = 1f;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private Color dimColor = Color.black;

    private bool isDead = false;
    private Renderer playerRenderer;
    private Collider playerCollider;

    private CharacterController playerCharacterController;
    private Rigidbody playerRigidbody;
    private RigidbodyConstraints originalRigidbodyConstraints;
    private PlayerMovement playerMovementScript;

    private void Awake()
    {
        playerRenderer = GetComponent<Renderer>();
        playerCollider = GetComponent<Collider>();

        playerCharacterController = GetComponent<CharacterController>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerMovementScript = GetComponent<PlayerMovement>();

        if (playerMovementScript == null)
        {
            playerMovementScript = GetComponentInChildren<PlayerMovement>();
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);
        }
        isDead = false;
        SetPlayerVisualsActive(true);
        SetPlayerControlActive(true);
        if (screenDimmerImage != null)
        {
            screenDimmerImage.color = new Color(dimColor.r, dimColor.g, dimColor.b, 0f);
            screenDimmerImage.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(25);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Heal(20);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        CinemachineShake.Instance.ShakeCamera(5f, .1f);

        if (healthBar != null) healthBar.SetCurrentHealth(currentHealth);

        if (currentHealth <= 0) StartCoroutine(Die());
    }

    public void Heal(int amount)
    {
        if (isDead) return;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null) healthBar.SetCurrentHealth(currentHealth);
    }

    private void SetPlayerVisualsActive(bool active)
    {
        if (playerRenderer != null) playerRenderer.enabled = active;
        if (playerCollider != null) playerCollider.enabled = active;
    }

    private void SetPlayerControlActive(bool active)
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = active;
        }

        if (playerCharacterController != null)
        {
            playerCharacterController.enabled = active;
        }

        if (playerRigidbody != null)
        {
            if (active)
            {
                if (playerRigidbody.isKinematic)
                {
                    playerRigidbody.isKinematic = false;
                    playerRigidbody.constraints = originalRigidbodyConstraints;
                }
            }
            else
            {
                if (!playerRigidbody.isKinematic)
                {
                    originalRigidbodyConstraints = playerRigidbody.constraints;
                }
                playerRigidbody.linearVelocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
                playerRigidbody.isKinematic = true;
            }
        }
    }

    private IEnumerator Die()
    {
        if (isDead) yield break;
        isDead = true;

        SetPlayerControlActive(false);

        GameObject deathEffectInstance = null;
        if (deathEffectPrefab != null)
        {
            deathEffectInstance = Instantiate(deathEffectPrefab, transform.position, transform.rotation);
        }

        SetPlayerVisualsActive(false);

        yield return StartCoroutine(FadeScreen(maxDimAlpha, fadeDuration));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
        /*
        yield return new WaitForSeconds(respawnDelay);

        if (deathEffectInstance != null)
        {
            Destroy(deathEffectInstance);
        }

        // transform.position = respawnPoint.position;

        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetCurrentHealth(currentHealth);
        }

        SetPlayerVisualsActive(true);
        SetPlayerControlActive(true);

        yield return StartCoroutine(FadeScreen(0f, fadeDuration));

        isDead = false;
        */
    }

    private IEnumerator FadeScreen(float targetAlpha, float duration)
    {
        if (screenDimmerImage == null) yield break;

        float timer = 0f;
        Color currentColor = screenDimmerImage.color;
        Color targetColor = new Color(dimColor.r, dimColor.g, dimColor.b, targetAlpha);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / duration);
            screenDimmerImage.color = Color.Lerp(currentColor, targetColor, progress);
            yield return null;
        }

        screenDimmerImage.color = targetColor;
    }
}