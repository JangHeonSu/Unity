using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    public float speed = 10f;
    public float health;
    public float deathPrice = 50f;
    public GameObject goldMessage;

    public static float maxHealth;
    public static float currentHealth;

    private Transform target;
    private int wavePointIndex = 0;

    public Image healthBarImage;

    void Start()
    {
        target = WayPoints.wayPoints[0];
        maxHealth = health;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position)<=0.1f)
        {
            GetNextWaypoint();
        }

        currentHealth = health;

        float fillAmount = currentHealth / maxHealth;
        healthBarImage.fillAmount = fillAmount;
        healthBarImage.color = Color.Lerp(Color.green, Color.red, 1 - fillAmount);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0) 
        {
            PlayerStats.Money += deathPrice;

            GameObject messageObject = Instantiate(goldMessage);
            TextMeshProUGUI messageText = messageObject.GetComponentInChildren<TextMeshProUGUI>();
            messageText.text = "+ " + deathPrice;

            messageObject.transform.position = transform.position + new Vector3(0f,0f,5f);

            Destroy(gameObject);
            Destroy(messageObject,1f);
        }
    }
    void GetNextWaypoint()
    {
        if(wavePointIndex >= WayPoints.wayPoints.Length-1)
        {
            Destroy(gameObject);
            PlayerStats.lives--;
            return;
        }

        wavePointIndex++;
        target = WayPoints.wayPoints[wavePointIndex];
    }

}
