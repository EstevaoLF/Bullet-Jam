using UnityEngine.UI;
using UnityEngine;
using System;

public class EnemyUI : MonoBehaviour
{
    [SerializeField]
    GameObject healthBarPrefab;    
    Image fill;
    float currentFill;

    Transform targetPosition;

    Transform ui;

    [HideInInspector]
    public Transform cam;

    Enemy enemy;

    [SerializeField]
    Vector3 offset;

    float currentHealth;
    float maxHealth;

    Canvas uiCanvas;

    private void OnEnable()
    {
        enemy = GetComponent<Enemy>();
        enemy.OnEnemyHealthChanged += OnHealthChanged;
    }

    private void Start()
    {
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                uiCanvas = canvas;
            }
        }
        SetHealthUI();
    }
    private void OnDisable()
    {
        enemy.OnEnemyHealthChanged -= OnHealthChanged;
    }

    public void SetHealthUI()
    {        
        {            
            cam = Camera.main.transform;

            targetPosition = this.transform;

            ui = Instantiate(healthBarPrefab, uiCanvas.transform).transform;

            fill = ui.GetChild(0).GetComponent<Image>();

            currentFill = 1;
        }
    }

    public void OnHealthChanged(float current, float max)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            currentHealth = current;
            maxHealth = max;
            GetCurrentFill();
            if (currentFill <= 0)
            {
                ui.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
    
    }
    public void GetCurrentFill()
    {
        currentFill = currentHealth / maxHealth;
    }

    void LateUpdate()
    {
        if (fill != null && currentFill != fill.fillAmount)
        {
            fill.fillAmount = Mathf.Lerp(fill.fillAmount, currentFill, 0.5f);
        }
        if (ui!=null)
        {
            ui.transform.forward = cam.forward;
            ui.transform.position = targetPosition.position + offset;
        }
    }
}
