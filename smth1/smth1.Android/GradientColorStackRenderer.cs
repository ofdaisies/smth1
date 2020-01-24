using smth1.Droid;
using smth1.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Java.Lang;

[assembly: ExportRenderer(typeof(GradientColorStack), typeof(GradientColorStackRenderer))]  
namespace smth1.Droid {  
    [System.Obsolete]
public class GradientColorStackRenderer : VisualElementRenderer<StackLayout>
{
    private Color StartColor
    {
        get;
        set;
    }
    private Color EndColor
    {
        get;
        set;
    }
    protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
    {
        var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Width,
        this.StartColor.ToAndroid(),
        this.EndColor.ToAndroid(),
        Android.Graphics.Shader.TileMode.Clamp);
        var paint = new Android.Graphics.Paint()
        {
            Dither = true,
        };
        paint.SetShader(gradient);
        canvas.DrawPaint(paint);
        base.DispatchDraw(canvas);
    }
    protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
    {
        base.OnElementChanged(e);
            var stack = e.NewElement as GradientColorStack;
            this.StartColor = stack.StartColor;
            this.EndColor = stack.EndColor;
       
    }
}  
}  