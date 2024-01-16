using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using RbsNetTopology.Data;

namespace RbsNetTopology.Controllers
{
    public partial class Exportrbs_net_topologyController : ExportController
    {
        private readonly rbs_net_topologyContext context;
        private readonly rbs_net_topologyService service;

        public Exportrbs_net_topologyController(rbs_net_topologyContext context, rbs_net_topologyService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/rbs_net_topology/dtreportrecipients/csv")]
        [HttpGet("/export/rbs_net_topology/dtreportrecipients/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDtReportRecipientsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDtReportRecipients(), Request.Query, false), fileName);
        }

        [HttpGet("/export/rbs_net_topology/dtreportrecipients/excel")]
        [HttpGet("/export/rbs_net_topology/dtreportrecipients/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDtReportRecipientsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDtReportRecipients(), Request.Query, false), fileName);
        }

        [HttpGet("/export/rbs_net_topology/dtstatustypes/csv")]
        [HttpGet("/export/rbs_net_topology/dtstatustypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDtStatusTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDtStatusTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/rbs_net_topology/dtstatustypes/excel")]
        [HttpGet("/export/rbs_net_topology/dtstatustypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDtStatusTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDtStatusTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/rbs_net_topology/issues/csv")]
        [HttpGet("/export/rbs_net_topology/issues/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportIssuesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetIssues(), Request.Query, false), fileName);
        }

        [HttpGet("/export/rbs_net_topology/issues/excel")]
        [HttpGet("/export/rbs_net_topology/issues/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportIssuesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetIssues(), Request.Query, false), fileName);
        }
    }
}
