using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Preferences;
using Android.Telephony;

namespace KulawyDewastator
{
    [Activity(Label = "KulawyDewastator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private EditTextPreference phoneTextPreference = new EditTextPreference(Application.Context);

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Button callButton = FindViewById<Button>(Resource.Id.CallButton);
            Button smsButton = FindViewById<Button>(Resource.Id.SendSms);
            Button mailButton = FindViewById<Button>(Resource.Id.SendEmail);
            EditText phoneNumber = FindViewById<EditText>(Resource.Id.PhoneNumber);
            EditText messageText = FindViewById<EditText>(Resource.Id.MessageText);
            EditText smsCount = FindViewById<EditText>(Resource.Id.SmsCount);
            EditText messageTopic = FindViewById<EditText>(Resource.Id.MessegeTopic);
            EditText toWhoMail = FindViewById<EditText>(Resource.Id.ToWhoMail);

            //var cos = Resources.GetString(Resource.String.PhoneNumber);
            //cos = "hgjds";
            //phoneTextPreference.Text = "609941530";
            phoneNumber.Text = phoneTextPreference.Text;

            callButton.Click += (object sender, EventArgs e) =>
            {
                var callIntent = new Intent(Intent.ActionCall);
                callIntent.SetData(Android.Net.Uri.Parse("tel:" + phoneNumber.Text));
                StartActivity(callIntent);
            };

            smsButton.Click += (object sender, EventArgs e) =>
            {
                for (int i = 0; i < int.Parse(smsCount.Text); i++)
                {
                    /*var smsUri = Android.Net.Uri.Parse("smsto:"+phoneNumber.Text);
                    var smsIntent = new Intent(Intent.ActionSendto, smsUri);
                    smsIntent.PutExtra(messageText.Text,1);
                    StartActivity(smsIntent);*/
                    SmsManager.Default.SendTextMessage(phoneNumber.Text, null, messageText.Text, null, null);
                }
            };

            mailButton.Click += (object sender, EventArgs e) =>
            {
                var email = new Intent(Intent.ActionSend);
                email.PutExtra(Intent.ExtraEmail, new string[] { toWhoMail.Text });

                email.PutExtra(Intent.ExtraSubject, messageTopic.Text);

                email.PutExtra(Intent.ExtraText, messageText.Text);

                email.SetType("message/rfc822");

                StartActivity(email);
            };
        }

        protected override void OnStop()
        {
            EditText phoneNumber = FindViewById<EditText>(Resource.Id.PhoneNumber);
            phoneTextPreference.Text = phoneNumber.Text;
        }
    }
}

