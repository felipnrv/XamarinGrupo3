﻿using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinGrupo3
{
    public class TicketsDB
    {
        FirebaseStorage firebaseStorage = new FirebaseStorage("fir-xamarin-213c2.appspot.com");
        FirebaseClient firebaseClient = new FirebaseClient("https://fir-xamarin-213c2-default-rtdb.firebaseio.com/");

        public async Task<bool> GuardarTicket(TicketModelo ticket)
        {
            var info = await firebaseClient.Child(nameof(TicketModelo)).PostAsync(JsonConvert.SerializeObject(ticket));
            if(!string.IsNullOrEmpty(info.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<TicketModelo>>GetAll()
        {
            return (await firebaseClient.Child(nameof(TicketModelo)).OnceAsync<TicketModelo>()).Select(item => new TicketModelo
            {
                Id=item.Key,
                NombreTick=item.Object.NombreTick,
                direcciontick=item.Object.direcciontick,
                detalletick=item.Object.detalletick,
               
                marca=item.Object.marca,
                modelo=item.Object.modelo,
                serie=item.Object.serie,
                estado=item.Object.estado,
                fecha=item.Object.fecha,

            
            }).ToList();

        }
        public async Task<TicketModelo> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(TicketModelo) + "/"+id).OnceSingleAsync<TicketModelo>());

        }
        public async Task<bool> Update(TicketModelo ticket)
        {
            await firebaseClient.Child(nameof(TicketModelo) + "/" + ticket.Id).PutAsync(JsonConvert.SerializeObject(ticket));
            return true;
        }
        public async Task<bool> Borrar(string id)
        {
            await firebaseClient.Child(nameof(TicketModelo) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<string> Subir(Stream img, string filenomb)
        {
            var image = await firebaseStorage.Child("Imagen").Child(filenomb).PutAsync(img);
            return image;
        }

    }
}
