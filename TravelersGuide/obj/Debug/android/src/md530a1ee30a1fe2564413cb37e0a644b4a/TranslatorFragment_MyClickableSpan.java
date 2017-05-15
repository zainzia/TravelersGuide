package md530a1ee30a1fe2564413cb37e0a644b4a;


public class TranslatorFragment_MyClickableSpan
	extends android.text.style.ClickableSpan
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler\n" +
			"";
		mono.android.Runtime.register ("TravelersGuide.Fragments.TranslatorFragment+MyClickableSpan, TravelersGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TranslatorFragment_MyClickableSpan.class, __md_methods);
	}


	public TranslatorFragment_MyClickableSpan () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TranslatorFragment_MyClickableSpan.class)
			mono.android.TypeManager.Activate ("TravelersGuide.Fragments.TranslatorFragment+MyClickableSpan, TravelersGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public TranslatorFragment_MyClickableSpan (java.lang.String p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == TranslatorFragment_MyClickableSpan.class)
			mono.android.TypeManager.Activate ("TravelersGuide.Fragments.TranslatorFragment+MyClickableSpan, TravelersGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public void onClick (android.view.View p0)
	{
		n_onClick (p0);
	}

	private native void n_onClick (android.view.View p0);

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
