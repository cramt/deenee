using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deenee {
    public class OptionsMenu {
        public GameObject MainObject { get; private set; }
        public MenuEntry Menu { get; private set; }
        public void Remove() {
            GameObject.Destroy(MainObject);
        }

        public OptionsMenu(MenuEntry menu, GameObject canvas) {
            Menu = menu;
            menu.Function?.Invoke();
            if (menu.SubEntires != null) {
                void build(GameObject gObject, Dictionary<string, MenuEntry> dic) {
                    GameObject wrapper = new GameObject();
                    wrapper.transform.SetParent(gObject.transform);
                    wrapper.transform.localPosition = Vector3.zero;
                    float currHeight = 0;
                    List<GameObject> buttons = dic.Select(x => {
                        GameObject g = GameObject.Instantiate(Resources.Load<GameObject>("ButtonTemplate"));
                        g.transform.SetParent(wrapper.transform);
                        g.transform.localPosition = Vector3.zero + (Vector3.down * currHeight);
                        g.transform.GetChild(0).GetComponent<Text>().text = x.Key;
                        Button but = g.GetComponent<Button>();
                        RectTransform rectTransform = g.GetComponent<RectTransform>();
                        if (x.Value.Function != null) {
                            but.onClick.AddListener(new UnityEngine.Events.UnityAction(x.Value.Function));
                        }
                        if (x.Value.SubEntires != null) {
                            GameObject newWrapper = new GameObject();
                            newWrapper.transform.SetParent(g.transform);
                            newWrapper.transform.localPosition = (Vector3.right * rectTransform.rect.width / 2) + (Vector3.up * rectTransform.rect.height / 2);
                            build(newWrapper, x.Value.SubEntires);
                            void disable() {
                                foreach (Behaviour ui in newWrapper.GetComponentsInChildren<Behaviour>()) {
                                    ui.enabled = false;
                                }
                            }
                            void enable() {
                                var t = newWrapper.transform.GetChild(0);
                                for (int i = 0; i < t.childCount; i++) {
                                    var child = t.GetChild(i);
                                    child.GetComponents<Behaviour>().ToList().ForEach(z => z.enabled = true);
                                    for (int j = 0; j < child.childCount; j++) {
                                        child.GetChild(j).GetComponents<Behaviour>().ToList().ForEach(y => y.enabled = true);
                                    }
                                }
                            }
                            EventBehavior eventTrigger = but.gameObject.AddComponent<EventBehavior>();
                            eventTrigger.AddOnPointerEnter(data => {
                                enable();
                            });
                            eventTrigger.AddOnPointerExit(data => {
                                disable();
                            });
                            disable();
                        }
                        currHeight += rectTransform.rect.height;
                        return g;
                    }).ToList();
                    RectTransform firstRect = wrapper.transform.GetChild(0).GetComponent<RectTransform>();
                    wrapper.transform.localPosition += (Vector3.down * firstRect.rect.height / 2) + (Vector3.right * firstRect.rect.width / 2);
                }
                GameObject firstWrap = new GameObject("firstWrap");
                firstWrap.transform.SetParent(canvas.transform);
                firstWrap.transform.position = Input.mousePosition;
                MainObject = firstWrap;
                build(firstWrap, menu.SubEntires);
            }
        }
    }
}