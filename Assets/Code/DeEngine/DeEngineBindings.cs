using Jint;
using Jint.Native;
using Jint.Native.Object;
using Jint.Runtime.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using CanvasBehavior = UnityEngine.Canvas;
using TextBehavior = UnityEngine.UI.Text;

namespace Deenee.DeEngine {
    public class DeEngineBindings {
        private static MapBehavior currentMap;
        public class Console {
            public static readonly Console Instance = new Console();
            private Console() { }
            public void log(params object[] stuff) {
                System.Console.WriteLine(string.Join(", ", stuff.ToList().Select(x => x.ToString()).ToArray()));
            }
        }

        public class Hooks {
            public static Hooks Instance = new Hooks();
            private Hooks() { }

            private Dictionary<string, List<Func<JsValue, JsValue[], JsValue>>> m_listeners = new Dictionary<string, List<Func<JsValue, JsValue[], JsValue>>>();
            public Hooks addListener(string even, Func<JsValue, JsValue[], JsValue> cb) {
                if (!m_listeners.ContainsKey(even)) {
                    m_listeners.Add(even, new List<Func<JsValue, JsValue[], JsValue>>());
                }
                m_listeners[even].Add(cb);
                return this;
            }
            public Hooks on(string even, Func<JsValue, JsValue[], JsValue> cb) {
                return addListener(even, cb);
            }
            public Hooks once(string even, Func<JsValue, JsValue[], JsValue> cb) {
                return addListener(even, new Func<JsValue, JsValue[], JsValue>((a, b) => {
                    m_listeners[even].Remove(cb);
                    return cb(a, b);
                }));
            }
            public Hooks removeListener(string even, Func<JsValue, JsValue[], JsValue> cb) {
                if (m_listeners.ContainsKey(even)) {
                    m_listeners[even].Remove(cb);
                }
                return this;
            }
            public Hooks removeAllListeners(string even) {
                if (even == null) {
                    m_listeners.Clear();
                    return this;
                }
                if (m_listeners.ContainsKey(even)) {
                    m_listeners.Remove(even);
                }
                return this;
            }
            public Func<JsValue, JsValue[], JsValue>[] listeners(string even) {
                if (m_listeners.ContainsKey(even)) {
                    return m_listeners[even].ToArray();
                }
                return new Func<JsValue, JsValue[], JsValue>[0];
            }
            public bool emit(string even, params JsValue[] args) {
                if (m_listeners.ContainsKey(even) && m_listeners[even].Count > 0) {
                    m_listeners[even].ForEach(x => {
                        x?.Invoke(JsValue.Undefined, args);
                    });
                }
                return false;
            }
            public static void OnMapLoad(MapBehavior map) {
                currentMap = map;
                Instance.emit("onMapLoad");
            }
        }
        public class Promise {
            public Promise(Func<JsValue, JsValue[], JsValue> func) {
                Task.Factory.StartNew(() => {

                });
            }
        }
        public abstract class UIElement {
            protected GameObject gameObject { get; set; }
            protected List<UIElement> children { get; set; } = new List<UIElement>();
            protected UIElement parent { get; set; }
            public Transform transform {
                get {
                    return gameObject.transform;
                }
            }
            private void Load() {
                gameObject = new GameObject();
            }
            public UIElement() {
                if (Thread.CurrentThread == Main.UnityThread) {
                    Load();
                }
                else {
                    currentMap.InvokeFunction(Load).Wait();
                }
            }

            public virtual UIElement addChild(UIElement child) {
                child.gameObject.transform.parent = gameObject.transform;
                children.Add(child);
                return this;
            }
            public virtual UIElement addParent(UIElement parent) {
                gameObject.transform.parent = parent.gameObject.transform;
                return this;
            }
            public virtual object getParent() {
                if (parent == null) {
                    return JsValue.Null;
                }
                return parent;
            }
        }
        public class UICanvas : UIElement {
            public CanvasBehavior canvasBehavior;
            public CanvasScaler canvasScaler;
            public GraphicRaycaster graphicRaycaster;
            public RectTransform rectTransform;

            public UICanvas() {
                canvasBehavior = gameObject.AddComponent<CanvasBehavior>();
                canvasBehavior.worldCamera = Camera.main;
                canvasBehavior.renderMode = RenderMode.WorldSpace;
                canvasScaler = gameObject.AddComponent<CanvasScaler>();
                canvasScaler.scaleFactor = 10f;
                canvasScaler.dynamicPixelsPerUnit = 10f;
                graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
                rectTransform = gameObject.GetComponent<RectTransform>();
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);
            }
        }
        public class UIText : UIElement {
            public string text {
                get {
                    return textBehavior.text;
                }
                set {
                    textBehavior.text = value;
                }
            }
            public TextBehavior textBehavior;
            public UIText(JsValue args) {
                TextAnchor alignment = TextAnchor.MiddleCenter;
                HorizontalWrapMode horizontalOverflow = HorizontalWrapMode.Overflow;
                VerticalWrapMode verticalOverflow = VerticalWrapMode.Overflow;
                int fontSize = 7;
                if (args.IsObject()) {
                    var jsAlignment = args.AsObject().Get("alignment");
                    if (!jsAlignment.IsString()) {
                        Enum.TryParse(jsAlignment.AsString(), out alignment);
                    }
                    var jsHorizontalOverflow = args.AsObject().Get("horizontalOverflow");
                    if (!jsHorizontalOverflow.IsString()) {
                        Enum.TryParse(jsHorizontalOverflow.AsString(), out horizontalOverflow);
                    }
                    var jsVerticalOverflow = args.AsObject().Get("verticalOverflow");
                    if (!jsVerticalOverflow.IsString()) {
                        Enum.TryParse(jsVerticalOverflow.AsString(), out verticalOverflow);
                    }
                    var jsFontSize = args.AsObject().Get("fontSize");
                    if (!jsFontSize.IsString()) {
                        Enum.TryParse(jsFontSize.AsString(), out fontSize);
                    }
                }
                else if (args.IsString()) {
                    text = args.AsString();
                }
                textBehavior = gameObject.AddComponent<TextBehavior>();
                var rectTransform = gameObject.GetComponent<RectTransform>();
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);
                textBehavior.alignment = alignment;
                textBehavior.horizontalOverflow = horizontalOverflow;
                textBehavior.verticalOverflow = verticalOverflow;
                Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                textBehavior.font = ArialFont;
                textBehavior.fontSize = fontSize;
                textBehavior.enabled = true;
                textBehavior.color = Color.black;
            }
        }
        public class Component : UIElement {
            public JsValue props;
            public Component(Func<JsValue, JsValue[], JsValue> loadFunc, JsValue props) {
                this.props = props;
                addChild((UIElement)loadFunc(JsValue.Undefined, new JsValue[] { props }).ToObject());

            }
        }
        public static Func<Func<JsValue, JsValue[], JsValue>, object> createComponent = new Func<Func<JsValue, JsValue[], JsValue>, object>((Func<JsValue, JsValue[], JsValue> loadFunc) => {
            return new {
                construct = new Func<JsValue, Component>((JsValue args) => new Component(loadFunc, args)),
            };
        });
    }
}