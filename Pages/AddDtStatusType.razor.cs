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
    public partial class AddDtStatusType
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

        protected override async Task OnInitializedAsync()
        {
            dtStatusType = new RbsNetTopology.Models.rbs_net_topology.DtStatusType();
        }
        protected bool errorVisible;
        protected RbsNetTopology.Models.rbs_net_topology.DtStatusType dtStatusType;

        protected async Task FormSubmit()
        {
            try
            {
                await rbs_net_topologyService.CreateDtStatusType(dtStatusType);
                DialogService.Close(dtStatusType);
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