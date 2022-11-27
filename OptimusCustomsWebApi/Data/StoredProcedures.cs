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
            public static string EsPagada = "_esPagada";
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
            public static string EsPagada = "_esPagada";
            public static string Comentarios = "_comentarios";
        }
        public struct UpdFactura
        {
            public static string SpName = "optimus_customs.upd_factura";
            public static string IdFactura = "_idFactura";
            public static string IdEstadoFactura = "_idEstadoFactura";
            public static string EsAprobado = "_esAprobado";
            public static string EsPagada = "_esPagada";
            public static string Comentarios = "_comentarios";
        }
        public struct DelFactura
        {
            public static string SpName = "optimus_customs.del_factura";
            public static string IdFactura = "_idFactura";
        }

        public struct GetFacturaPDF
        {
            public static string SpName = "optimus_customs.get_factura_pdf";
            public static string IdFactura = "_idFactura";
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
            public static string IdCliente = "_idUsuario";
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

        public struct GetTipoOperacionCatalogue
        {
            public static string SpName = "optimus_customs.get_catalogo_tipo_operacion";
        }

        public struct GetUsuariosCatalogue
        {
            public static string SpName = "optimus_customs.get_catalogo_usuario";
        }

        public struct GetTipoDocumentoCatalogue
        {
            public static string SpName = "optimus_customs.get_catalogo_tipo_documento";
        }
        #endregion

        #region Operaciones
        public struct GetOperaciones
        {
            public static string SpName = "optimus_customs.get_operaciones";
            public static string FromDate = "_fromDate";
            public static string ToDate = "_toDate";
        }

        public struct GetOperacion
        {
            public static string SpName = "optimus_customs.get_operacion";
            public static string IdOperacion = "_idOperacion";
        }

        public struct InsOperacion
        {
            public static string SpName = "optimus_customs.ins_operacion";
            public static string NumeroOp = "_numeroOp";
            public static string IdTipoOperacion = "_idTipoOperacion";
            public static string IdUsuario = "_idUsuario";
        }

        public struct ValidateOperacion
        {
            public static string SpName = "optimus_customs.validate_operacion";
            public static string NumeroOp = "_numeroOp";
        }

        public struct UpdOperacion
        {
            public static string SpName = "optimus_customs.upd_operacion";
            public static string IdOperacion = "_idOperacion";
            public static string IdFactura = "_idFactura";
            public static string IdTipoOperacion = "_idTipoOperacion";
            public static string IdUsuario = "_idUsuario";
            public static string NumeroOp = "_numeroOp";
            public static string FechaFin = "_fechaFin";
        }

        public struct InsDetalleOperacion
        {
            public static string SpName = "optimus_customs.ins_operacion";
            public static string IdOperacion = "_idOperacion";
            public static string IdTipoDocumento = "_idTipoDoc";
        }

        public struct UpdDetalleOperacion
        {
            public static string SpName = "optimus_customs.upd_documento";
            public static string IdOperacion = "_idOperacion";
            public static string IdTipoDocumento = "_idTipoDoc";
            public static string SourceFile = "_sourceFile";
        }

        public struct GetDocumento
        {
            public static string SpName = "optimus_customs.get_documento";
            public static string IdOperacion = "_idOperacion";
            public static string IdTipoDocumento = "_idTipoDoc";
        }

        public struct DelOperacion
        {
            public static string SpName = "optimus_customs.del_operacion";
            public static string IdOperacion = "_idOperacion";
        }
        #endregion



    }
}
