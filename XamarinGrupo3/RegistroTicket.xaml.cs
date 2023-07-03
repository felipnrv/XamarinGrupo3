﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinGrupo3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroTicket : ContentPage
    {
        TicketsDB Guardtickets = new TicketsDB();
        public RegistroTicket()
        {
            InitializeComponent();
        }

        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            TicketModelo ticket = new TicketModelo();

            string nombre=txtNombreTicket.Text;
            string direccion = txtDireccion.Text;
            string detalletck = txtdetck.Text;
            DateTime fechatck = dpFecha.Date;
            string actrlz = txtAct.Text;
            string detallesol = txtDetSol.Text;
            string marca = txtMarca.Text;
            string modelo = txtModelo.Text;
            string serie = txtSerie.Text;

            if(string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(detalletck) || fechatck==DateTime.MinValue ||fechatck==null || string.IsNullOrEmpty(actrlz) || string.IsNullOrEmpty(detallesol) || string.IsNullOrEmpty(marca) || string.IsNullOrEmpty(modelo) || string.IsNullOrEmpty(serie))
            {
                await DisplayAlert("Abrir", "Complete toda la Información", "Cerrar");
            }
            else
            {
                ticket.NombreTick = nombre;
                ticket.direcciontick= direccion;
                ticket.detalletick=detalletck;
                ticket.marca = marca;
                ticket.modelo = modelo;
                ticket.serie = serie;
                ticket.detsolucion = detallesol;
                ticket.fecha = fechatck;
                ticket.actsolucion = actrlz;

                var guardar = await Guardtickets.GuardarTicket(ticket);
                if (guardar)
                {
                    await DisplayAlert("Información", "Registro guardado correctamente", "Cerrar");
                }
                else
                {
                    await DisplayAlert("Error", "El registro no se guardo correctamente", "Cerrar");

                }
            }

        }
    }
}