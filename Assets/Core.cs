using System.Collections;
using System.Collections.Generic;
using Stats;
using TMPro;
using UnityEngine;

public class Core : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI healthText;
    public StatsManager statsManager;
    private Transform target;
    void Start()
    {
        statsManager.OnDamageTaken.AddListener(UpdateHealthText);
        statsManager.OnHealTaken.AddListener(UpdateHealthText);
        target = Camera.main.transform;
        UpdateHealthText();
    }

  
    public void UpdateHealthText()
    {
        healthText.text = statsManager.GetCurrentHealth().ToString();
    }
    private void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget(target);
    }
}
