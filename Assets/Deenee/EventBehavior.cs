using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Deenee {
    public class EventBehavior : EventTrigger {
        private List<Action<PointerEventData>> onBeginDrag = new List<Action<PointerEventData>>();

        public void AddOnBeginDrag(Action<PointerEventData> func) {
            onBeginDrag.Add(func);
        }

        public void RemoveOnBeginDrag(Action<PointerEventData> func) {
            onBeginDrag.Remove(func);
        }
        public override void OnBeginDrag(PointerEventData data) {
            onBeginDrag.ForEach(x => x(data));
        }

        private List<Action<BaseEventData>> onDeselect = new List<Action<BaseEventData>>();

        public void AddOnDeselect(Action<BaseEventData> func) {
            onDeselect.Add(func);
        }

        public void RemoveOnDeselect(Action<BaseEventData> func) {
            onDeselect.Remove(func);
        }

        public override void OnDeselect(BaseEventData data) {
            Debug.Log("OnDeselect called.");
        }

        private List<Action<PointerEventData>> onDrag = new List<Action<PointerEventData>>();

        public void AddOnDrag(Action<PointerEventData> func) {
            onDrag.Add(func);
        }

        public void RemoveOnDrag(Action<PointerEventData> func) {
            onDrag.Remove(func);
        }
        public override void OnDrag(PointerEventData data) {
            onDrag.ForEach(x => x(data));
        }

        private List<Action<PointerEventData>> onDrop = new List<Action<PointerEventData>>();

        public void AddOnDrop(Action<PointerEventData> func) {
            onDrop.Add(func);
        }

        public void RemoveOnDrop(Action<PointerEventData> func) {
            onDrop.Remove(func);
        }
        public override void OnDrop(PointerEventData data) {
            onDrop.ForEach(x => x(data));
        }

        private List<Action<PointerEventData>> onEndDrag = new List<Action<PointerEventData>>();

        public void AddOnEndDrag(Action<PointerEventData> func) {
            onEndDrag.Add(func);
        }

        public void RemoveOnEndDrag(Action<PointerEventData> func) {
            onEndDrag.Remove(func);
        }
        public override void OnEndDrag(PointerEventData data) {
            onEndDrag.ForEach(x => x(data));
        }

        private List<Action<PointerEventData>> onInitializePotentialDrag = new List<Action<PointerEventData>>();

        public void AddOnInitializePotentialDrag(Action<PointerEventData> func) {
            onInitializePotentialDrag.Add(func);
        }

        public void RemoveOnInitializePotentialDrag(Action<PointerEventData> func) {
            onInitializePotentialDrag.Remove(func);
        }
        public override void OnInitializePotentialDrag(PointerEventData data) {
            onInitializePotentialDrag.ForEach(x => x(data));
        }

        private List<Action<AxisEventData>> onMove = new List<Action<AxisEventData>>();

        public void AddOnMove(Action<AxisEventData> func) {
            onMove.Add(func);
        }

        public void RemoveOnMove(Action<AxisEventData> func) {
            onMove.Remove(func);
        }
        public override void OnMove(AxisEventData data) {
            onMove.ForEach(x => x(data));
        }

        private List<Action<PointerEventData>> onPointerClick = new List<Action<PointerEventData>>();

        public void AddOnPointerClick(Action<PointerEventData> func) {
            onPointerClick.Add(func);
        }

        public void RemoveOnPointerClick(Action<PointerEventData> func) {
            onPointerClick.Remove(func);
        }
        public override void OnPointerClick(PointerEventData data) {
            onPointerClick.ForEach(x => x(data));
        }

        private List<Action<PointerEventData>> onPointerDown = new List<Action<PointerEventData>>();

        public void AddOnPointerDown(Action<PointerEventData> func) {
            onPointerDown.Add(func);
        }

        public void RemoveOnPointerDown(Action<PointerEventData> func) {
            onPointerDown.Remove(func);
        }
        public override void OnPointerDown(PointerEventData data) {
            onPointerDown.ForEach(x => x(data));
        }

        private List<Action<PointerEventData>> onPointerEnter = new List<Action<PointerEventData>>();

        public void AddOnPointerEnter(Action<PointerEventData> func) {
            onPointerEnter.Add(func);
        }

        public void RemoveOnPointerEnter(Action<PointerEventData> func) {
            onPointerEnter.Remove(func);
        }
        public override void OnPointerEnter(PointerEventData data) {
            onPointerEnter.ForEach(x => x(data));
        }

        private List<Action<PointerEventData>> onPointerExit = new List<Action<PointerEventData>>();

        public void AddOnPointerExit(Action<PointerEventData> func) {
            onPointerExit.Add(func);
        }

        public void RemoveOnPointerExit(Action<PointerEventData> func) {
            onPointerExit.Remove(func);
        }
        public override void OnPointerExit(PointerEventData data) {
            onPointerExit.ForEach(x => x(data));
        }

        private List<Action<PointerEventData>> onPointerUp = new List<Action<PointerEventData>>();

        public void AddOnPointerUp(Action<PointerEventData> func) {
            onPointerUp.Add(func);
        }

        public void RemoveOnPointerUp(Action<PointerEventData> func) {
            onPointerUp.Remove(func);
        }
        public override void OnPointerUp(PointerEventData data) {
            onPointerUp.ForEach(x => x(data));
        }

        private List<Action<PointerEventData>> onScroll = new List<Action<PointerEventData>>();

        public void AddOnScroll(Action<PointerEventData> func) {
            onScroll.Add(func);
        }

        public void RemoveOnScroll(Action<PointerEventData> func) {
            onScroll.Remove(func);
        }
        public override void OnScroll(PointerEventData data) {
            onScroll.ForEach(x => x(data));
        }

        private List<Action<BaseEventData>> onSelect = new List<Action<BaseEventData>>();

        public void AddOnSelect(Action<BaseEventData> func) {
            onSelect.Add(func);
        }

        public void RemoveOnSelect(Action<BaseEventData> func) {
            onSelect.Remove(func);
        }
        public override void OnSelect(BaseEventData data) {
            onSelect.ForEach(x => x(data));
        }

        private List<Action<BaseEventData>> onSubmit = new List<Action<BaseEventData>>();

        public void AddOnSubmit(Action<BaseEventData> func) {
            onSubmit.Add(func);
        }

        public void RemoveOnSubmit(Action<BaseEventData> func) {
            onSubmit.Remove(func);
        }
        public override void OnSubmit(BaseEventData data) {
            onSubmit.ForEach(x => x(data));
        }

        private List<Action<BaseEventData>> onUpdateSelected = new List<Action<BaseEventData>>();

        public void AddOnUpdateSelected(Action<BaseEventData> func) {
            onUpdateSelected.Add(func);
        }

        public void RemoveOnUpdateSelected(Action<BaseEventData> func) {
            onUpdateSelected.Remove(func);
        }
        public override void OnUpdateSelected(BaseEventData data) {
            onUpdateSelected.ForEach(x => x(data));
        }

    }
}