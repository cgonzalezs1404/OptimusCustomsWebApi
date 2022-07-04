using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Data
{
    public class StoredProcedures
    {
        #region Facturas
        public struct GetFacturas
        {
            public static string SpName = "optimus_customs.get_facturas";
            public static string FromDate = "_fromDate";
            public static string ToDate = "_toDate";
            public static string IdTipoFactura = "_idTipoFactura";
            public static string IdEstadoFactura = "_idEstadoFactura";
            public static string EsAprobada = "_esAprobada";
            public static string IdCP = "_idCP";
        }

        public struct GetFactura
        {
            public static string SpName = "optimus_customs.get_factura";
            public static string IdFactura = "_idFactura";
        }
        public struct InsFactura
        {
            public static string SpName = "optimus_customs.ins_factura";
            public static string IdTipoFactura = "_idTipoFactura";
            public static string IdEstadoFactura = "_idEstadoFactura";
            public static string RazonSocial = "_razonSocial";
            public static string FechaEmision = "_fechaEmision";
            public static string RFC = "_rfc";
            public static string Serie = "_serie";
            public static string Folio = "_folio";
            public static string Total = "_total";
            public static string Descripcion = "_descripcion";
            public static string UrlPDF = "_urlPdf";
            public static string UrlXML = "_urlXml";
            public static string EsAprobado = "_esAprobado";
        }
        public struct UpdFactura
        {
            public static string SpName = "optimus_customs.upd_factura";
            public static string IdFactura = "_idFactura";
            public static string IdEstadoFactura = "_idEstadoFactura";
            public static string EsAprobado = "_esAprobado";
        }
        public struct DelFactura
        {
            public static string SpName = "optimus_customs.del_factura";
            public static string IdFactura = "_idFactura";
        }
        #endregion

        #region ComplementoPago
        public struct GetComplementoPago
        {
            public static string SpName = "optimus_customs.get_complemento_pago";
            public static string IdComplementoPago = "_idComplementoPago";
        }
        public struct InsComplementoPago
        {
            public static string SpName = "optimus_customs.ins_complemento_pago";
            public static string Nombre = "nombre";
            public static string FechaEmision = "fechaEmision";
            public static string Url = "_url";
        }
        public struct UpdComplementoPago
        {
            public static string SpName = "optimus_customs.upd_complemento_pago";
            public static string IdComplementoPago = "_idComplementoPago";
            public static string Nombre = "nombre";
            public static string FechaEmision = "fechaEmision";
            public static string Url = "_url";
        }
        public struct DelComplementoPago
        {
            public static string SpName = "optimus_customs.del_complemento_pago";
            public static string IdComplementoPago = "_idComplementoPago";
        }
        #endregion

        #region ComprobantePago
        public struct GetComprobantePago
        {
            public static string SpName = "optimus_customs.get_comprobante_pago";
            public static string IdComprobantePago = "_idComprobantePago";
        }
        public struct InsComprobantePago
        {
            public static string SpName = "optimus_customs.ins_comprobante_pago";
            public static string Nombre = "nombre";
            public static string FechaEmision = "fechaEmision";
            public static string Url = "_url";
        }
        public struct UpdComprobantePago
        {
            public static string SpName = "optimus_customs.upd_comprobante_pago";
            public static string IdComprobantePago = "_idComprobantePago";
            public static string Nombre = "nombre";
            public static string FechaEmision = "fechaEmision";
            public static string Url = "_url";
        }
        public struct DelComprobantePago
        {
            public static string SpName = "optimus_customs.del_comprobante_pago";
            public static string IdComprobantePago = "_idComprobantePago";
        }
        #endregion

        #region PruebaEntrega
        public struct GetPruebaEntrega
        {
            public static string SpName = "optimus_customs.get_prueba_entrega";
            public static string IdPruebaEntrega = "_idComprobantePago";
        }
        public struct InsPruebaEntrega
        {
            public static string SpName = "optimus_customs.ins_prueba_entrega";
            public static string Nombre = "nombre";
            public static string FechaEmision = "fechaEmision";
            public static string Url = "_url";
        }
        public struct UpdPruebaEntrega
        {
            public static string SpName = "optimus_customs.upd_prueba_entrega";
            public static string IdPruebaEntrega = "_idPruebaEntrega";
            public static string Nombre = "nombre";
            public static string FechaEmision = "fechaEmision";
            public static string Url = "_url";
        }
        public struct DelPruebaEntrega
        {
            public static string SpName = "optimus_customs.del_prueba_entrega";
            public static string IdPruebaEntrega = "_idPruebaEntrega";
        }
        #endregion

        #region Usuarios
        public struct GetUsuarios
        {
            public static string SpName = "optimus_customs.get_usuarios";
            public static string IdTipoUsuario = "_idPrivilegio";
            public static string RFC = "_rfc";
        }

        public struct GetUsuario
        {
            public static string SpName = "optimus_customs.get_usuario";
            public static string IdCliente = "_idCliente";
        }

        public struct LoginUsuario
        {
            public static string SpName = "optimus_customs.login_usuario";
            public static string Username = "_mail";
            public static string Password = "_password";
        }

        public struct InsUsuario
        {
            public static string SpName = "optimus_customs.ins_usuario";
            public static string IdPrivilegio = "_idPrivilegio";
            public static string Nombre = "_nombre";
            public static string Apellido = "_apellido";
            public static string RFC = "_rfc";
            public static string RazonSocial = "_razonSocial";
            public static string Mail = "_mail";
            public static string Telefono = "_telefono";
            public static string DireccionFiscal = "_direccionFiscal";
            public static string Password = "_password";
        }

        public struct UpdUsuario
        {
            public static string SpName = "optimus_customs.upd_usuario";
            public static string IdCliente = "_idUsuario";
            public static string IdPrivilegio = "_idPrivilegio";
            public static string Nombre = "_nombre";
            public static string Apellido = "_apellido";
            public static string RFC = "_rfc";
            public static string RazonSocial = "_razonSocial";
            public static string Mail = "_mail";
            public static string Telefono = "_telefono";
            public static string DireccionFiscal = "_direccionFiscal";
            public static string Password = "_password";
        }

        public struct DelUsuario
        {
            public static string SpName = "optimus_customs.del_usuario";
            public static string IdUsuario = "_idUsuario";
        }
        #endregion

        #region Catalogos
        public struct GetPrivilegiosCatalogue
        {
            public static string SpName = "optimus_customs.get_privilegios";
        }

        public struct GetEstadoFacturaCatalogue
        {
            public static string SpName = "optimus_customs.get_catalogo_estado_factura";
        }

        public struct GetConfigFacturas
        {
            public static string SpName = "optimus_customs.get_config_facturas";
        }
        #endregion



    }
}
