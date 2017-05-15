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
using Android.Webkit;
using Android.Graphics;

using TravelersGuide.HelperClasses;
using TravelersGuide.Core.Model;
using TravelersGuide.Adapters;

using TravelersGuide.Activities;

namespace TravelersGuide.Fragments
{
    public class ImageDetailFragment : Fragment
    {
        private View view;
        private int[] mDatasetTypes = { 0, 1, 2 }; //view types
        private List<CustomImageDetail> imageDetail;
        private ProgressBar progressBar;

        public Android.Support.V7.Widget.GridLayoutManager mLayoutManager;
        public static readonly EndpointAddress EndPoint = new EndpointAddress("http://128.189.166.224:9001/TravelersGuideWCFService");
        public ImageView ImageDetailImageView;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }


        private void DetectText()
        {
            try
            {
                var filePath = imageDetail[0].ImagePath + "/" + imageDetail[0].ImageName;
                var ImageBitmap = ImageHelper.GetImageBitmapFromFilePath(filePath, imageDetail[0].ImageWidth, imageDetail[0].ImageHeight);

                byte[] bitmapData;
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


                var TravelersGuideCloudService = new TravelersGuideClient(binding, EndPoint);
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
                    Activity.RunOnUiThread(() => progressBar.Visibility = ViewStates.Gone);
                    return;
                }

                StringBuilder detectedText = new StringBuilder();
                TextView detectedTextView = View.FindViewById<TextView>(Resource.Id.ImageDetail_DetectedView);

                if (e.Result == null || e.Result.Length == 0)
                {
                    //Handle error case
                    //No Text detected
                    detectedText.Append("No Text Detected");
                    //Set Font color red
                    Activity.RunOnUiThread(() => Toast.MakeText(Context, "No Text Detected", ToastLength.Long).Show());
                }
                else
                {
                    foreach (string resultText in e.Result)
                    {
                        detectedText.Append(resultText).Append(" ");
                    }
                    Activity.RunOnUiThread(() => Toast.MakeText(Context, "Text Detected Successfully", ToastLength.Long).Show());
                }
                Activity.RunOnUiThread(() => detectedTextView.Text = detectedText.ToString());
                GlobalVariables.db.AddDetectedTextToImage(imageDetail[0].CapturedImagesID, detectedText.ToString());
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }

            imageDetail = GlobalVariables.db.GetImageDetail(Activity.Intent.GetIntExtra("ImageID", 1));
            Activity.RunOnUiThread(() => progressBar.Visibility = ViewStates.Gone);
        }


        void OnItemClick(object sender, int position)
        {
            if (position == mDatasetTypes[0])
            {
                //Open Image in Gallery
                var filePath = "file://" + imageDetail[0].ImagePath + "/" + imageDetail[0].ImageName;
                var mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(MimeTypeMap.GetFileExtensionFromUrl(filePath));
                Android.Net.Uri imageURL = Android.Net.Uri.Parse(filePath);

                Intent intent = new Intent();
                intent.SetAction(Intent.ActionView);
                //intent.SetDataAndType(imageURL, "image/*");
                intent.SetDataAndType(imageURL, mimeType);

                StartActivity(intent);
            }
            else if (position == mDatasetTypes[1])
            {
                
                if (imageDetail[0].TextDetected == 1)
                {
                    //Open TextDetailView
                    var intent = new Intent(Activity, typeof(TextDetailActivity));
                    intent.PutExtra("TextToTranslateID", imageDetail[0].TextToTransID);
                    StartActivity(intent);
                }
                else
                {
                    //Detect Text in Image 
                    progressBar = view.FindViewById<ProgressBar>(Resource.Id.ImageDetail_DetectedSpinner);
                    progressBar.Visibility = ViewStates.Visible;

                    var detectedView = view.FindViewById<TextView>(Resource.Id.ImageDetail_DetectedView);
                    detectedView.Text = "";
                    DetectText();
                }
            }
            else
            {
                //Caption Click
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.ImageDetailView, container, false);

            imageDetail = GlobalVariables.db.GetImageDetail(Activity.Intent.GetIntExtra("ImageID", 1));
            ((ImageDetailActivity)Activity).setImageDetail(imageDetail);
            
            var adapter = new ImageDetailAdapter(imageDetail, mDatasetTypes);
            adapter.ItemClick += OnItemClick;

            var mRecyclerView = view.FindViewById<Android.Support.V7.Widget.RecyclerView>(Resource.Id.ImageDetailRecyclerView);

            mRecyclerView.SetAdapter(adapter);

            mLayoutManager = new Android.Support.V7.Widget.GridLayoutManager(Context, 1, Android.Support.V7.Widget.GridLayoutManager.Vertical, false);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            return view;
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