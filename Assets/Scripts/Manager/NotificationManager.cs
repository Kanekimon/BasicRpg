using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Manager
{


    public class NotificationManager : MonoBehaviour
    {
        List<NotificationObject> _activeNotifications = new List<NotificationObject>();


        public float fadeOutDuration;
        public static NotificationManager Instance;
        public GameObject prefab;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }


        private void Update()
        {
            if (_activeNotifications.Count > 0)
            {
                for (int i = _activeNotifications.Count - 1; i >= 0; i--)
                {
                    NotificationObject obj = _activeNotifications[i];
                    if (obj.IsFinished())
                    {
                        StartFadeOut(i, obj);
                    }
                }

            }
        }

        public void ShowNotification(string pText, NotificationType pType, float pDuration = 1.0f)
        {
            GameObject g = Instantiate(prefab);

            g.GetComponent<Image>().color = GetNotificationColor(pType);
            g.transform.Find("NotificationText").GetComponent<Text>().text = pText;
            g.transform.parent = UiManager.Instance.GetNotificationContainer().transform;


            NotificationObject notificationObject = new NotificationObject(pDuration, Time.deltaTime, pText, pType, g);
            _activeNotifications.Add(notificationObject);
        }


        public Color GetNotificationColor(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.info: return new Color(1,1,1,0.5f);
                case NotificationType.success: return new Color(0,1,0,0.5f);
                case NotificationType.error: return new Color(1,0,0,0.5f);
                default: return Color.white;
            }
        }

        private void StartFadeOut(int index, NotificationObject obj)
        {
            StartCoroutine(FadeOut(obj.GetUiElement(), obj, index));
        }

        private IEnumerator FadeOut(GameObject g, NotificationObject obj, int index)
        {
            _activeNotifications.RemoveAt(index);
            CanvasGroup cGroup = g.GetComponent<CanvasGroup>(); 

            float elapsedTime = 0;
            float startValue = cGroup.alpha;
            while (elapsedTime < fadeOutDuration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, 0, elapsedTime / fadeOutDuration);
                cGroup.alpha =  newAlpha;
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            Destroy(g);
        }
    }

    public class NotificationObject
    {
        float duration;
        float startTime;
        string text;
        NotificationType type;
        GameObject uiElement;
        float durationTime;

        public NotificationObject(float pDuration, float pStartTime, string pText, NotificationType pType, GameObject pUiElement)
        {
            this.duration = pDuration;
            this.startTime = pStartTime;
            this.text = pText;
            this.type = pType;
            this.uiElement = pUiElement;
            this.durationTime = pDuration+startTime;
        }


        public bool IsFinished()
        {
            if (durationTime > 0)
            {
                durationTime -= Time.deltaTime;
                return false;
            }
            else
                return true;      
        }

        public GameObject GetUiElement()
        {
            return uiElement;
        }
    }

}
