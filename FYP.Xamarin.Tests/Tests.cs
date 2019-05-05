using System;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace FYP.Xamarin.Tests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;


        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {

            app = AppInitializer.StartApp(platform);
        }


        [Test]
        public void ViewAlertSuggestion()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            Thread.Sleep(8000);
            app.SwipeLeftToRight(0.99, 1000, true);
            app.Tap("NoResourceEntry-58");
            app.SwipeRightToLeft(0.99, 1000, true);
        }

        [Test]
        public void ViewAlerts()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            Thread.Sleep(8000);
            app.SwipeLeftToRight(0.99, 1000, true);
            app.Tap("NoResourceEntry-58");
            app.SwipeRightToLeft(0.99, 1000, true);

        }

        [Test]
        public void ViewSpeedLeaderboard()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            Thread.Sleep(8000);
            app.SwipeLeftToRight(0.99, 1000, true);
            app.Tap("NoResourceEntry-56");
            app.SwipeRightToLeft(0.99, 1000, true);
            app.Tap("NoResourceEntry-91");
            app.Repl();
        }

        [Test]
        public void ViewCadenceLeaderboard()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            Thread.Sleep(8000);
            app.SwipeLeftToRight(0.99, 1000, true);
            app.Tap("NoResourceEntry-56");
            app.SwipeRightToLeft(0.99, 1000, true);
            app.Tap("NoResourceEntry-90");
            app.Repl();
        }

        [Test]
        public void ViewPowerLeaderboard()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            Thread.Sleep(8000);
            app.SwipeLeftToRight(0.99, 1000, true);
            app.Tap("NoResourceEntry-56");
            app.SwipeRightToLeft(0.99, 1000, true);
            app.Tap("NoResourceEntry-89");
            app.Repl();
        }


        [Test]
        public void ViewSpeedTrend()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            Thread.Sleep(8000);
            app.SwipeLeftToRight(0.99, 1000, true);
            app.Tap("NoResourceEntry-54");
            app.SwipeRightToLeft(0.99, 1000, true);
            app.Tap("NoResourceEntry-91");

        }

        [Test]
        public void ViewCadenceTrend()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            Thread.Sleep(8000);
            app.SwipeLeftToRight(0.99, 1000, true);
            app.Tap("NoResourceEntry-54");
            app.SwipeRightToLeft(0.99, 1000, true);
            app.Tap("NoResourceEntry-90");

        }


        [Test]
        public void ViewPowerTrend()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            Thread.Sleep(8000);
            app.SwipeLeftToRight(0.99, 1000, true);
            app.Tap("NoResourceEntry-54");
            app.SwipeRightToLeft(0.99, 1000, true);
            app.Tap("NoResourceEntry-89");

        }


        [Test]
        public void ViewActivityPower()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            app.Tap("NoResourceEntry-68");
            app.Tap("NoResourceEntry-87");
            app.Repl();
        }

        [Test]
        public void ViewActivityCadence()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            app.Tap("NoResourceEntry-68");
            app.Tap("NoResourceEntry-88");
        }


        [Test]
        public void ViewActivitySpeed()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            app.Tap("NoResourceEntry-68");
            app.Tap("NoResourceEntry-89");
        }


        [Test]
        public void ViewRecentActivity()
        {
            Signup();
            app.ClearText("NoResourceEntry-40");
            app.ClearText("NoResourceEntry-41");
            app.EnterText("NoResourceEntry-40", "sean.mayer1");
            app.EnterText("NoResourceEntry-41", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-42");
            app.Tap("NoResourceEntry-68");
        }

        [Test]
        public void Login()
        {
            Signup();
            app.ClearText("NoResourceEntry-7");
            app.ClearText("NoResourceEntry-8");
            app.EnterText("NoResourceEntry-7", "sean.mayer1");
            app.EnterText("NoResourceEntry-8", "password");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-9");
        }


        [Test]
        public void Signup()
        {
            app.Tap("NoResourceEntry-12");

            app.ClearText(c => c.Marked("NoResourceEntry-28"));
            app.ClearText(c => c.Marked("NoResourceEntry-29"));
            app.ClearText(c => c.Marked("NoResourceEntry-30"));
            app.ClearText(c => c.Marked("NoResourceEntry-31"));
            app.ClearText(c => c.Marked("NoResourceEntry-32"));

            app.EnterText(c => c.Marked("NoResourceEntry-28"), "sean.mayer1");
            app.EnterText(c => c.Marked("NoResourceEntry-29"), "password");
            app.EnterText(c => c.Marked("NoResourceEntry-30"), "password");
            app.EnterText(c => c.Marked("NoResourceEntry-31"), "35193560");
            app.EnterText(c => c.Marked("NoResourceEntry-32"), "e8a14408cd001cb6a86607a21ff50bd42f0b76f8");
            app.DismissKeyboard();
            app.Tap("NoResourceEntry-33");
            app.Tap("button2");

        }








    }
}
