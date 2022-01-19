﻿using Assets.Scripts.UI.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class WindowManager : MonoBehaviour
    {
        public static WindowManager Instance;
        public bool IsWindowOpen = false;
        public GameObject slot;

        public Dictionary<KeyCode, string> windowKeys = new Dictionary<KeyCode, string>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Update()
        {
            if(_currentActiveWindow != null && Input.GetKeyDown(KeyCode.Escape))
                CloseWindow(_currentActiveWindow);
        }


        private UiWindow _currentActiveWindow;

        public List<UiWindow> _windows = new List<UiWindow>();

        public void OpenWindow(string windowName)
        {
            if (_currentActiveWindow != null)
            {
                if (_currentActiveWindow.name.Equals(windowName))
                {
                    CloseWindow(_currentActiveWindow);
                    return;
                }
                else
                {
                    CloseWindow(_currentActiveWindow);
                }
            }

            _currentActiveWindow = _windows.Where(a => a.gameObject.name == windowName).FirstOrDefault();
            _currentActiveWindow.gameObject.SetActive(true);

            _currentActiveWindow.OnOpen();
            GameManager.Instance.ToggleCursor(CursorLockMode.None);
            IsWindowOpen = true;

        }

        public void CloseWindow(UiWindow window)
        {
            window.OnClose();
            IsWindowOpen=false;
            _currentActiveWindow.gameObject.SetActive(false);
            _currentActiveWindow = null;
            UiManager.Instance.CloseContextMenu();
            UiManager.Instance.CloseHoverMenu();
            GameManager.Instance.ToggleCursor(CursorLockMode.Locked);
        }

        public UiWindow GetCurrentActive()
        {
            return _currentActiveWindow;
        }
    }
}
