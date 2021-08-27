﻿using System;

using Microsoft.AspNetCore.Components;

namespace Majorsoft.Blazor.Components.Notifications
{
	/// <summary>
	/// Properties for individual <see cref="Toast"/> Notifications. 
	/// NOTE: most of the properties can be used with default values from <see cref="ToastContainerGlobalSettings"/> static properties.
	/// </summary>
	public class ToastSettings
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ToastSettings()
		{
			Id = Guid.NewGuid();
			NotificationTime = DateTime.Now;
		}

		/// <summary>
		/// Internal Toast Id
		/// </summary>
		internal Guid Id { get; }
		/// <summary>
		/// Internal Toast created time
		/// </summary>
		internal DateTime NotificationTime { get; }
		/// <summary>
		/// Internal variable for flagging a Toast to be removed.
		/// </summary>
		internal bool IsRemove { get; set; } = false;
		/// <summary>
		/// Internal visibility flag for Toast.
		/// </summary>
		internal bool IsVisible { get; set; } = true;
		/// <summary>
		/// Determines if the Toast item is the last in the list or not.
		/// </summary>
		internal bool IsLastItem { get; set; } = false;


		/// <summary>
		/// HTML Content of the<see cref="Toast"/> notification.
		/// </summary>
		public RenderFragment Content { get; set; }

		/// <summary>
		/// Notification type or severity level.
		/// </summary>
		public NotificationTypes Type { get; set; } = NotificationTypes.Primary;

		/// <summary>
		/// Notification style to show different variant of the same <see cref="Type"/> Toast.
		/// </summary>
		public NotificationStyles NotificationStyle { get; set; } = ToastContainerGlobalSettings.DefaultToastsNotificationStyle;

		/// <summary>
		/// When true Toast will show an icon corresponding to the <see cref="NotificationTypes"/>. Default icon can be overwritten.
		/// </summary>
		public bool ShowIcon { get; set; } = ToastContainerGlobalSettings.DefaultToastsShowIcon;
		/// <summary>
		/// Icon customization it accepts an SVG `Path` value to override the default icon. When empty or NULL it is omitted and default used.
		/// </summary>
		public string CustomIconSvgPath { get; set; } = "";
		/// <summary>
		/// When true Toast will show close "x" button.
		/// </summary>
		public bool ShowCloseButton { get; set; } = ToastContainerGlobalSettings.DefaultToastsShowCloseButton;

		/// <summary>
		/// Toast will close after set time elapsed in Sec.
		/// </summary>
		public uint AutoCloseInSec { get; set; } = ToastContainerGlobalSettings.DefaultToastsAutoCloseInSec;

		/// <summary>
		/// When it's true a progress bar will show the remaining time until Alert closes.
		/// </summary>
		public bool ShowCloseCountdownProgress { get; set; } = ToastContainerGlobalSettings.DefaultToastsShowCloseCountdownProgress;

		private uint _shadowEffect = ToastContainerGlobalSettings.DefaultToastsShadowEffect;
		/// <summary>
		/// Determines the shadow effect strongness which makes Toast elevated. Value should be between 0 and 20.
		/// </summary>
		public uint ShadowEffect
		{
			get => _shadowEffect;
			set
			{
				if (value > 20)
				{
					_shadowEffect = 20;
				}
				else
				{
					_shadowEffect = value;
				}
			}
		}
	}
}