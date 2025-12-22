using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum WeatherStates
{
    Clear,
    Rain,
    Storming
}
public class WeatherManager : TimeAgent
{
    [Range(0f, 1f)][SerializeField] float chanceToChangeWeather = 0.02f;

    WeatherStates currentWeatherState = WeatherStates.Clear;
    [SerializeField] ParticleSystem rainObject;
    [SerializeField] ParticleSystem stormingObject;
    [SerializeField] ScreenFader screenFader;
    private Coroutine lightningCoroutine;
    [SerializeField] GameObject darkOverlay;

    private void Start()
    {
        Init();
        onTimeTick += RandomWeatherChangeCheck;
        UpdateWeather();
    }
    public void RandomWeatherChangeCheck(GameTime gameTime)
    {
        if (UnityEngine.Random.value < chanceToChangeWeather)
        {
            RandomWeatherChange();
        }
    }
    private void RandomWeatherChange()
    {
        WeatherStates newWeatherState = (WeatherStates)UnityEngine.Random.Range(0, Enum.GetNames(typeof(WeatherStates)).Length);
        ChangeWeather(newWeatherState);
    }

    private void ChangeWeather(WeatherStates newWeatherState)
    {
        currentWeatherState = newWeatherState;
        UpdateWeather();
    }
    private IEnumerator LightningEffect()
    {
        while (currentWeatherState == WeatherStates.Storming)
        {
            // Trigger the lightning flash using the ScreenFader
            screenFader.Tint();

            // Wait for a brief moment (random duration to simulate irregular lightning strikes)
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.3f));

            // Return to normal
            screenFader.UnTint();

            // Wait for the next lightning flash (random delay)
            yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 5f));
        }
    }
    private void UpdateWeather()
    {
        switch (currentWeatherState)
        {
                case WeatherStates.Clear:
                    rainObject.gameObject.SetActive(false);
                    stormingObject.gameObject.SetActive(false);
                    darkOverlay.SetActive(false);
                if (lightningCoroutine != null)
                    {
                        StopCoroutine(lightningCoroutine);
                        lightningCoroutine = null;
                    }
                    break;
                case WeatherStates.Rain:
                    rainObject.gameObject.SetActive(true);
                    stormingObject.gameObject.SetActive(false);
                    darkOverlay.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f); // Light tint
                    darkOverlay.SetActive(true);
                if (lightningCoroutine != null)
                    {
                        StopCoroutine(lightningCoroutine);
                        lightningCoroutine = null;
                    }
                    break;
                case WeatherStates.Storming:
                    rainObject.gameObject.SetActive(false);
                    stormingObject.gameObject.SetActive(true);
                    darkOverlay.GetComponent<Image>().color = new Color(0, 0, 0, 0.7f); // Darker tint
                    darkOverlay.SetActive(true);
                    if (lightningCoroutine == null)
                    {
                        //lightning.GameObject.SetActive(true);
                        lightningCoroutine = StartCoroutine(LightningEffect());
                    }
                    break;
        }
                
        
    }
}
