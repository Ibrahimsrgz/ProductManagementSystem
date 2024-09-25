﻿using Android.App;
using Android.Content;
using Android.Content.PM;

namespace ProductManagementSystem.Maui.Platforms.Android;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = CALLBACK_SCHEME)]
public class ProductManagementSystemWebAuthenticatorCallbackActivity : WebAuthenticatorCallbackActivity
{
   public const string CALLBACK_SCHEME = "productmanagementsystem";
}