



App Center Xamarin UI Tests:
Prerequites 
Install: NodeJS -> https://nodejs.org/en/
Install: CMD (Administrator) -> npm install -g appcenter-cli
Run Command in:
{USERPATH}.nuget\packages\xamarin.uitest\2.2.7\tools>
Appcenter test run uitest Command: 
appcenter test run uitest--app "{email-address}/Smart-Cycling-Performance-Application" 
--devices "{email-address}/all-devices" 
--app-path "{USERPATH}\fyp.xamarin.mobile\FYP.Xamarin.Mobile\FYP.Xamarin.Mobile.Android\bin\Release\com.companyname.FYP.Xamarin.Mobile.Android.apk"  
--test-series "all-devices" --locale "en_US" 
--build-dir "{USERPATH}\fyp.xamarin.mobile\FYP.Xamarin.Tests\bin\Release" --uitest-tools-dir "{USERPATH}.nuget\packages\xamarin.uitest\2.2.7\tools"

https://appcenter.ms/users/{email-address}/apps/Smart-Cycling-Performance-Application/test/runs/f63ff9c5-b9ba-4062-a161-db4630a09f7b
