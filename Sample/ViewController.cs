using System;
using Foundation;
using UIKit;
using Xamarin.LFLiveKit;

namespace Sample
{
    public partial class ViewController : UIViewController,ILFLiveSessionDelegate
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        LFLiveSession session;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            session = new LFLiveSession(LFLiveAudioConfiguration.DefaultConfiguration(), LFLiveVideoConfiguration.DefaultConfiguration());
            session.PreView = this.View;
            session.Delegate = this;
            var stream = new LFLiveStreamInfo();
            stream.Url = "http://test";
            session.StartLive(stream);
            session.SaveLocalVideo = true;
            //session.SaveLocalVideoPath=new NSUrl()
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        [Export("liveSession:liveStateDidChange:")]
        public void LiveStateDidChange(LFLiveSession session, LFLiveState state)
        {
            System.Diagnostics.Debug.WriteLine(state.ToString());
        }

        [Export("liveSession:debugInfo:")]
        public void DebugInfo(LFLiveSession session, LFLiveDebug debugInfo)
        {
            System.Diagnostics.Debug.WriteLine(debugInfo.DebugDescription);
        }

        [Export("liveSession:errorCode:")]
        public void ErrorCode(LFLiveSession session, LFLiveSocketErrorCode errorCode)
        {
            System.Diagnostics.Debug.WriteLine(errorCode.ToString());
        }
    }
}
