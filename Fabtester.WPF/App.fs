namespace Fabtester.WPF

open System

open System.Windows
open Xamarin.Forms
open Xamarin.Forms.Platform.WPF

type MainWindow() = 
    inherit FormsApplicationPage()

module Main = 
    [<EntryPoint>]
    [<STAThread>]
    let main(_args) =

        let app = new System.Windows.Application()
        Forms.Init()
        let window = MainWindow()
        window.Width <- 1000.
        window.Height <- 1000.
        window.WindowStartupLocation <- WindowStartupLocation.CenterScreen
        window.LoadApplication(new Fabtester.App())

        app.Run(window)
