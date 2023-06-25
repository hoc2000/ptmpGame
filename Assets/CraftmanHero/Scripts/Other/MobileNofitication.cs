using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Notifications.Android;
using UnityEngine;

public class MobileNofitication : MonoBehaviour
{
    //void Start()
    //{
    //    SendMessageNotify(5);
    //}

    //public void SendMessageNotify(int time)
    //{
    //    AndroidNotificationCenter.CancelAllDisplayedNotifications();
    //    var channel = new AndroidNotificationChannel()
    //    {
    //        Id = "channel_id",
    //        Name = "Notifications Channel",
    //        Importance = Importance.Default,
    //        Description = "Reminder notifications",
    //    };
    //    AndroidNotificationCenter.RegisterNotificationChannel(channel);

    //    var notification = new AndroidNotification();
    //    notification.Title = "Hey! The Craftman is misssing you";
    //    notification.Text = "Let's go and fuck them up";

    //    notification.SmallIcon = "my_custom_icon_id";
    //    notification.LargeIcon = "my_custom_large_icon_id";
    //    notification.FireTime = System.DateTime.Now.AddSeconds(time);

    //    var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

    //    if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
    //    {
    //        AndroidNotificationCenter.CancelAllNotifications();
    //        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    //    }
    //}

    //public void CheckDaysAndTimes(int year, int month )
    //{
    //    int days = DateTime.DaysInMonth(year, month); 
    //}
}
