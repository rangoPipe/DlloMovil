using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace taller2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EditText txtValor = FindViewById<EditText>(Resource.Id.txtValor);
            TextView lblResult = FindViewById<TextView>(Resource.Id.lblResultado);
            Button btnCalcular = FindViewById<Button>(Resource.Id.btnCalcular);


            btnCalcular.Click += (sender, args) =>
            {
                try
                {
                    lblResult.Text = string.Empty;
                    lblResult.Text = CalculateFactorial(txtValor.Text);
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
            };

            txtValor.KeyPress += (sender, e) => {
                try
                {
                    e.Handled = false;
                    if (e.KeyCode == Android.Views.Keycode.Enter)
                    {
                        lblResult.Text = string.Empty;
                        lblResult.Text = CalculateFactorial(txtValor.Text);
                        e.Handled = true;
                    }
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }

            }; 

            /*txtValor.KeyPress += (keycode, e) =>
            {
                try
                {
                    if (e.KeyCode == Android.Views.Keycode.Enter && e.Event.Action == Android.Views.KeyEventActions.Down)
                    {
                        lblResult.Text = string.Empty;
                        lblResult.Text = CalculateFactorial(txtValor.Text);
                    }
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
        };*/

        }

        private void BtnCalcular_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void TxtValor_KeyPress(object sender, Android.Views.View.KeyEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private string CalculateFactorial(string toCalculate)
        {
            int valor;
            if (int.TryParse(toCalculate, out valor))
            {
                int fact = 1;
                for (int i = 1; i <= valor; i++)
                    fact = fact * i;

                return $"El factorial de {valor} es: {fact}";

            }
            else
            {
                throw new System.Exception( "Por favor ingresar un valor númerico");
            }
        }
    }
}