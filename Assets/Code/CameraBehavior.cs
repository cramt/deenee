using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Deenee {

    public class CameraBehavior : MainBehavior {
        public float dragSpeed = 0.1f;
        public float movespeed = 0.1f;
        public override void OnStart(OnStartProperties onStartProperties) {
            base.OnStart(onStartProperties);
        }
        private Vector3 handleOpenMenuClickPosStart;
        private GameObject currentMenu = null;
        private void HandleOpenMenu() {
            if(Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Escape)) {
                if(currentMenu != null) {
                    InvokeFunction(() => {
                        Destroy(currentMenu);
                    });
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                handleOpenMenuClickPosStart = Input.mousePosition;
            }
            if (Input.GetKeyUp(KeyCode.Mouse1)) {
                if (handleOpenMenuClickPosStart == Input.mousePosition) {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit)) {
                        var menu = hit.transform.GetComponent<IMenuOpenable>()?.Menu;
                        if (menu != null) {
                            menu.Function?.Invoke();
                            if (menu.SubEntires != null) {
                                void build(GameObject gObject, Dictionary<string, MenuEntry> dic) {
                                    GameObject wrapper = new GameObject();
                                    wrapper.transform.SetParent(gObject.transform);
                                    wrapper.transform.localPosition = Vector3.zero;
                                    float currHeight = 0;
                                    List<GameObject> buttons = dic.Select(x => {
                                        GameObject g = Instantiate(Resources.Load<GameObject>("ButtonTemplate"));
                                        g.transform.SetParent(wrapper.transform);
                                        g.transform.localPosition = Vector3.zero + (Vector3.down * currHeight);
                                        g.transform.GetChild(0).GetComponent<Text>().text = x.Key;
                                        Button but = g.GetComponent<Button>();
                                        RectTransform rectTransform = g.GetComponent<RectTransform>();
                                        if (x.Value.Function != null) {
                                            but.onClick.AddListener(new UnityEngine.Events.UnityAction(x.Value.Function));
                                        }
                                        if(x.Value.SubEntires != null) {
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
                                                    for(int j = 0; j < child.childCount; j++) {
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
                                    currentMenu = wrapper;
                                }
                                GameObject canvas = GameObject.Find("Canvas");
                                GameObject firstWrap = new GameObject("firstWrap");
                                firstWrap.transform.SetParent(canvas.transform);
                                firstWrap.transform.position = Input.mousePosition;
                                build(firstWrap, menu.SubEntires);

                                
                                
                            }
                        }
                    }
                }
            }
        }
        private void HandleRotation() {
            if (Input.GetKey(KeyCode.Mouse1)) {
                var pos1 = Input.mousePosition;
                InvokeFunction(() => {
                    var pos2 = Input.mousePosition;
                    var diff = pos1 - pos2;
                    transform.eulerAngles += new Vector3(-diff.y, diff.x, 0) * dragSpeed;
                });
            }
        }
        private void HandleMovement() {
            void move(Vector3 direction) {
                transform.Translate(transform.rotation * direction * movespeed, Space.World);
            }
            if (Input.GetKey(KeyCode.W)) {
                move(Vector3.forward);
            }
            if (Input.GetKey(KeyCode.A)) {
                move(Vector3.left);
            }
            if (Input.GetKey(KeyCode.S)) {
                move(Vector3.back);
            }
            if (Input.GetKey(KeyCode.D)) {
                move(Vector3.right);
            }
            if (Input.GetKey(KeyCode.LeftShift)) {
                move(Vector3.down);
            }
            if (Input.GetKey(KeyCode.Space)) {
                move(Vector3.up);
            }
        }
        public override void OnUpdate(OnUpdateProperties onUpdateProperties) {
            base.OnUpdate(onUpdateProperties);
            HandleRotation();
            HandleMovement();
            HandleOpenMenu();

        }

    }
}