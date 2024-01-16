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
    public partial class Issues
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

        protected IEnumerable<RbsNetTopology.Models.rbs_net_topology.Issue> issues;

        protected RadzenDataGrid<RbsNetTopology.Models.rbs_net_topology.Issue> grid0;
        protected override async Task OnInitializedAsync()
        {
            issues = await rbs_net_topologyService.GetIssues();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddIssue>("Add Issue", null);
            await grid0.Reload();
        }

        protected async Task EditRow(RbsNetTopology.Models.rbs_net_topology.Issue args)
        {
            await DialogService.OpenAsync<EditIssue>("Edit Issue", new Dictionary<string, object> { {"Id", args.Id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, RbsNetTopology.Models.rbs_net_topology.Issue issue)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await rbs_net_topologyService.DeleteIssue(issue.Id);

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
                    Detail = $"Unable to delete Issue"
                });
            }
        }
    }
}