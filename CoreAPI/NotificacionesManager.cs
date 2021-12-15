using Aspose.Pdf;
using Aspose.Pdf.Text;
using Entities_POJO;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CoreAPI
{
    public class NotificacionesManager
    {
        private static readonly string _dataDir = "..\\..\\..\\..\\..\\Samples\\";


        //Inicializar el objeto
        private static Document document = new Document();

        //Add page
        private static Page page = document.Pages.Add();

        public NotificacionesManager()
        {

        }

        public string generarModeloCorreo(Usuarios user, string titulo, string mensaje)
        {
            enviarCorreos(user, titulo, mensaje).Wait();
            return "éxito";
        }

        public static async Task enviarCorreos(Usuarios user, string titulo, string mensaje)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("rentnridecom@gmail.com", "RentNRide.com");
            var subject = titulo;
            var to = new EmailAddress(user.Correo, "Usuario");
            var plainTextContent = mensaje;
            var htmlContent = mensaje;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public string generarModeloSMS(Usuarios user, string mensaje)
        {
            enviarMensajes(user, mensaje);
            return "éxito";
        }

        private void enviarMensajes(Usuarios user, string mensaje)
        {
            string accountSid = "AC8cb56422437c955f6487c195eecccaa6";
            string authToken = "b402568cb8a6047c8725bb097c792f22";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: mensaje,
                from: new Twilio.Types.PhoneNumber("+12059531826"),
                to: new Twilio.Types.PhoneNumber("+506" + user.Telefono)
            );

            Console.WriteLine(message.Sid);
        }

        public string recuperarClaveCorreo(Usuarios user)
        {
            EnviarCorreoContrasenna(user).Wait();
            return "éxito";
        }

        public static async Task EnviarCorreoContrasenna(Usuarios user)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("rentnridecom@gmail.com", "RentNRide.com");
            var subject = "Resultado operación";
            var to = new EmailAddress(user.Correo, "Usuario");
            var plainTextContent = "El código OTP es el siguiente: ";
            var htmlContent = Convert.ToString(user.OTP);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public string recuperarClaveSMS(Usuarios user)
        {
            EnviarSMSContrasenna(user);
            return "éxito";
        }
        public string recuperarClaveSMS2(Usuarios user)
        {
            EnviarSMSContrasenna2(user);
            return "éxito";
        }

        public static void EnviarSMSContrasenna(Usuarios user)
        {

            string accountSid = "AC8cb56422437c955f6487c195eecccaa6";
            string authToken = "b402568cb8a6047c8725bb097c792f22";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "El código OTP es el siguiente: " + user.OTP,
                from: new Twilio.Types.PhoneNumber("+12059531826"),
                to: new Twilio.Types.PhoneNumber("+506" + user.Telefono)
            );

            Console.WriteLine(message.Sid);
        }

        public static void EnviarSMSContrasenna2(Usuarios user)
        {

            string accountSid = "AC8cb56422437c955f6487c195eecccaa6";
            string authToken = "b402568cb8a6047c8725bb097c792f22";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "El código OTP es el siguiente: " + user.OTPSMS,
                from: new Twilio.Types.PhoneNumber("+12059531826"),
                to: new Twilio.Types.PhoneNumber("+506" + user.Telefono)
            );

            Console.WriteLine(message.Sid);
        }
        public static string encoding(string toEncode) {
            byte[] bytes = Encoding.GetEncoding(28591).GetBytes(toEncode);
            string toReturn = System.Convert.ToBase64String(bytes);
            return toReturn;
        }

        public static async Task EnviarCorreoMembresia(Usuarios user, Membresias membresia, string resultado) {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("rentnridecom@gmail.com", "RentNRide.com");
            var subject = "Estado Solicitud";
            var to = new EmailAddress(user.Correo, "Usuario");
            var plainTextContent = "El resultado de su solicitud es " + resultado;
            string contenido = "";
            if(resultado == "ACEPTADO") {
                var correoEncr = encoding(user.Correo);
                var membEncr = encoding(membresia.Id + "");

                contenido = contenido + $@"<h2>Membresia Recibida</h2>
    <table>
        <tr>
            <th> Nombre </th>
            <th> Monto Mensual </th>
            <th> Comisión Transacción</th>
            <th> Número de Días</th>
        </tr>
        <tr>
            <td> {membresia.Nombre} </td>
            <td>{membresia.MontoMensual}</td>
            <td>{membresia.ComisionTransaccion}</td>
            <td>{membresia.NumDias}</td>
        </tr >
    </table> ";

                contenido += $"<a href=\"https://localhost:44383/Home/Membresia/?correo={correoEncr}&membresia={membEncr}\" > Dale click aquí para ver su membresía</a>";
            } else {
                contenido += "<p> Lo sentimos su solicitud no cumple con los requisitos establecidos  </p>";
            }
            var htmlContent = contenido;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public void generarFactura(Factura factura, Reserva reserva, int horas, Vehiculo auto)
        {
            Document document = new Document();
            Page page = document.Pages.Add();

            string nombreEmpresa = "Softlutions S.A";
            string correoEmpresa = "rentnridecom@gmail.com";
            crearEncabezado(document, page, nombreEmpresa, correoEmpresa);

            string control = factura.Consecutivo;
            string tiquete = "Factura Electrónica";
            string clave = factura.Clave;
            DateTime fechaEmision = factura.FechaEmision;
            crearCamposFactura(document, page, control, tiquete, clave, fechaEmision);

            string cedula = factura.Identificacion;
            string nombreCompleto = factura.Nombre;
            string correo = factura.Correo;
            crearDatosReceptor(document, page, cedula, nombreCompleto, correo);

            int cod = 12234;
            int cant = horas;
            string descripcion = "Alquiler de vehículo";
            decimal precioUnitario = reserva.Tarifa;
            decimal total = cant * precioUnitario + reserva.Comision;
            crearCargos(document, page, cod, cant, descripcion, precioUnitario, total, reserva, auto);

            crearFooter(document, page, factura);
        }


        static void crearEncabezado(Document document, Page page, string nombreEmpresa, string correo)
        {
            //--------------------------Datos encabezado------------------------------
            var empresa = new TextFragment(nombreEmpresa);
            empresa.TextState.Font = FontRepository.FindFont("Arial");
            empresa.TextState.FontStyle = FontStyles.Bold;
            empresa.TextState.FontSize = 20;
            empresa.HorizontalAlignment = HorizontalAlignment.Left;
            page.Paragraphs.Add(empresa);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Cédula jurídica: 3101222222"));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Dirección: Del parque Central 100 norte, 100 sur"));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Teléfono: +506 2222-2222"));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Correo electrónico: " + correo));

            // Logo
            //var imageFileName = System.IO.Path.Combine(_dataDir, "logo.png");
            //page.AddImage(imageFileName, new Rectangle(0, 0, page.PageInfo.Width, 15));
            ImageStamp imageStamp = new ImageStamp(_dataDir + "logo.png");
            // Set properties of the stamp
            imageStamp.TopMargin = 60;
            imageStamp.RightMargin = 75;
            imageStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imageStamp.VerticalAlignment = VerticalAlignment.Top;
            imageStamp.Height = 75;
            imageStamp.Width = 75;
            page.AddStamp(imageStamp);

            //Dibujar línea
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(100, 5);
            page.Paragraphs.Add(graph);
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(new float[] { 0, 0, (float)430, 0 });
            line.GraphInfo.DashPhase = 1;
            graph.Shapes.Add(line);
        }

        static void crearCamposFactura(Document document, Page page, string control, string tiquete, string clave, DateTime fechaEmision)
        {
            //--------------------------Campos factura---------------------------------
            var numeroControl = new TextFragment("Número de control: " + control);
            numeroControl.TextState.Font = FontRepository.FindFont("Arial");
            numeroControl.TextState.FontStyle = FontStyles.Bold;
            numeroControl.TextState.FontSize = 15;
            numeroControl.Margin.Top = 10;
            page.Paragraphs.Add(numeroControl);

            var tiqueteElect = new TextFragment("Tiquete electrónico: " + tiquete);
            tiqueteElect.TextState.Font = FontRepository.FindFont("Arial");
            tiqueteElect.TextState.FontStyle = FontStyles.Bold;
            tiqueteElect.TextState.FontSize = 12;
            tiqueteElect.HorizontalAlignment = HorizontalAlignment.Left;
            tiqueteElect.Margin.Top = 5;
            page.Paragraphs.Add(tiqueteElect);

            var claveNum = new TextFragment("Clave: " + clave);
            claveNum.TextState.Font = FontRepository.FindFont("Arial");
            claveNum.TextState.FontStyle = FontStyles.Bold;
            claveNum.HorizontalAlignment = HorizontalAlignment.Left;
            claveNum.Margin.Top = 5;
            page.Paragraphs.Add(claveNum);

            var campoFecha = new TextFragment("Fecha de emisión: " + fechaEmision);
            campoFecha.TextState.Font = FontRepository.FindFont("Arial");
            campoFecha.TextState.FontStyle = FontStyles.Bold;
            campoFecha.HorizontalAlignment = HorizontalAlignment.Left;
            campoFecha.Margin.Top = 5;
            page.Paragraphs.Add(campoFecha);

            //Dibujar línea
            Aspose.Pdf.Drawing.Graph graph1 = new Aspose.Pdf.Drawing.Graph(100, 5);
            page.Paragraphs.Add(graph1);
            Aspose.Pdf.Drawing.Line line1 = new Aspose.Pdf.Drawing.Line(new float[] { 0, 0, (float)430, 0 });
            line1.GraphInfo.DashPhase = 1;
            graph1.Shapes.Add(line1);

        }

        static void crearDatosReceptor(Document document, Page page, string cedula, string nombreCompleto, string correo)
        {
            //-----------------------Datos receptor-------------------------------------
            var campoReceptor = new TextFragment("Datos del Receptor: ");
            campoReceptor.TextState.Font = FontRepository.FindFont("Arial");
            campoReceptor.TextState.FontStyle = FontStyles.Bold;
            campoReceptor.TextState.FontSize = 15;
            campoReceptor.HorizontalAlignment = HorizontalAlignment.Left;
            campoReceptor.Margin.Top = 10;
            page.Paragraphs.Add(campoReceptor);

            //Dibujar línea
            Aspose.Pdf.Drawing.Graph graph2 = new Aspose.Pdf.Drawing.Graph(100, 5);
            page.Paragraphs.Add(graph2);
            Aspose.Pdf.Drawing.Line line2 = new Aspose.Pdf.Drawing.Line(new float[] { 0, 0, (float)430, 0 });
            line2.GraphInfo.DashPhase = 1;
            graph2.Shapes.Add(line2);

            var campoNombre = new TextFragment(cedula + "-" + nombreCompleto);
            campoNombre.TextState.Font = FontRepository.FindFont("Arial");
            campoNombre.TextState.FontStyle = FontStyles.Bold;
            campoNombre.TextState.FontSize = 12;
            campoNombre.HorizontalAlignment = HorizontalAlignment.Left;
            campoNombre.Margin.Top = 5;
            page.Paragraphs.Add(campoNombre);

            var campoCorreo = new TextFragment("Correo electrónico: " + correo);
            campoCorreo.TextState.Font = FontRepository.FindFont("Arial");
            campoCorreo.TextState.FontStyle = FontStyles.Bold;
            campoCorreo.HorizontalAlignment = HorizontalAlignment.Left;
            campoCorreo.Margin.Top = 5;
            page.Paragraphs.Add(campoCorreo);

            //Dibujar línea
            Aspose.Pdf.Drawing.Graph graph3 = new Aspose.Pdf.Drawing.Graph(100, 5);
            page.Paragraphs.Add(graph3);
            Aspose.Pdf.Drawing.Line line3 = new Aspose.Pdf.Drawing.Line(new float[] { 0, 0, (float)430, 0 });
            line3.GraphInfo.DashPhase = 1;
            graph3.Shapes.Add(line2);
        }

        static void crearCargos(Document document, Page page, int cod, int cant, string descripcion, decimal precioUnitario, decimal total, Reserva reserva, Vehiculo auto)
        {

            //-----------------------Cargos-----------------------------------------

            // Add table
            var table = new Table
            {
                //ColumnWidths = "200",
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,
                Border = new BorderInfo(BorderSide.Box, 1f, Color.DarkSlateGray),
                DefaultCellBorder = new BorderInfo(BorderSide.Box, 0.5f, Color.Black),
                DefaultCellPadding = new MarginInfo(4.5, 4.5, 4.5, 4.5),
                Margin =
                {
                    Top = 10,
                    Bottom = 10
                },
                DefaultCellTextState =
                {
                    Font =  FontRepository.FindFont("Helvetica")
                }
            };

            var headerRow = table.Rows.Add();
            headerRow.Cells.Add("#");
            headerRow.Cells.Add("Cod.");
            headerRow.Cells.Add("Cant.");
            headerRow.Cells.Add("Descripción");
            headerRow.Cells.Add("P/Unit.");
            headerRow.Cells.Add("Total");
            foreach (Cell headerRowCell in headerRow.Cells)
            {
                headerRowCell.BackgroundColor = Color.Gray;
                headerRowCell.DefaultCellTextState.ForegroundColor = Color.WhiteSmoke;
            }

            int k = 1;
            var dataRow = table.Rows.Add();
            dataRow.Cells.Add(Convert.ToString(k));
            dataRow.Cells.Add(Convert.ToString(cod));
            dataRow.Cells.Add(Convert.ToString(cant));
            dataRow.Cells.Add(descripcion);
            dataRow.Cells.Add(Convert.ToString(precioUnitario));
            dataRow.Cells.Add(Convert.ToString(total));



            string excedido = "no";
            int codKm = 12488;
            int cantKm = 0;
            string descripcionKm = "Cargo km excedido";
            decimal precioUnitarioKm = auto.cKmExcedido;
            decimal totalKm = 0;
            if (reserva.KmExcedido != 0)
            {
                k++;
                cantKm = 1;
                totalKm = Decimal.Multiply(cantKm, precioUnitarioKm);
                var filasKm = table.Rows.Add();
                filasKm.Cells.Add(Convert.ToString(k));
                filasKm.Cells.Add(Convert.ToString(codKm));
                filasKm.Cells.Add(Convert.ToString(cantKm));
                filasKm.Cells.Add(descripcionKm);
                filasKm.Cells.Add(Convert.ToString(precioUnitarioKm));
                filasKm.Cells.Add(Convert.ToString(totalKm));
            }

            string lugarDiferente = "yes";
            int codDif = 12488;
            int cantDif = 0;
            string descripcionDif = "Lugar diferente";
            decimal precioUnitarioDif = auto.cLugarDiferente;
            decimal totalDif = 0;
            if (reserva.Entrega != 0)
            {
                k++;
                cantDif = 1;
                totalDif = cantDif * precioUnitarioDif;
                var filasDif = table.Rows.Add();
                filasDif.Cells.Add(Convert.ToString(k));
                filasDif.Cells.Add(Convert.ToString(codDif));
                filasDif.Cells.Add(Convert.ToString(cantDif));
                filasDif.Cells.Add(descripcionDif);
                filasDif.Cells.Add(Convert.ToString(precioUnitarioDif));
                filasDif.Cells.Add(Convert.ToString(totalDif));
            }

            string malEstado = "no";
            int codMal = 12488;
            int cantMal = 0;
            string descripcionMal = "Vehículo en mal estado";
            decimal precioUnitarioMal = auto.cMalEstado;
            decimal totalMal = 0;
            if (reserva.MalEstado != 0)
            {
                k++;
                cantMal = 1;
                totalMal = cantMal * precioUnitarioMal;
                var filasMal = table.Rows.Add();
                filasMal.Cells.Add(Convert.ToString(k));
                filasMal.Cells.Add(Convert.ToString(codMal));
                filasMal.Cells.Add(Convert.ToString(cantMal));
                filasMal.Cells.Add(descripcionMal);
                filasMal.Cells.Add(Convert.ToString(precioUnitarioMal));
                filasMal.Cells.Add(Convert.ToString(totalMal));
            }


            page.Paragraphs.Add(table);

            //-----------------------Resumen operacion-------------------------------------

            // Add table

            // Initializes a new instance of the Table
            Table tablaResumen = new Table();

            tablaResumen.Margin.Left = 225;

            // Set default cell border using BorderInfo object
            tablaResumen.DefaultCellBorder = new BorderInfo(BorderSide.None, 0.1F);

            // Set table border using another customized BorderInfo object
            tablaResumen.Border = new BorderInfo(BorderSide.All, 1F);

            // Create MarginInfo object and set its left, bottom, right and top margins
            MarginInfo margin = new MarginInfo();
            margin.Top = 5f;
            margin.Left = 5f;
            margin.Right = 5f;
            margin.Bottom = 5f;

            tablaResumen.DefaultCellPadding = margin;

            Row excento = tablaResumen.Rows.Add();
            excento.Cells.Add("Exento: ");
            excento.Cells.Add(Convert.ToString(0));

            Row gravado = tablaResumen.Rows.Add();
            gravado.Cells.Add("Gravado: ");
            gravado.Cells.Add(Convert.ToString(total + totalKm + totalDif + totalMal));

            Row exonerado = tablaResumen.Rows.Add();
            exonerado.Cells.Add("Exonerado: ");
            exonerado.Cells.Add(Convert.ToString(0));

            Row subTotal = tablaResumen.Rows.Add();
            subTotal.Cells.Add("Sub-total: ");
            subTotal.Cells.Add(Convert.ToString(total + totalKm + totalDif + totalMal));
            subTotal.DefaultCellTextState.FontStyle = FontStyles.Bold;
            subTotal.DefaultCellTextState.FontSize = 12;


            int ingresarDescuento = 0;
            Row descuentos = tablaResumen.Rows.Add();
            descuentos.Cells.Add("Descuentos: ");
            descuentos.Cells.Add(Convert.ToString(total * ingresarDescuento));

            Row impuesto = tablaResumen.Rows.Add();
            impuesto.Cells.Add("Impuesto: ");
            impuesto.Cells.Add(Convert.ToString(Decimal.Multiply((total + totalKm + totalDif + totalMal), (decimal)1.13)));


            Row totalFinal = tablaResumen.Rows.Add();
            totalFinal.Cells.Add("Total: ");
            totalFinal.Cells.Add(Convert.ToString(Decimal.Multiply((total + totalKm + totalDif + totalMal), (decimal)1.13) - total * ingresarDescuento));
            totalFinal.DefaultCellTextState.FontStyle = FontStyles.Bold;
            totalFinal.DefaultCellTextState.FontSize = 15;

            Row moneda = tablaResumen.Rows.Add();
            moneda.Cells.Add("Moneda: ");
            moneda.Cells.Add("CRC");

            Row tipoCambio = tablaResumen.Rows.Add();
            tipoCambio.Cells.Add("Tipo de cambio: ");
            tipoCambio.Cells.Add(Convert.ToString(1));

            tablaResumen.Alignment = HorizontalAlignment.Right;
            // Add table object to first page of input document
            page.Paragraphs.Add(tablaResumen);
        }

        static void crearFooter(Document document, Page page, Factura factura)
        {
            //-----------------------Footer------------------------------------------------
            var leyenda = new TextFragment("Autorizado mediante la resolución DGT-R-033-2019 del 20/06/2019 de la D.G.T.");
            leyenda.TextState.Font = FontRepository.FindFont("Arial");
            leyenda.TextState.FontStyle = FontStyles.Bold;
            leyenda.TextState.FontSize = 12;
            leyenda.HorizontalAlignment = HorizontalAlignment.Center;
            leyenda.Margin.Top = 10;
            page.Paragraphs.Add(leyenda);

            var version = new TextFragment("Comprobante Electrónico v4.3");
            version.TextState.Font = FontRepository.FindFont("Arial");
            version.TextState.FontStyle = FontStyles.Bold;
            version.HorizontalAlignment = HorizontalAlignment.Center;
            version.Margin.Top = 5;
            page.Paragraphs.Add(version);



            //-----------------------Guardar el documento-----------------------------------
            document.Save(System.IO.Path.Combine(_dataDir, "factura" + factura.IdFactura + ".pdf"));

        }


    }
}
