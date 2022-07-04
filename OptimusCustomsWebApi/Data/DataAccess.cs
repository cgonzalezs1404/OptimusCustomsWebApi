using Api.Configuration;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MySql.Data.MySqlClient;
using OptimusCustomsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
            }
            catch (Exception ex)
            {

            }


        }

        #region Facturas
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idFactura"></param>
        /// <returns></returns>
        public List<Factura> GetFacturas(DateTime fromDate, DateTime toDate)
        {
            List<Factura> result = new List<Factura>();
            using (var command = new MySqlCommand(StoredProcedures.GetFacturas.SpName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(StoredProcedures.GetFacturas.FromDate, fromDate);
                command.Parameters.AddWithValue(StoredProcedures.GetFacturas.ToDate, toDate);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = new Factura();
                        model.IdFactura = reader.GetInt32("idFactura");
                        model.IdTipoFactura = reader.GetInt32("idTipoFactura");
                        model.TipoFactura = reader.GetString("estadoFactura");
                        model.IdEstadoFactura = reader.GetInt32("idEstadoFactura");
                        model.EstadoFactura = reader.GetString("estadoFactura");
                        model.RazonSocial = reader.GetString("razonSocial");
                        model.FechaEmision = reader.GetDateTime("fechaEmision");
                        model.RFC = reader.GetString("rfc");
                        model.Serie = reader.GetString("serie");
                        model.Folio = reader.GetString("folio");
                        model.Total = reader.GetDouble("total");
                        model.Descripcion = reader.GetString("descripcion");
                        //model.UrlPdf = reader.GetString("urlPdf");
                        //model.UrlXml = reader.GetString("urlXml");
                        model.EsAprobado = reader.GetBoolean("esAprobado");
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
            Factura result = null;
            using (var command = new MySqlCommand(StoredProcedures.GetFactura.SpName, connection))
            {
                command.Parameters.AddWithValue(StoredProcedures.GetFactura.IdFactura, idFactura);
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new Factura();
                        result.IdFactura = reader.GetInt32("idFactura");
                        result.IdTipoFactura = reader.GetInt32("idTipoFactura");
                        result.TipoFactura = reader.GetString("estadoFactura");
                        result.IdEstadoFactura = reader.GetInt32("idEstadoFactura");
                        result.EstadoFactura = reader.GetString("estadoFactura");
                        result.RazonSocial = reader.GetString("razonSocial");
                        result.FechaEmision = reader.GetDateTime("fechaEmision");
                        result.RFC = reader.GetString("rfc");
                        result.Serie = reader.GetString("serie");
                        result.Folio = reader.GetString("folio");
                        result.Total = reader.GetDouble("total");
                        result.Descripcion = reader.GetString("descripcion");
                        //result.FilePdf = reader.GetBytes("urlPdf");
                        //result.FileXml = reader.GetString("urlXml");
                        result.EsAprobado = reader.GetBoolean("esAprobado");
                    }
                }
            }
            return result;
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
        public Factura UpdateFactura(Factura model)
        {
            Factura result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.UpdFactura.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.UpdFactura.IdFactura, model.IdFactura);
                    command.Parameters.AddWithValue(StoredProcedures.UpdFactura.Nombre, model.RazonSocial);
                    command.Parameters.AddWithValue(StoredProcedures.UpdFactura.FechaEmision, model.FechaEmision);
                    command.Parameters.AddWithValue(StoredProcedures.UpdFactura.Url, model.FileXml);
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
                    command.Parameters.AddWithValue(StoredProcedures.DelFactura.IdFactura, idFactura);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region ComplementoPago
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ComplementoPago GetComplementoPago(int idComplementoPago)
        {
            ComplementoPago result = null;
            using (var command = new MySqlCommand(StoredProcedures.GetComplementoPago.SpName, connection))
            {
                command.Parameters.AddWithValue(StoredProcedures.GetComplementoPago.IdComplementoPago, idComplementoPago);
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new ComplementoPago();
                        result.IdComplementoPago = reader.GetInt32("idComplementoPago");
                        result.Nombre = reader.GetString("nombre");
                        result.FechaEmision = reader.GetDateTime("fechaEmision");
                        result.Url = reader.GetString("url");
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ComplementoPago InsertComplementoPago(ComplementoPago model)
        {
            ComplementoPago result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.InsComplementoPago.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.InsComplementoPago.Nombre, model.Nombre);
                    command.Parameters.AddWithValue(StoredProcedures.InsComplementoPago.FechaEmision, model.FechaEmision);
                    command.Parameters.AddWithValue(StoredProcedures.InsComplementoPago.Url, model.Url);
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
        public ComplementoPago UpdateComplementoPago(ComplementoPago model)
        {
            ComplementoPago result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.UpdComplementoPago.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.UpdComplementoPago.IdComplementoPago, model.IdComplementoPago);
                    command.Parameters.AddWithValue(StoredProcedures.UpdComplementoPago.Nombre, model.Nombre);
                    command.Parameters.AddWithValue(StoredProcedures.UpdComplementoPago.FechaEmision, model.FechaEmision);
                    command.Parameters.AddWithValue(StoredProcedures.UpdComplementoPago.Url, model.Url);
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
        public void DeleteComplementoPago(int idComplementoPago)
        {
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.DelComplementoPago.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.DelComplementoPago.IdComplementoPago, idComplementoPago);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region ComprobantePago
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ComprobantePago GetComprobantePago(int idComprobantePago)
        {
            ComprobantePago result = null;
            using (var command = new MySqlCommand(StoredProcedures.GetComprobantePago.SpName, connection))
            {
                command.Parameters.AddWithValue(StoredProcedures.GetComprobantePago.IdComprobantePago, idComprobantePago);
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new ComprobantePago();
                        result.IdComprobantePago = reader.GetInt32("idComprobantePago");
                        result.Nombre = reader.GetString("nombre");
                        result.FechaEmision = reader.GetDateTime("fechaEmision");
                        result.Url = reader.GetString("url");
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ComprobantePago InsertComprobantePago(ComprobantePago model)
        {
            ComprobantePago result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.InsComprobantePago.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.InsComprobantePago.Nombre, model.Nombre);
                    command.Parameters.AddWithValue(StoredProcedures.InsComprobantePago.FechaEmision, model.FechaEmision);
                    command.Parameters.AddWithValue(StoredProcedures.InsComprobantePago.Url, model.Url);
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
        public ComprobantePago UpdateComprobantePago(ComprobantePago model)
        {
            ComprobantePago result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.UpdComprobantePago.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.UpdComprobantePago.IdComprobantePago, model.IdComprobantePago);
                    command.Parameters.AddWithValue(StoredProcedures.UpdComprobantePago.Nombre, model.Nombre);
                    command.Parameters.AddWithValue(StoredProcedures.UpdComprobantePago.FechaEmision, model.FechaEmision);
                    command.Parameters.AddWithValue(StoredProcedures.UpdComprobantePago.Url, model.Url);
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
        public void DeleteComprobantePago(int idComprobantePago)
        {
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.DelComprobantePago.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.DelComprobantePago.IdComprobantePago, idComprobantePago);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region PruebaEntrega
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PruebaEntrega GetPruebaEntrega(int idPruebaEntrega)
        {
            PruebaEntrega result = null;
            using (var command = new MySqlCommand(StoredProcedures.GetPruebaEntrega.SpName, connection))
            {
                command.Parameters.AddWithValue(StoredProcedures.GetPruebaEntrega.IdPruebaEntrega, idPruebaEntrega);
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new PruebaEntrega();
                        result.IdPruebaEntrega = reader.GetInt32("idPruebaEntrega");
                        result.Nombre = reader.GetString("nombre");
                        result.FechaEmision = reader.GetDateTime("fechaEmision");
                        result.Url = reader.GetString("url");
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PruebaEntrega InsertPruebaEntrega(PruebaEntrega model)
        {
            PruebaEntrega result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.InsPruebaEntrega.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.InsPruebaEntrega.Nombre, model.Nombre);
                    command.Parameters.AddWithValue(StoredProcedures.InsPruebaEntrega.FechaEmision, model.FechaEmision);
                    command.Parameters.AddWithValue(StoredProcedures.InsPruebaEntrega.Url, model.Url);
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
        public PruebaEntrega UpdatePruebaEntrega(PruebaEntrega model)
        {
            PruebaEntrega result = null;
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.UpdPruebaEntrega.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.UpdPruebaEntrega.IdPruebaEntrega, model.IdPruebaEntrega);
                    command.Parameters.AddWithValue(StoredProcedures.UpdPruebaEntrega.Nombre, model.Nombre);
                    command.Parameters.AddWithValue(StoredProcedures.UpdPruebaEntrega.FechaEmision, model.FechaEmision);
                    command.Parameters.AddWithValue(StoredProcedures.UpdPruebaEntrega.Url, model.Url);
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
        public void DeletePruebaEntrega(int idPruebaEntrega)
        {
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.DelPruebaEntrega.SpName, connection))
                {
                    command.Parameters.AddWithValue(StoredProcedures.DelPruebaEntrega.IdPruebaEntrega, idPruebaEntrega);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Clientes
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
                                IdPrivilegio = reader.GetInt32("idPrivilegio"),
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
                                IdPrivilegio = reader.GetInt32("idPrivilegio"),
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
                                IdPrivilegio = reader.GetInt32("idPrivilegio"),
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
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.IdPrivilegio, model.IdPrivilegio);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.Nombre, model.Nombre);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.Apellido, model.Apellido);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.RFC, model.RFC);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.RazonSocial, model.RazonSocial);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.Mail, model.Mail);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.Telefono, model.Telefono);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.DireccionFiscal, model.DireccionFiscal);
                    command.Parameters.AddWithValue(StoredProcedures.UpdUsuario.Password, model.Password);

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
                    command.Parameters.AddWithValue(StoredProcedures.InsUsuario.IdPrivilegio, model.IdPrivilegio);
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

        #region Catalogos
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Privilegio> GetPrivilegios()
        {
            List<Privilegio> result = new List<Privilegio>();
            try
            {
                using (var command = new MySqlCommand(StoredProcedures.GetPrivilegiosCatalogue.SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        result.Add(new Privilegio { Id = 0, Codigo = "Seleccionar...", Nombre = "Seleccionar..." });
                        while (reader.Read())
                        {
                            var model = new Privilegio
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
    }
}
