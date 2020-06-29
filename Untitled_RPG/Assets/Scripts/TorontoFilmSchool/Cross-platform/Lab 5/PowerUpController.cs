using System.Collections;
using UnityEngine;
using RPG.Control;
using RPG.Attributes;
using System;

public class PowerUpController : MonoBehaviour
{
    [ShowOnly] public bool godMode;
    [SerializeField] float godPowerUpTimer;

    [ShowOnly] public bool speedMode;
    [Range(1.5f, 2.0f)]
    [SerializeField] public float speedBoostMultiplier;
    [SerializeField] public float speedPowerUpTimer;

    PlayerController playerController;
    Health player;

    float timeSinceSpeedBoost;
    float timeSinceGodBoost;
    bool isGodding;
    bool isSpeeding;
    float currentDefenceRating;

    public float debugGodMode;
    public float debugSpeedMode;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        player = GetComponent<Health>();
    }

    private void Start()
    {
        godMode = false;
        speedMode = false;
        isGodding = false;
        isSpeeding = false;

        timeSinceSpeedBoost = Mathf.Infinity;
        timeSinceGodBoost = timeSinceSpeedBoost;

        if (godPowerUpTimer == 0) godPowerUpTimer = 5.0f;
        if (speedBoostMultiplier == 0) speedBoostMultiplier = 2.0f;
        if (speedPowerUpTimer == 0) speedPowerUpTimer = 10.0f;
    }

    private void Update()
    {
        if (godMode && !isGodding)
        {
            isGodding = true;
            StopCoroutine(StopGodBoost(godPowerUpTimer));
            StartCoroutine(StopGodBoost(godPowerUpTimer));
        }

        if (speedMode && !isSpeeding)
        {
            isSpeeding = true;
            StopCoroutine(StopSpeedBoost(speedPowerUpTimer));
            StartCoroutine(StopSpeedBoost(speedPowerUpTimer));
        }
    }

    private IEnumerator StopSpeedBoost(float timer)
    {
        ActivateSpeedBoost();
        yield return new WaitForSeconds(timer);
        DisableSpeedBoost();
    }

    private void DisableSpeedBoost()
    {
        playerController.moveSpeedFraction /= speedBoostMultiplier;
        debugSpeedMode = playerController.moveSpeedFraction;
        speedMode = false;
        isSpeeding = false;
    }

    private void ActivateSpeedBoost()
    {
        playerController.moveSpeedFraction *= speedBoostMultiplier;
        debugSpeedMode = playerController.moveSpeedFraction;
    }

    private IEnumerator StopGodBoost(float timer)
    {
        ActivateGodMode();
        yield return new WaitForSeconds(timer);
        DisableGodMode();
    }

    private void DisableGodMode()
    {
        player.defenceRating = currentDefenceRating;
        debugGodMode = player.defenceRating;
        godMode = false;
        isGodding = false;
    }

    private void ActivateGodMode()
    {
        currentDefenceRating = player.defenceRating;
        player.defenceRating = 1.0f;
        debugGodMode = player.defenceRating;
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Power Up" && (!isGodding || !isSpeeding))
        {
            switch (c.GetComponent<CollectibleProperty>().collectibleType)
            {
                case CollectibleProperty.CollectibleType.MovementSpeedBoost:
                    speedMode = true;
                    break;
                case CollectibleProperty.CollectibleType.GodModeBoost:
                    godMode = true;
                    break;
            }

            Debug.Log("Destroy object");
            Destroy(c.gameObject);
        }
    }
}
