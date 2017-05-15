<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register TagPrefix="Modelweb" Namespace="CLicense.Utils"%>
<%@ Register TagPrefix="rsweb" Namespace="CLicense" %>
<%@ Register TagPrefix="rsweb" Namespace="System.Web.Configuration" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>ReportViwer in MVC4 Application</title>  
    <script runat="server">
        void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                    ReportViewer1.ServerReport.ReportServerUrl = 
                        new Uri(WebConfigurationManager.AppSettings["ReportServicePath"].ToString());
                    ReportViewer1.ServerReport.ReportPath = "/CLicenseReports/"+TempData["ReportName"].ToString();
                    ReportViewer1.ServerReport.SetParameters((ReportParameterCollection)TempData["ReportParams"]);
                    ReportViewer1.ServerReport.Refresh();
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.InnerException.ToString();
                    throw;
                }
            }
        }
</script>
</head>

<body>
    <form id="form1" runat="server">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" AsyncRendering="false" BorderStyle="None" Height="1400px" Width="750px">
            <%--<ServerReport  ReportServerUrl="http://10.76.96.90/ReportServer" Timeout="900000" />--%>
            <ServerReport  Timeout="900000" />
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        </p>
    </form>
</body>
</html>