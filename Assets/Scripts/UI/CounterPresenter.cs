﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WhacAMole.Infrastructure;

namespace WhacAMole.UI
{
    public class CounterPresenter : MonoBehaviour
    {
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private Button _increaseButton;
        [SerializeField] private TextMeshProUGUI _indicator;

        private Counter _counter;

        #region Unity
        private void OnEnable()
        {
            _decreaseButton.onClick.AddListener(Decrease);
            _increaseButton.onClick.AddListener(Increase);
        }

        private void OnDisable()
        {
            _decreaseButton.onClick.RemoveListener(Decrease);
            _increaseButton.onClick.RemoveListener(Increase);
        }
        #endregion

        public void Init(Counter counter)
        {
            _counter = counter;
            SetIndicator(counter.Value);
        }

        public void Enable()
        {
            _decreaseButton.interactable = true;
            _increaseButton.interactable = true;
        }

        public void Disable()
        {
            _decreaseButton.interactable = false;
            _increaseButton.interactable = false;
        }

        private void Decrease() => SetIndicator(_counter.Decrease());

        private void Increase() => SetIndicator(_counter.Increase());

        private void SetIndicator(int value) => _indicator.text = value.ToString();
    }
}
