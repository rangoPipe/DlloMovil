using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

namespace WebService
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

            Button btnConsultar = FindViewById<Button>(Resource.Id.btnConsultar);
            Button btnGuardar = FindViewById<Button>(Resource.Id.btnGuardar);
            Button btnEliminar = FindViewById<Button>(Resource.Id.btnEliminar);
            Button btnParchar = FindViewById<Button>(Resource.Id.btnParchar);

            EditText txtId = FindViewById<EditText>(Resource.Id.txtId);
            EditText txtTitulo = FindViewById<EditText>(Resource.Id.txtTitulo);
            EditText txtContent = FindViewById<EditText>(Resource.Id.txtContent);

            string urlServicio = "https://jsonplaceholder.typicode.com/posts";

            btnConsultar.Click += async (sender, e) =>
            {
                try
                {
                    Cliente client = new Cliente();
                    if (!string.IsNullOrWhiteSpace(txtId.Text))
                    {
                        int id = 0;
                        if (int.TryParse(txtId.Text, out id))
                        {
                            Entrada result = await client.Get<Entrada>($"{urlServicio}/{id}");
                            if (client.CodigoHttp == System.Net.HttpStatusCode.OK)
                            {
                                txtTitulo.Text = result.Title;
                                txtContent.Text = result.Body;
                                Toast.MakeText(this, "Consulta realizada con exito", ToastLength.Long).Show();
                            }
                            else
                            {
                                throw new Exception(client.CodigoHttp.ToString());
                            }

                        }
                        else
                        {
                            Toast.MakeText(this, "Por favor ingresar un valor númerico para el ID", ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Por favor ingresar un ID", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Long).Show();
                }
            };

            btnGuardar.Click += async (sender, e) =>
            {
                try
                {
                    Cliente client = new Cliente();

                    if (!string.IsNullOrWhiteSpace(txtTitulo.Text) && !string.IsNullOrWhiteSpace(txtContent.Text))
                    {
                        Entrada item = new Entrada(txtTitulo.Text, txtContent.Text);
                        Entrada result = await client.Post<Entrada>(item, urlServicio);
                        if (client.CodigoHttp == System.Net.HttpStatusCode.Created)
                        {
                            txtId.Text = result.Id.ToString();
                            Toast.MakeText(this, $"Se guardo exitosamente", ToastLength.Long).Show();
                        }
                        else
                        {
                            throw new Exception(client.CodigoHttp.ToString());
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, $"Llenar los campos contenido y/o titulo", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Long).Show();
                }
            };

            btnEliminar.Click += async (sender, e) =>
            {
                try
                {
                    Cliente client = new Cliente();

                    if (!string.IsNullOrWhiteSpace(txtId.Text))
                    {
                        await client.Delete($"{urlServicio}/{txtId.Text}");
                        if (client.CodigoHttp == System.Net.HttpStatusCode.OK)
                        {
                            Toast.MakeText(this, $"Se elimino correctamente", ToastLength.Long).Show();
                        }
                        else
                        {
                            throw new Exception(client.CodigoHttp.ToString());
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, $"El campo ID no puede estar limpio", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Long).Show();
                }
            };

            btnParchar.Click += async (sender, e) =>
            {
                try
                {
                    Cliente client = new Cliente();

                    if (!string.IsNullOrWhiteSpace(txtId.Text))
                    {
                        Content item = new Content(txtTitulo.Text, txtContent.Text);
                        Entrada result = await client.Patch<Entrada>(item, $"{urlServicio}/{txtId.Text}");
                        if (client.CodigoHttp == System.Net.HttpStatusCode.OK)
                        {
                            txtTitulo.Text = result.Title.ToString();
                            txtContent.Text = result.Body;
                            Toast.MakeText(this, $"Se parcho exitosamente", ToastLength.Long).Show();
                        }
                        else
                        {
                            throw new Exception(client.CodigoHttp.ToString());
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, $"El campo ID no puede estar limpio", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Long).Show();
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