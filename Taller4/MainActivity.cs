using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Logic.Manager;
using Logic.Model;

namespace Taller3
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

            EditText txtId = FindViewById<EditText>(Resource.Id.txtId);

            Button btnSearch = FindViewById<Button>(Resource.Id.btnSearch);
            Button btnInsert = FindViewById<Button>(Resource.Id.btnInsert);

            btnSearch.Click += (sender, args) =>
            {
                try
                {
                    ConnectionClass Manager = new ConnectionClass();
                    PersonClass person = Manager.GetById(int.Parse(txtId.Text));
                    Toast.MakeText(this, (person != null)? $"Se encontro con ID {person.Id.ToString()}": "Dicho ID no existe", ToastLength.Long).Show();
                    
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            };

            btnInsert.Click += (sender, args) =>
            {
                try
                {
                    ConnectionClass Manager = new ConnectionClass();
                    PersonClass person = new PersonClass() { Id = int.Parse(txtId.Text) };
                    Manager.SaveChanges(person);
                    Toast.MakeText(this, "Se almaceno correctamente", ToastLength.Long).Show();
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
    }
}