using BingDailyWallpaper.Models;
using BingDailyWallpaper.Storage;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace BingDailyWallpaper.Notification
{
    public static class NotificationManager
    {
        private static ToastContent CreateToastContent(Image image)
        {
            int position = image.Copyright.LastIndexOf(" (");
            string copyright = image.Copyright.Substring(0, position);

            return new ToastContent()
            {
                Duration = ToastDuration.Long,

                Launch = typeof(NotificationManager).Namespace,

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = copyright
                            },
                            new AdaptiveText()
                            {
                                Text = "Nouveau fond d'écran 📸"
                            },
                            new AdaptiveImage()
                            {
                                Source = image.FileName
                            }
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Notifies the user that the wallpaper has changed.
        /// </summary>
        /// <param name="image">The new wallpaper. If it is no notification is displayed</param>
        public static void Notify(this Image image)
        {
            if (image == null || Settings.Current.NotificationEnabled == false)
            {
                return;
            }

            ToastContent content = CreateToastContent(image);

            var doc = new XmlDocument();
            doc.LoadXml(content.GetContent());

            var toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier("Test").Show(toast);
        }
    }
}
