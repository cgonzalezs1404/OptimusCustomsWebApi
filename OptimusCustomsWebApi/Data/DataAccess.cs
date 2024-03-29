﻿using Api.Configuration;
using MySql.Data.MySqlClient;
using OptimusCustomsWebApi.Enum;
using OptimusCustomsWebApi.Jobs;
using OptimusCustomsWebApi.Model;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;

namespace OptimusCustomsWebApi.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DataAccess
    {
        /// <summary>
        /// 
        /// </summary>
        private static DataAccess instance;
        /// <summary>
        /// 
        /// </summary>
        private static object objectLock = new object();
        /// <summary>
        /// 
        /// </summary>
        private MySqlConnection connection;

        protected static int FLAG_GREEN = 0;
        protected static int FLAG_YELLOW = 0;
        protected static int FLAG_RED = 0;

        /// <summary>
        /// 
        /// </summary>
        public static DataAccess Instance
        {
            get
            {
                lock (objectLock)
                {
                    if (instance == null)
                    {
                        instance = new DataAccess();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void TestDataConnection()
        {
            try
            {
                connection = new MySqlConnection(ApiConfiguration.Instance.ConnectionString);
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine("Database Connection Created: {0}", ApiConfiguration.Instance.ConnectionString);
                }

                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();

                scheduler.Start();

                IJobDetail job = JobBuilder.Create<FacturaJob>()
                .WithIdentity("job1", "group1")
                .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1").WithDailyTimeIntervalSchedule(s =>
                    s.OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(13, 32))
                    .EndingDailyAfterCount(1))
                    .Build();

                // Tell quartz to schedule the job using our trigger
                scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
            }
            catch (Exception ex)
            {

            }


        }

        #region Facturas
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<Factura> GetFacturas(DateTime fromDate, DateTime toDate, int idTipoFactura, int idEstdoFactura, int idUsuario)
        {
            List<Factura> result = new List<Factura>();
            using (var command = new MySqlCommand(StoredProcedures.GetFacturas.SpName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(StoredProcedures.GetFacturas.FromDate, fromDate);
                command.Parameters.AddWithValue(StoredProcedures.GetFacturas.ToDate, toDate);
                command.Parameters.AddWithValue(StoredProcedures.GetFacturas.IdEstadoFactura, idEstdoFactura == 0 ? null : idEstdoFactura);
                command.Parameters.AddWithValue(StoredProcedures.GetFacturas.IdTipoFactura, idTipoFactura == 0 ? null : idTipoFactura);
                command.Parameters.AddWithValue(StoredProcedures.GetFacturas.IdUsuario, idUsuario == 0 ? null : idUsuario);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = new Factura();
                        model.IdFactura = reader.GetInt32("idFactura");
                        model.IdOperacion = reader["idOperacion"] is DBNull ? 0 : reader.GetInt32("idOperacion");
                        model.IdTipoFactura = reader.GetInt32("idTipoFactura");
                        model.TipoFactura = reader.GetString("tipoFactura");
                        model.IdEstadoFactura = reader.GetInt32("idEstadoFactura");
                        model.EstadoFactura = reader.GetString("estadoFactura");
                        model.RazonSocial = reader.GetString("razonSocial");
                        model.FechaEmision = reader.GetDateTime("fechaEmision");
                        model.RFC = reader.GetString("rfc");
                        model.Serie = reader.GetString("serie");
                        model.Folio = reader.GetString("folio");
                        model.Total = reader.GetDouble("total");
                        model.Descripcion = reader.GetString("descripcion");
                        model.Comentarios = reader.GetString("comentarios");
                        model.EsAprobado = reader.GetBoolean("esAprobado");
                        model.EsPagada = reader.GetBoolean("esPagada");
                        result.Add(model);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Factura GetFactura(int idFactura)
        {
            Factura model = null;
            using (var command = new MySqlCommand(StoredProcedures.GetFactura.SpName, connection))
            {
                command.Parameters.AddWithValue(StoredProcedures.GetFactura.IdFactura, idFactura);
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model = new Factura();
                        model.IdFactura = reader.GetInt32("idFactura");
                        model.IdOperacion = reader["idOperacion"] is DBNull ? 0 : reader.GetInt32("idOperacion");
                        model.IdTipoFactura = reader.GetInt32("idTipoFactura");
                        model.TipoFactura = reader.GetString("tipoFactura");
                        model.IdEstadoFactura = reader.GetInt32("idEstadoFactura");
                        model.EstadoFactura = reader.GetString("estadoFactura");
                        model.RazonSocial = reader.GetString("razonSocial");
                        model.FechaEmision = reader.GetDateTime("fechaEmision");
                        model.RFC = reader.GetString("rfc");
                        model.Serie = reader.GetString("serie");
                        model.Folio = reader.GetString("folio");
                        model.Total = reader.GetDouble("total");
                        model.Descripcion = reader.GetString("descripcion");
                        model.EsAprobado = reader.GetBoolean("esAprobado");
                        model.EsPagada = reader.GetBoolean("esPagada");
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Factura InsertFactura(Factura model)
        {
            Factura result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.InsFactura.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.IdEstadoFactura, model.IdEstadoFactura);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.IdTipoFactura, model.IdTipoFactura);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.RazonSocial, model.RazonSocial);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.FechaEmision, model.FechaEmision);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.RFC, model.RFC);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.Serie, model.Serie);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.Folio, model.Folio);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.Total, model.Total);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.Descripcion, model.Descripcion);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.UrlPDF, model.FilePdf);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.UrlXML, model.FileXml);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.EsAprobado, model.EsAprobado);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.EsPagada, model.EsPagada);
                    command.Parameters.AddWithValue(StoredProcedures.InsFactura.Comentarios, model.Comentarios);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new Factura();
                            result.IdFactura = reader.GetInt32("idFactura");
                            result.IdTipoFactura = reader.GetInt32("idTipoFactura");
                            result.TipoFactura = reader.GetString("tipoFactura");
                            result.IdEstadoFactura = reader.GetInt32("idEstadoFactura");
                            result.EstadoFactura = reader.GetString("estadoFactura");
                            result.RazonSocial = reader.GetString("razonSocial");
                            result.FechaEmision = reader.GetDateTime("fechaEmision");
                            result.RFC = reader.GetString("rfc");
                            result.Serie = reader.GetString("serie");
                            result.Folio = reader.GetString("folio");
                            result.Total = reader.GetDouble("total");
                            result.Descripcion = reader.GetString("descripcion");
                            result.EsAprobado = reader.GetBoolean("esAprobado");
                            result.Comentarios = reader.GetString("comentarios");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Factura UpdateFactura(Factura model)
        {
            Factura result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.UpdFactura.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.UpdFactura.IdFactura, model.IdFactura);
                    command.Parameters.AddWithValue(StoredProcedures.UpdFactura.IdEstadoFactura, model.IdEstadoFactura);
                    command.Parameters.AddWithValue(StoredProcedures.UpdFactura.EsAprobado, model.EsAprobado);
                    command.ExecuteNonQuery();

                    result = model;

                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void DeleteFactura(int idFactura)
        {
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.DelFactura.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.DelFactura.IdFactura, idFactura);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Stream GetFacturaPDF(int idFactura)
        {
            Stream result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.GetFacturaPDF.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.GetFacturaPDF.IdFactura, idFactura);
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetStream("filePdf");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion

        #region Usuarios
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Usuario> GetUsuarios(int? idTipoUsuario, string? RFC)
        {
            List<Usuario> result = new List<Usuario>();
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.GetUsuarios.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.GetUsuarios.IdTipoUsuario, idTipoUsuario);
                    command.Parameters.AddWithValue(StoredProcedures.GetUsuarios.RFC, RFC);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = new Usuario
                            {
                                IdUsuario = reader.GetInt32("idUsuario"),
                                Nombre = reader.GetString("nombre"),
                                Apellido = reader.GetString("apellido"),
                                RFC = reader.GetString("rfc"),
                                RazonSocial = reader.GetString("razonSocial"),
                                Mail = reader.GetString("mail"),
                                Telefono = reader.GetString("telefono"),
                                DireccionFiscal = reader.GetString("direccionFiscal"),
                                IdTipoUsuario = reader.GetInt32("idTipoUsuario"),
                                TipoUsuario = reader.GetString("tipoUsuario")
                            };
                            result.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public Usuario GetUsuario(int idUsuario)
        {
            Usuario result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.GetUsuario.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.GetUsuario.IdCliente, idUsuario);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new Usuario
                            {
                                IdUsuario = reader.GetInt32("idUsuario"),
                                Nombre = reader.GetString("nombre"),
                                Apellido = reader.GetString("apellido"),
                                RFC = reader.GetString("rfc"),
                                RazonSocial = reader.GetString("razonSocial"),
                                Mail = reader.GetString("mail"),
                                Telefono = reader.GetString("telefono"),
                                DireccionFiscal = reader.GetString("direccionFiscal"),
                                IdTipoUsuario = reader.GetInt32("idTipoUsuario"),
                                TipoUsuario = reader.GetString("tipoUsuario")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public Usuario GetUsuario(string username, string password)
        {
            Usuario result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.LoginUsuario.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.LoginUsuario.Username, username);
                    command.Parameters.AddWithValue(StoredProcedures.LoginUsuario.Password, password);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new Usuario
                            {
                                IdUsuario = reader.GetInt32("idUsuario"),
                                Nombre = reader.GetString("nombre"),
                                Apellido = reader.GetString("apellido"),
                                RFC = reader.GetString("rfc"),
                                RazonSocial = reader.GetString("razonSocial"),
                                Mail = reader.GetString("mail"),
                                Telefono = reader.GetString("telefono"),
                                DireccionFiscal = reader.GetString("direccionFiscal"),
                                IdTipoUsuario = reader.GetInt32("idTipoUsuario"),
                                TipoUsuario = reader.GetString("tipoUsuario")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Usuario UpdateCliente(Usuario model)
        {
            Usuario result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.UpdUsuario.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.IdCliente, model.IdUsuario);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.IdPrivilegio, model.IdTipoUsuario);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.Nombre, model.Nombre);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.Apellido, model.Apellido);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.RFC, model.RFC);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.RazonSocial, model.RazonSocial);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.Mail, model.Mail);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.Telefono, model.Telefono);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.DireccionFiscal, model.DireccionFiscal);

                    command.ExecuteNonQuery();

                    result = model;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Usuario InsertUsuario(Usuario model)
        {
            Usuario result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.InsUsuario.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.InsUsuario.Nombre, model.Nombre);
                    command.Parameters.AddWithValue(StoredProcedures.InsUsuario.Apellido, model.Apellido);
                    command.Parameters.AddWithValue(StoredProcedures.InsUsuario.RFC, model.RFC);
                    command.Parameters.AddWithValue(StoredProcedures.InsUsuario.RazonSocial, model.RazonSocial);
                    command.Parameters.AddWithValue(StoredProcedures.InsUsuario.Mail, model.Mail);
                    command.Parameters.AddWithValue(StoredProcedures.InsUsuario.Telefono, model.Telefono);
                    command.Parameters.AddWithValue(StoredProcedures.InsUsuario.DireccionFiscal, model.DireccionFiscal);
                    command.Parameters.AddWithValue(StoredProcedures.InsUsuario.Password, model.Password);

                    command.ExecuteNonQuery();

                    result = model;

                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCliente"></param>
        public void DeleteCliente(int idCliente)
        {
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.DelUsuario.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.DelUsuario.IdUsuario, idCliente);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Operaciones
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<Operacion> GetOperaciones(DateTime fromDate, DateTime toDate, int idTipoOperacion, int idUsuario)
        {
            List<Operacion> result = new List<Operacion>();
            using (var command = new MySqlCommand(StoredProcedures.GetOperaciones.SpName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(StoredProcedures.GetOperaciones.FromDate, fromDate);
                command.Parameters.AddWithValue(StoredProcedures.GetOperaciones.ToDate, toDate);
                command.Parameters.AddWithValue(StoredProcedures.GetOperaciones.IdTipoOperacion, idTipoOperacion == 0 ? null : idTipoOperacion);
                command.Parameters.AddWithValue(StoredProcedures.GetOperaciones.IdUsuario, idUsuario == 0 ? null : idUsuario);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = new Operacion
                        {
                            IdOperacion = reader.GetInt32("idOperacion"),
                            IdTipoOperacion = reader.GetInt32("idTipoOperacion"),
                            TipoOperacion = reader.GetString("tipoOperacion"),
                            IdUsuario = reader["idUsuario"] is DBNull ? null : reader.GetInt32("idUsuario"),
                            RazonSocial = reader.GetString("razonSocial"),
                            IdFactura = reader["idFactura"] is DBNull ? null : reader.GetInt32("idFactura"),
                            ExisteFactura = reader["factura"] is DBNull ? false : reader.GetBoolean("factura"),
                            ExistePruebaEntrega = reader["pruebaEntrega"] is DBNull ? false : reader.GetBoolean("pruebaEntrega"),
                            ExisteComplementoPago = reader["complementoPago"] is DBNull ? false : reader.GetBoolean("complementoPago"),
                            ExisteComprobantePago = reader["comprobantePago"] is DBNull ? false : reader.GetBoolean("comprobantePago"),
                            NumOperacion = reader["numeroOp"] is DBNull ? "" : reader.GetString("numeroOp"),
                            FechaInicio = reader["fechaInicio"] is DBNull ? null : reader.GetDateTime("fechaInicio"),
                            FechaFin = reader["fechaFin"] is DBNull ? null : reader.GetDateTime("fechaFin")
                        };
                        result.Add(model);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public Operacion GetOperacion(int Id)
        {
            Operacion result = null;
            using (var command = new MySqlCommand(StoredProcedures.GetOperacion.SpName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(StoredProcedures.GetOperacion.IdOperacion, Id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new Operacion
                        {
                            IdOperacion = reader.GetInt32("idOperacion"),
                            IdTipoOperacion = reader.GetInt32("idTipoOperacion"),
                            TipoOperacion = reader.GetString("tipoOperacion"),
                            IdUsuario = reader["idUsuario"] is DBNull ? null : reader.GetInt32("idUsuario"),
                            RazonSocial = reader.GetString("razonSocial"),
                            IdFactura = reader["idFactura"] is DBNull ? null : reader.GetInt32("idFactura"),
                            ExistePruebaEntrega = reader["pruebaEntrega"] is DBNull ? false : reader.GetBoolean("pruebaEntrega"),
                            ExisteComplementoPago = reader["complementoPago"] is DBNull ? false : reader.GetBoolean("complementoPago"),
                            ExisteComprobantePago = reader["comprobantePago"] is DBNull ? false : reader.GetBoolean("comprobantePago"),
                            NumOperacion = reader["numeroOp"] is DBNull ? "" : reader.GetString("numeroOp"),
                            FechaInicio = reader["fechaInicio"] is DBNull ? null : reader.GetDateTime("fechaInicio"),
                            FechaFin = reader["fechaFin"] is DBNull ? null : reader.GetDateTime("fechaFin")
                        };
                    }
                }
            }
            return result;
        }

        public bool InsOperacion(Operacion model)
        {
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.InsOperacion.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.InsOperacion.IdTipoOperacion, model.IdTipoOperacion);
                    command.Parameters.AddWithValue(StoredProcedures.InsOperacion.IdUsuario, model.IdUsuario);
                    command.Parameters.AddWithValue(StoredProcedures.InsOperacion.NumeroOp, model.NumOperacion);
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public Operacion ValidateOperacion(string numOperacion)
        {
            Operacion result = null;
            using (var command = new MySqlCommand(StoredProcedures.ValidateOperacion.SpName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(StoredProcedures.ValidateOperacion.NumeroOp, numOperacion);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new Operacion
                        {
                            IdOperacion = reader.GetInt32("idOperacion"),
                            IdTipoOperacion = reader.GetInt32("idTipoOperacion"),
                            TipoOperacion = reader.GetString("tipoOperacion"),
                            IdUsuario = reader["idUsuario"] is DBNull ? null : reader.GetInt32("idUsuario"),
                            RazonSocial = reader.GetString("razonSocial"),
                            IdFactura = reader["idFactura"] is DBNull ? null : reader.GetInt32("idFactura"),
                            ExistePruebaEntrega = reader["pruebaEntrega"] is DBNull ? false : reader.GetBoolean("pruebaEntrega"),
                            ExisteComplementoPago = reader["complementoPago"] is DBNull ? false : reader.GetBoolean("complementoPago"),
                            ExisteComprobantePago = reader["comprobantePago"] is DBNull ? false : reader.GetBoolean("comprobantePago"),
                            NumOperacion = reader["numeroOp"] is DBNull ? "" : reader.GetString("numeroOp"),
                            FechaInicio = reader["fechaInicio"] is DBNull ? null : reader.GetDateTime("fechaInicio"),
                            FechaFin = reader["fechaFin"] is DBNull ? null : reader.GetDateTime("fechaFin")
                        };
                    }
                }
            }
            return result;
        }

        public bool UpdOperacion(Operacion model)
        {
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.UpdOperacion.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.UpdOperacion.IdOperacion, model.IdOperacion);
                    command.Parameters.AddWithValue(StoredProcedures.UpdOperacion.IdTipoOperacion, model.IdTipoOperacion);
                    command.Parameters.AddWithValue(StoredProcedures.UpdOperacion.IdUsuario, model.IdUsuario);
                    command.Parameters.AddWithValue(StoredProcedures.UpdOperacion.NumeroOp, model.NumOperacion);
                    command.Parameters.AddWithValue(StoredProcedures.UpdOperacion.IdFactura, model.IdFactura);
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool InsDocumento(Documento model)
        {
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.InsDetalleOperacion.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.InsDetalleOperacion.IdOperacion, model.IdOperacion);
                    command.Parameters.AddWithValue(StoredProcedures.InsDetalleOperacion.IdTipoDocumento, model.IdTipoDocumento);
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool UpdDocumento(Documento model)
        {
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.UpdDetalleOperacion.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.UpdDetalleOperacion.IdOperacion, model.IdOperacion);
                    command.Parameters.AddWithValue(StoredProcedures.UpdDetalleOperacion.IdTipoDocumento, model.IdTipoDocumento);
                    command.Parameters.AddWithValue(StoredProcedures.UpdDetalleOperacion.SourceFile, model.SourceFile);
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public Stream GetDocumento(int idOperacion, int idTipoDocumento)
        {
            Stream result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.GetDocumento.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.GetDocumento.IdOperacion, idOperacion);
                    command.Parameters.AddWithValue(StoredProcedures.GetDocumento.IdTipoDocumento, idTipoDocumento);
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetStream("sourceFile");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idOperacion"></param>
        public void DeleteOperacion(int idOperacion)
        {
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.DelOperacion.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(StoredProcedures.DelOperacion.IdOperacion, idOperacion);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Catalogos
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Catalogo> GetCatalogo(TipoCatalogo tipoCatalogo)
        {
            List<Catalogo> result = new List<Catalogo>();
            try
            {
                string sp = "";
                switch (tipoCatalogo)
                {
                    case TipoCatalogo.Privilegio:
                        sp = StoredProcedures.GetPrivilegiosCatalogue.SpName;
                        break;
                    case TipoCatalogo.EstadoFactura:
                        sp = StoredProcedures.GetEstadoFacturaCatalogue.SpName;
                        break;
                    case TipoCatalogo.TipoFactura:
                        sp = StoredProcedures.GetTipoFacturaCatalogue.SpName;
                        break;
                    case TipoCatalogo.TipoOperacion:
                        sp = StoredProcedures.GetTipoOperacionCatalogue.SpName;
                        break;
                    case TipoCatalogo.Usuarios:
                        sp = StoredProcedures.GetUsuariosCatalogue.SpName;
                        break;
                    case TipoCatalogo.TipoDocumento:
                        sp = StoredProcedures.GetTipoDocumentoCatalogue.SpName;
                        break;
                    default:
                        sp = "";
                        break;

                }
                using (var command = new MySqlCommand(sp, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        result.Add(new Catalogo { Id = 0, Codigo = "Seleccionar...", Nombre = "Seleccionar..." });
                        while (reader.Read())
                        {
                            var model = new Catalogo
                            {
                                Id = reader.GetInt32("Id"),
                                Codigo = reader.GetString("Codigo"),
                                Nombre = reader.GetString("Nombre"),

                            };
                            result.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetConfigFacturas()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.GetConfigFacturas.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString("nombre"),
                                       reader.GetString("valor"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        public void ExecuteJob()
        {
            var facturas = DataAccess.Instance.GetFacturas(new DateTime(DateTime.Now.Year, 4, 1), new DateTime(DateTime.Now.Year, 4, 30), 0, 0, 0);
            var estadosFactura = DataAccess.Instance.GetCatalogo(TipoCatalogo.EstadoFactura);
            var config = DataAccess.Instance.GetConfigFacturas();

            FLAG_GREEN = Convert.ToInt32(config.GetValueOrDefault("CONFIG_FACT_NORM"));
            FLAG_YELLOW = Convert.ToInt32(config.GetValueOrDefault("CONFIG_FACT_WARN"));
            FLAG_RED = Convert.ToInt32(config.GetValueOrDefault("CONFIG_FACT_DANG"));

            foreach (var factura in facturas)
            {
                var maxDay = factura.FechaEmision.AddDays(15);
                var dias = (maxDay - new DateTime(2022, 7, 17)).TotalDays;

                if (dias > FLAG_GREEN)
                {
                    factura.IdEstadoFactura = estadosFactura.Where(T => T.Nombre == "Normal").FirstOrDefault().Id;
                }
                else if (dias <= FLAG_GREEN && dias > FLAG_YELLOW)
                {
                    factura.IdEstadoFactura = estadosFactura.Where(T => T.Nombre == "Normal").FirstOrDefault().Id;
                }
                else if (dias <= FLAG_YELLOW && dias > FLAG_RED)
                {
                    factura.IdEstadoFactura = estadosFactura.Where(T => T.Nombre == "Por vencer").FirstOrDefault().Id;
                }
                else
                {
                    factura.IdEstadoFactura = estadosFactura.Where(T => T.Nombre == "Vencido").FirstOrDefault().Id;
                }

                DataAccess.Instance.UpdateFactura(factura);

            }
        }
    }
}
