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

namespace WebService
{
    public class Content
    {
        public string Title { get; set; }
        public string Body { get; set; }

        public Content(string title = "", string body = "")
        {
            Title = title;
            Body = body;
        }
    }

    public class Entrada: Content
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        


        public Entrada(string title = "", string body = "")
        {
            UserId = 1;
            Id = 0;
            Title = title;
            Body = body;
        }
    }
}