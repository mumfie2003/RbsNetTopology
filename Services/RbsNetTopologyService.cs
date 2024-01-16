using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using RbsNetTopology.Data;

namespace RbsNetTopology
{
    public partial class rbs_net_topologyService
    {
        rbs_net_topologyContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly rbs_net_topologyContext context;
        private readonly NavigationManager navigationManager;

        public rbs_net_topologyService(rbs_net_topologyContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportIssuesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/rbs_net_topology/issues/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/rbs_net_topology/issues/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportIssuesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/rbs_net_topology/issues/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/rbs_net_topology/issues/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnIssuesRead(ref IQueryable<RbsNetTopology.Models.rbs_net_topology.Issue> items);

        public async Task<IQueryable<RbsNetTopology.Models.rbs_net_topology.Issue>> GetIssues(Query query = null)
        {
            var items = Context.Issues.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnIssuesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnIssueGet(RbsNetTopology.Models.rbs_net_topology.Issue item);
        partial void OnGetIssueById(ref IQueryable<RbsNetTopology.Models.rbs_net_topology.Issue> items);


        public async Task<RbsNetTopology.Models.rbs_net_topology.Issue> GetIssueById(int id)
        {
            var items = Context.Issues
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetIssueById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnIssueGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnIssueCreated(RbsNetTopology.Models.rbs_net_topology.Issue item);
        partial void OnAfterIssueCreated(RbsNetTopology.Models.rbs_net_topology.Issue item);

        public async Task<RbsNetTopology.Models.rbs_net_topology.Issue> CreateIssue(RbsNetTopology.Models.rbs_net_topology.Issue issue)
        {
            OnIssueCreated(issue);

            var existingItem = Context.Issues
                              .Where(i => i.Id == issue.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Issues.Add(issue);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(issue).State = EntityState.Detached;
                throw;
            }

            OnAfterIssueCreated(issue);

            return issue;
        }

        public async Task<RbsNetTopology.Models.rbs_net_topology.Issue> CancelIssueChanges(RbsNetTopology.Models.rbs_net_topology.Issue item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnIssueUpdated(RbsNetTopology.Models.rbs_net_topology.Issue item);
        partial void OnAfterIssueUpdated(RbsNetTopology.Models.rbs_net_topology.Issue item);

        public async Task<RbsNetTopology.Models.rbs_net_topology.Issue> UpdateIssue(int id, RbsNetTopology.Models.rbs_net_topology.Issue issue)
        {
            OnIssueUpdated(issue);

            var itemToUpdate = Context.Issues
                              .Where(i => i.Id == issue.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(issue);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterIssueUpdated(issue);

            return issue;
        }

        partial void OnIssueDeleted(RbsNetTopology.Models.rbs_net_topology.Issue item);
        partial void OnAfterIssueDeleted(RbsNetTopology.Models.rbs_net_topology.Issue item);

        public async Task<RbsNetTopology.Models.rbs_net_topology.Issue> DeleteIssue(int id)
        {
            var itemToDelete = Context.Issues
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnIssueDeleted(itemToDelete);


            Context.Issues.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterIssueDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}