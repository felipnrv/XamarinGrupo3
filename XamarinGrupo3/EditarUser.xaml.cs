﻿using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinGrupo3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarUser : ContentPage
    {
        MediaFile file;
        UsuarioDB usuarioDB = new UsuarioDB();
        public EditarUser(UsuarioModelo usuario)
        {
            InitializeComponent();
            txtNombreUser.Text = usuario.nombreuser;
            txtApellido.Text = usuario.apellidouser;
            dpFecha.Date = usuario.edaduser;
            txtGen.Text = usuario.generouser;
            txtCI.Text = usuario.cedulauser;
            txtCiudad.Text = usuario.ciudaduser;
            txtelf.Text = usuario.telefonouser;
            txtId.Text = usuario.Id;
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            UsuarioModelo usuario = new UsuarioModelo();

            string nombre = txtNombreUser.Text;
            string apellido = txtApellido.Text;
            DateTime edad = dpFecha.Date;
            string id = txtId.Text;
            string Genero = txtGen.Text;
            string Cedula = txtCI.Text;
            string Ciudad = txtCiudad.Text;
            string Telefono = txtelf.Text;


            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(Genero) || string.IsNullOrEmpty(Cedula) || string.IsNullOrEmpty(Ciudad) || string.IsNullOrEmpty(Telefono))
            {
                await DisplayAlert("Abrir", "Complete toda la Información", "Cerrar");
            }
            else
            {
                usuario.nombreuser = nombre;
                usuario.apellidouser = apellido;
                usuario.edaduser = edad;
                usuario.generouser = Genero;
                usuario.cedulauser= Cedula;
                usuario.ciudaduser= Ciudad;
                usuario.telefonouser= Telefono;
                usuario.Id = id;

                if (file != null)
                {
                    string imagen = await usuarioDB.Subir(file.GetStream(), Path.GetFileName(file.Path));
                    usuario.imagen = imagen;
                }

                bool isUpdate = await usuarioDB.Update(usuario);
                if (isUpdate)
                {
                    await DisplayAlert("Información", "Registro se actualizo correctamente", "Cerrar");
                    
                    
                }
                else
                {
                    await DisplayAlert("Error", "Fallo en la edicion no se guardo correctamente", "Cerrar");
                   
                }
               await Navigation.PushModalAsync(new DetallesUsuario());
            }
           
        }

        private async void ImgTap_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                {
                    return;
                }
                ImgTicket.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {

            }
        }
    }
  
}