using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Client
{
    [RequireComponent(typeof(Button))]
    public class MenuButtonSegment : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button _button;
        public event Action ButtonPressed;
        public event Action<Transform> ButtonMouseEnter;
        public event Action<Transform> ButtonMouseExit;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            if (ButtonPressed != null) _button.onClick.AddListener(ButtonPressed.Invoke);
        }

        private void OnDisable()
        {
            if (ButtonPressed != null) _button.onClick.RemoveListener(ButtonPressed.Invoke);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ButtonMouseEnter?.Invoke(transform);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ButtonMouseExit?.Invoke(transform);
        }
    }
}