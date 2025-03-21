﻿using DavxeShop.Library.Services.Interfaces;
using DavxeShop.Models;
using DavxeShop.Persistance;
using DavxeShop.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DavxeShop.Library.Services
{
    public class TrenService : ITrenService
    {
        private readonly ITrenDboHelper _trenDboHelper;

        public TrenService(ITrenDboHelper trenDboHelper)
        {
            _trenDboHelper = trenDboHelper;
        }

        public List<ViajeBasico> GetRecomendedTrains()
        {
            var random = new Random();
            var hoy = DateTime.Today.AddDays(1);

            var allTrains = _trenDboHelper.GetAllTrenes()
                .Where(v =>
                {
                    var fechas = v.fecha.Split('/');
                    if (fechas.Length == 2 &&
                        DateTime.TryParse(fechas[0], out DateTime fechaInicio) &&
                        DateTime.TryParse(fechas[1], out DateTime fechaFin))
                    {
                        return fechaInicio >= hoy && fechaFin >= hoy;
                    }
                    return false;
                })
                .OrderBy(x => random.Next())
                .Take(10)
                .Select(v => new ViajeBasico
                {
                    id_viaje = v.id_viaje,
                    origen = v.Ruta.origen,
                    destino = v.Ruta.destino,
                    duracion = v.duracion,
                    fecha = v.fecha,
                    precio = v.Tarifas != null && v.Tarifas.Any() ? v.Tarifas.FirstOrDefault()?.precio ?? 0m : 0m
                })
                .ToList();

            return allTrains;
        }

        public List<ViajeSimplificado> GetAllTrains()
        {
            var allTrains = _trenDboHelper.GetAllTrenes2().ToList();

            return allTrains;
        }

        public UpdateTren GetTrainById(int id)
        {
            var getTrainById = _trenDboHelper.GetTrainById(id);

            return getTrainById;
        }

        public bool DeleteTren(string username, int id_viaje)
        {
            var userInDb = _trenDboHelper.GetUserDb(username);
            if (userInDb == null)
            {
                return false;
            }

            var tren = _trenDboHelper.GetTrenById(id_viaje);
            if (tren == null)
            {
                return false;
            }

            if (userInDb.Rol != 1)
            {
                return false;
            }

            try
            {
                _trenDboHelper.DeleteTren(id_viaje);
                _trenDboHelper.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTren(int id, UpdateTren trenInfo)
        {
            return _trenDboHelper.UpdateTren(id, trenInfo);
        }
    }
}
