using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using TravelersGuide;
using TravelersGuide.HelperClasses;
using TravelersGuide.Adapters;
using TravelersGuide.Activities;
using TravelersGuide.Core.Service;
using TravelersGuide.Core.Model;

namespace TravelersGuide.Fragments
{
    public class TakePictureImageViewFragment : BaseFragment
    {
        private View view;
        private ImageView imageView;
        private Java.IO.File ImageDirectory;
        private Java.IO.File Image;
        private TravelersGuideClient TravelersGuideCloudService;
        private byte[] bitmapData;

        public static readonly EndpointAddress EndPoint = new EndpointAddress("http://128.189.166.224:9001/TravelersGuideWCFService");
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            view = inflater.Inflate(Resource.Layout.TakePictureImageView, container, false);

            imageView = view.FindViewById<ImageView>(Resource.Id.TakePictureImageView);
            imageView.Click += ImageView_Click;

            var ImageBitmap = ((TakePictureActivity)Activity).getImageBitmap();
            if (ImageBitmap != null)
            {
                imageView.SetImageBitmap(ImageBitmap);
            }

            return view;
        }

        private void ImageView_Click(object sender, EventArgs e)
        {
            try
            {
                var ImageBitmap = ((TakePictureActivity)Activity).getImageBitmap();

                using (var stream = new MemoryStream())
                {
                    ImageBitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
                    bitmapData = stream.ToArray();
                }


                BasicHttpBinding binding = CreateBasicHttp();
                binding.Name = "basicHttpBinding";
                binding.MaxBufferSize = int.MaxValue;
                binding.MaxReceivedMessageSize = int.MaxValue;
                binding.ReceiveTimeout = TimeSpan.FromMinutes(10.0);
                binding.SendTimeout = TimeSpan.FromMinutes(10.0);
                binding.CloseTimeout = TimeSpan.FromMinutes(5.0);
                binding.OpenTimeout = TimeSpan.FromMinutes(5.0);


                TravelersGuideCloudService = new TravelersGuideClient(binding, EndPoint);
                TravelersGuideCloudService.InnerChannel.OperationTimeout = TimeSpan.FromMinutes(10);
                TravelersGuideCloudService.Open();

                TravelersGuideCloudService.GetImageTextCompleted += TravelersGuideCloudService_GetImageTextCompleted;
                TravelersGuideCloudService.GetImageTextAsync(bitmapData);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        private void TravelersGuideCloudService_GetImageTextCompleted(object sender, GetImageTextCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    //Handle Error case here
                    //Problem connecting to Server OR Server threw an error
                    Logger.Log(e.Error.Data.ToString());
                    return;
                }

                StringBuilder detectedText = new StringBuilder();
                EditText detectedTextView = View.FindViewById<EditText>(Resource.Id.TextDetectedTextView);

                if (e.Result == null || e.Result.Length == 0)
                {
                    //Handle error case
                    //No Text detected
                    detectedText.Append("No Results found");
                    //Set FOnt color red
                    
                }
                else
                {
                    foreach (string resultText in e.Result)
                    {
                        detectedText.Append(resultText).Append(" ");
                    }
                }
                Activity.RunOnUiThread(() => detectedTextView.Text = detectedText.ToString());
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        private static BasicHttpBinding CreateBasicHttp()
        {
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Name = "basicHttpBinding",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };
            TimeSpan timeout = new TimeSpan(0, 0, 30);
            binding.SendTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.ReceiveTimeout = timeout;
            return binding;
        }

    }
}