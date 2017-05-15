package md59f4f62c97b5d184d1ab4b9a6ef99c8e3;


public class ImageDetailViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("TravelersGuide.ViewHolders.ImageDetailViewHolder, TravelersGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ImageDetailViewHolder.class, __md_methods);
	}


	public ImageDetailViewHolder (android.view.View p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == ImageDetailViewHolder.class)
			mono.android.TypeManager.Activate ("TravelersGuide.ViewHolders.ImageDetailViewHolder, TravelersGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
