using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PigeonMail;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CitiesStatesMessages : MonoBehaviour
{
    [SerializeField]
    private Image _flagImage;
    private MailUI.Settings _settings;
    private Dictionary<CityColor, Sprite> _flags = new();

    [Inject]
    public void Construct(MailUI.Settings settings, SignalBus signalBus)
    {
        _settings = settings;
        signalBus.Subscribe<CityDamageSignal>(OnCityDamage);
    }

    private void Awake()
    {
        _flags.Add(CityColor.Red, _settings.redFlag);
        _flags.Add(CityColor.Blue, _settings.blueFlag);
        _flags.Add(CityColor.Yellow, _settings.yellowFlag);
    }

    private async void OnCityDamage(CityDamageSignal signal)
    {
        _flagImage.sprite = _flags[signal.cityTracker.FlagColor];
        _flagImage.gameObject.SetActive(true);
        await UniTask.WaitForSeconds(1.5f);
        _flagImage.gameObject.SetActive(false);
    }
}
