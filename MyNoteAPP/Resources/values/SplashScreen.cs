using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using MyNoteAPP;

namespace MyNoteApp.Resources.values
{
    [Activity(Label = "SplashScreen",MainLauncher=true, Theme="@style/Theme.Splash", NoHistory=true, Icon="@drawable/icon")]

    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Display Splash Screen 
            Thread.Sleep(100);
            //Start Activity1 Activity 
             
            StartActivity(typeof(MainActivity));
        }
    }
}