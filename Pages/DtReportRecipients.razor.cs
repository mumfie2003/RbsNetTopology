using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace RbsNetTopology.Pages
{
    public partial class DtReportRecipients
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public rbs_net_topologyService rbs_net_topologyService { get; set; }

        protected IEnumerable<RbsNetTopology.Models.rbs_net_topology.DtReportRecipient> dtReportRecipients;

        protected RadzenDataGrid<RbsNetTopology.Models.rbs_net_topology.DtReportRecipient> grid0;
        protected override async Task OnInitializedAsync()
        {
            dtReportRecipients = await rbs_net_topologyService.GetDtReportRecipients();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddDtReportRecipient>("Add DtReportRecipient", null);
            await grid0.Reload();
        }

        protected async Task EditRow(RbsNetTopology.Models.rbs_net_topology.DtReportRecipient args)
        {
            await DialogService.OpenAsync<EditDtReportRecipient>("Edit DtReportRecipient", new Dictionary<string, object> { {"Code", args.Code} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, RbsNetTopology.Models.rbs_net_topology.DtReportRecipient dtReportRecipient)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await rbs_net_topologyService.DeleteDtReportRecipient(dtReportRecipient.Code);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete DtReportRecipient"
                });
            }
        }
    }
}