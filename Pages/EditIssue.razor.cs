using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RbsNetTopology.Utils;

namespace RbsNetTopology.Pages
{
    public partial class EditIssue
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

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            issue = await rbs_net_topologyService.GetIssueById(Id);
        }
        protected bool errorVisible;
        protected RbsNetTopology.Models.rbs_net_topology.Issue issue;

        protected async Task FormSubmit()
        {
            try
            {
#if !RADZEN
                MappingUtils mappingUtils = new MappingUtils();
                issue.Location = mappingUtils.CalculateLocation(issue.Latitude, issue.Longitude);
#endif  
                await rbs_net_topologyService.UpdateIssue(Id, issue);
                DialogService.Close(issue);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}