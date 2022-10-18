using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectStardust.Components
{
public class PointerEventAdaptor : MonoBehaviour,
	IPointerEnterHandler,
	IPointerExitHandler,
	IPointerMoveHandler,
	IPointerDownHandler,
	IPointerUpHandler,
	IPointerClickHandler,
	IScrollHandler
{
	[Serializable]
	public class PointerEvent : UnityEvent<PointerEventData>
	{}

	public PointerEvent PointerEnter;
	public PointerEvent PointerExit;
	public PointerEvent PointerMove;
	public PointerEvent PointerDown;
	public PointerEvent PointerUp;
	public PointerEvent PointerClick;
	public PointerEvent Scroll;

	public void OnPointerEnter(PointerEventData eventData)
	{
		PointerEnter.Invoke(eventData);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		PointerExit.Invoke(eventData);
	}

	public void OnPointerMove(PointerEventData eventData)
	{
		PointerMove.Invoke(eventData);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		PointerDown.Invoke(eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		PointerUp.Invoke(eventData);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		PointerClick.Invoke(eventData);
	}

	public void OnScroll(PointerEventData eventData)
	{
		Scroll.Invoke(eventData);
	}
}
}