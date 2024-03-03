using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [HideInInspector]
    public float maxHealth = 100;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public Image healthBar;

    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        var camRotation = _cam.transform.rotation;
        transform.LookAt(transform.position + camRotation * Vector3.back, camRotation * Vector3.back);
    }

    public void UpdateHealth(float amount)
    {
        currentHealth += amount;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
