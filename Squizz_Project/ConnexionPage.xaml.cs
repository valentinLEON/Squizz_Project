﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ServiceModel;
using Squizz_Project.SquizzWebService;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Squizz_Project
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    
    public sealed partial class ConnexionPage : Page
    {
        DataManagementClient client = new DataManagementClient();
        public ConnexionPage()
        {
            this.InitializeComponent();
           // ApplicationView.PreferredLaunchViewSize = new Size(360, 640);
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(400, 650));
        }

        private async void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            //TODO: connexion to the DB via Web service if it exists
            String msg = "";
            if (txtUsernameConnexion.Text == "" || txtUserNamePassword.Password == "")
            {
                msg = "Username or password missing";
            }
            else
            {
                msg = client.ConnectionCheckPlayerAsync(txtUsernameConnexion.Text, txtUserNamePassword.Password).Result;
            }

            //this.Frame.Navigate(typeof(GameTypeSelectionPage)); 
            //btnConnexion.Content = msg; // POUR DEBUG SANS POPUP


            var dialog = new Windows.UI.Popups.MessageDialog(
                msg);

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                // Adding a 3rd command will crash the app when running on Mobile !!!
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Maybe later") { Id = 2 });
            }

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            // PENSEZ A VOIR LA FERMETURE DE LA CONNECTION SQL 

        }


        private async void forgotPassword_Click(object sender, TappedRoutedEventArgs e)
        {
            // Changement d'interface pour que l'utilisateur renseigne son adresse mail
            // Page : ForgotPassword
            //this.Frame.Navigate(typeof(ForgotPassword), null);
            String msg = "";
            msg = await client.ForgotPasswordAsync("teamsquizz@outlook.fr", 2);

            btnForgotPwd.Content = msg;

        }

        private void Connexion_Click(object sender, TappedRoutedEventArgs e)
        {
            // Redirection vers la page principale du jeu 

            // Faire un check en BDD pour savoir si l'utilisateur existe

            // Faire une check pour savoir si l'utilisateur et mot de passe correspondent



        }

        private void NewAccount_Click(object sender, TappedRoutedEventArgs e)
        {
            // Redirection vers la page de création d'utilisateur 
            this.Frame.Navigate(typeof(SignInPage), null);
        }
    }
    
}