using System;
using System.Runtime.InteropServices;

namespace Gadsme.Core.Native
{
	internal class IOSNativeInterface : IIOSNativeInterface
	{
		[PreserveSig]
		public static extern IntPtr GDSPluginGetUpdateTextureCallback();

		[PreserveSig]
		public static extern void GDSPluginSetMessageCallback(IntPtr nativeMessageCallback);

		[PreserveSig]
		public static extern IntPtr GDSWebTexture_new(int width, int height);

		[PreserveSig]
		public static extern void GDSWebTexture_setGadsmeBridgeJavascript(string gadsmeBridgeJavascript, string addon);

		[PreserveSig]
		public static extern void GDSWebTexture_setGadsmeSDKVersion(string sdkVersion);

		[PreserveSig]
		public static extern void GDSWebTexture_setMediaBlockingRuleEnabled(bool enabled);

		[PreserveSig]
		public static extern void GDSWebTexture_setRemoteDebugEnabled(bool enabled);

		[PreserveSig]
		public static extern void GDSWebTexture_setRemoteDebugBoolConfig(string name, bool value);

		[PreserveSig]
		public static extern void GDSWebTexture_setRemoteDebugStringConfig(string name, string value);

		[PreserveSig]
		public static extern void GDSWebTexture_setRemoteDebugDoubleConfig(string name, double value);

		[PreserveSig]
		public static extern int GDSWebTexture_getInstanceId(IntPtr webViewTexturePtr);

		[PreserveSig]
		public static extern void GDSWebTexture_setUserAgent(IntPtr webViewTexturePtr, string userAgent);

		[PreserveSig]
		public static extern void GDSWebTexture_loadUrl(IntPtr webViewTexturePtr, string url);

		[PreserveSig]
		public static extern void GDSWebTexture_loadHtml(IntPtr webViewTexturePtr, string html);

		[PreserveSig]
		public static extern void GDSWebTexture_setViewable(IntPtr webViewTexturePtr, bool viewable);

		[PreserveSig]
		public static extern void GDSWebTexture_render(IntPtr webViewTexturePtr);

		[PreserveSig]
		public static extern void GDSWebTexture_click(IntPtr webViewTexturePtr, int posX, int posY);

		[PreserveSig]
		public static extern void GDSWebTexture_setSkAdNetworkData(IntPtr webViewTexturePtr, string skAdNetworkDataString);

		[PreserveSig]
		public static extern void GDSWebTexture_destroy(IntPtr webViewTexturePtr);

		[PreserveSig]
		public static extern void GDSAdPresenter_presentURL(string url, string adNetworkData);

		[PreserveSig]
		public static extern int GDSAdvertisingIdentifier_request(bool allowConsentDialog);

		[PreserveSig]
		public static extern IntPtr GDSVendorIdentifier_getIdentifier();

		[PreserveSig]
		public static extern IntPtr GDSSKAdNetwork_getAdNetworkIds();

		[PreserveSig]
		public static extern int GDSUserAgent_request();

		[PreserveSig]
		public static extern IntPtr GDSTCF_getConsentString();

		[PreserveSig]
		public static extern float GDSAudio_requestAudioVolume();

		public IntPtr PluginGetUpdateTextureCallback()
		{
			return (IntPtr)0;
		}

		public void PluginSetMessageCallback(IntPtr nativeMessageCallback)
		{
		}

		public IntPtr WebTexture_new(int width, int height)
		{
			return (IntPtr)0;
		}

		public void WebTexture_setGadsmeBridgeJavascript(string gadsmeBridgeJavascript, string addon)
		{
		}

		public void WebTexture_setGadsmeSDKVersion(string sdkVersion)
		{
		}

		public void WebTexture_setMediaBlockingRuleEnabled(bool enabled)
		{
		}

		public void WebTexture_setRemoteDebugEnabled(bool enabled)
		{
		}

		public void WebTexture_setRemoteDebugBoolConfig(string name, bool value)
		{
		}

		public void WebTexture_setRemoteDebugStringConfig(string name, string value)
		{
		}

		public void WebTexture_setRemoteDebugDoubleConfig(string name, double value)
		{
		}

		public int WebTexture_getInstanceId(IntPtr webViewTexturePtr)
		{
			return 0;
		}

		public void WebTexture_setUserAgent(IntPtr webViewTexturePtr, string userAgent)
		{
		}

		public void WebTexture_loadUrl(IntPtr webViewTexturePtr, string url)
		{
		}

		public void WebTexture_loadHtml(IntPtr webViewTexturePtr, string html)
		{
		}

		public void WebTexture_setViewable(IntPtr webViewTexturePtr, bool viewable)
		{
		}

		public void WebTexture_render(IntPtr webViewTexturePtr)
		{
		}

		public void WebTexture_click(IntPtr webViewTexturePtr, int posX, int posY)
		{
		}

		public void WebTexture_setSkAdNetworkData(IntPtr webViewTexturePtr, string skAdNetworkDataString)
		{
		}

		public void WebTexture_destroy(IntPtr webViewTexturePtr)
		{
		}

		public void AdPresenter_presentURL(string url, string adNetworkData)
		{
		}

		public int AdvertisingIdentifier_request(bool allowConsentDialog)
		{
			return 0;
		}

		public IntPtr VendorIdentifier_getIdentifier()
		{
			return (IntPtr)0;
		}

		public IntPtr SKAdNetwork_getAdNetworkIds()
		{
			return (IntPtr)0;
		}

		public int UserAgent_request()
		{
			return 0;
		}

		public IntPtr TCF_getConsentString()
		{
			return (IntPtr)0;
		}

		public float Audio_requestAudioVolume()
		{
			return 0f;
		}
	}
}
