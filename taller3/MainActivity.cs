using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Threading.Tasks;
using System.IO;

namespace taller3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private string _FileName = "content.txt";
        private string _Path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EditText txtFirst = FindViewById<EditText>(Resource.Id.txtFirsttxt);
            EditText txtSecond = FindViewById<EditText>(Resource.Id.txtSecondtxt);
            TextView lblResult = FindViewById<TextView>(Resource.Id.lblResult);

            Button btnConcatenar = FindViewById<Button>(Resource.Id.btnConcatenar);

            btnConcatenar.Click += async (sender, e) =>
            {
                try
                {
                    string text = txtFirst.Text.Trim();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        return;
                    }

                    string body = await ReadFile();
                    await WriteFile($"{body} {text}");
                    lblResult.Text = $"{body} {txtSecond.Text}";
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async Task WriteFile(string text)
        {
            string fullPath = Path.Combine(_Path, _FileName);
            using (var writer = File.CreateText(fullPath))
            {
                await writer.WriteLineAsync(text);
            }
        }

        private async Task<string> ReadFile()
        {
            try
            {
                string fullPath = Path.Combine(_Path, _FileName);
                if (File.Exists(fullPath))
                {
                    using (var reader = new StreamReader(fullPath, true))
                    {
                        string textoLeido;
                        while ((textoLeido = await reader.ReadLineAsync()) != null)
                        {
                            return textoLeido;
                        }
                    }
                }
                return string.Empty;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}