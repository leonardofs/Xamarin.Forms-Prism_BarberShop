using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Prism.Unity;
using Microsoft.Practices.Unity;
using Syncfusion.ListView.XForms.UWP;

namespace PrismBarbearia.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            SfListViewRenderer.Init();
            Xamarin.FormsMaps.Init("PMUCtHlcSNeLpp9FnbuT~1Ne08600EU_QjbTyPlOALQ~AjtycRCT3IZ16WZ79G9z16Wd7E8CCZeVyMWYrNMya7II2sPsBUhR7s8879BKV0OR");
            LoadApplication(new PrismBarbearia.App(new UwpInitializer()));            
        }
    }

    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }

}
