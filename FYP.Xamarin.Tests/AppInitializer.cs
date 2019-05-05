using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;


namespace FYP.Xamarin.Tests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
                    .ApkFile("Z:/Desktop/FYP.Xamarin.Mobile/fyp.xamarin.mobile/FYP.Xamarin.Mobile/FYP.Xamarin.Mobile.Android/bin/Release/com.companyname.FYP.Xamarin.Mobile.Android.apk")
                    .DeviceSerial("emulator-5554")//3063QH3001014002//emulator-5554
                    .PreferIdeSettings()
                    .EnableLocalScreenshots()
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}