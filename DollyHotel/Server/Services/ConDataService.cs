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

using DollyHotel.Server.Data;

namespace DollyHotel.Server
{
    public partial class ConDataService
    {
        ConDataContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly ConDataContext context;
        private readonly NavigationManager navigationManager;

        public ConDataService(ConDataContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);


        public async Task ExportAspNetRoleClaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetRoleClaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetRoleClaimsRead(ref IQueryable<DollyHotel.Server.Models.ConData.AspNetRoleClaim> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.AspNetRoleClaim>> GetAspNetRoleClaims(Query query = null)
        {
            var items = Context.AspNetRoleClaims.AsQueryable();

            items = items.Include(i => i.AspNetRole);

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

            OnAspNetRoleClaimsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetRoleClaimGet(DollyHotel.Server.Models.ConData.AspNetRoleClaim item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRoleClaim> GetAspNetRoleClaimById(int id)
        {
            var items = Context.AspNetRoleClaims
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AspNetRole);
  
            var itemToReturn = items.FirstOrDefault();

            OnAspNetRoleClaimGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetRoleClaimCreated(DollyHotel.Server.Models.ConData.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimCreated(DollyHotel.Server.Models.ConData.AspNetRoleClaim item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRoleClaim> CreateAspNetRoleClaim(DollyHotel.Server.Models.ConData.AspNetRoleClaim aspnetroleclaim)
        {
            OnAspNetRoleClaimCreated(aspnetroleclaim);

            var existingItem = Context.AspNetRoleClaims
                              .Where(i => i.Id == aspnetroleclaim.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetRoleClaims.Add(aspnetroleclaim);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetroleclaim).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetRoleClaimCreated(aspnetroleclaim);

            return aspnetroleclaim;
        }

        public async Task<DollyHotel.Server.Models.ConData.AspNetRoleClaim> CancelAspNetRoleClaimChanges(DollyHotel.Server.Models.ConData.AspNetRoleClaim item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetRoleClaimUpdated(DollyHotel.Server.Models.ConData.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimUpdated(DollyHotel.Server.Models.ConData.AspNetRoleClaim item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRoleClaim> UpdateAspNetRoleClaim(int id, DollyHotel.Server.Models.ConData.AspNetRoleClaim aspnetroleclaim)
        {
            OnAspNetRoleClaimUpdated(aspnetroleclaim);

            var itemToUpdate = Context.AspNetRoleClaims
                              .Where(i => i.Id == aspnetroleclaim.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetroleclaim);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetRoleClaimUpdated(aspnetroleclaim);

            return aspnetroleclaim;
        }

        partial void OnAspNetRoleClaimDeleted(DollyHotel.Server.Models.ConData.AspNetRoleClaim item);
        partial void OnAfterAspNetRoleClaimDeleted(DollyHotel.Server.Models.ConData.AspNetRoleClaim item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRoleClaim> DeleteAspNetRoleClaim(int id)
        {
            var itemToDelete = Context.AspNetRoleClaims
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetRoleClaimDeleted(itemToDelete);


            Context.AspNetRoleClaims.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetRoleClaimDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetRolesRead(ref IQueryable<DollyHotel.Server.Models.ConData.AspNetRole> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.AspNetRole>> GetAspNetRoles(Query query = null)
        {
            var items = Context.AspNetRoles.AsQueryable();


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

            OnAspNetRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetRoleGet(DollyHotel.Server.Models.ConData.AspNetRole item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRole> GetAspNetRoleById(string id)
        {
            var items = Context.AspNetRoles
                              .AsNoTracking()
                              .Where(i => i.Id == id);

  
            var itemToReturn = items.FirstOrDefault();

            OnAspNetRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetRoleCreated(DollyHotel.Server.Models.ConData.AspNetRole item);
        partial void OnAfterAspNetRoleCreated(DollyHotel.Server.Models.ConData.AspNetRole item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRole> CreateAspNetRole(DollyHotel.Server.Models.ConData.AspNetRole aspnetrole)
        {
            OnAspNetRoleCreated(aspnetrole);

            var existingItem = Context.AspNetRoles
                              .Where(i => i.Id == aspnetrole.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetRoles.Add(aspnetrole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetrole).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetRoleCreated(aspnetrole);

            return aspnetrole;
        }

        public async Task<DollyHotel.Server.Models.ConData.AspNetRole> CancelAspNetRoleChanges(DollyHotel.Server.Models.ConData.AspNetRole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetRoleUpdated(DollyHotel.Server.Models.ConData.AspNetRole item);
        partial void OnAfterAspNetRoleUpdated(DollyHotel.Server.Models.ConData.AspNetRole item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRole> UpdateAspNetRole(string id, DollyHotel.Server.Models.ConData.AspNetRole aspnetrole)
        {
            OnAspNetRoleUpdated(aspnetrole);

            var itemToUpdate = Context.AspNetRoles
                              .Where(i => i.Id == aspnetrole.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetrole);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetRoleUpdated(aspnetrole);

            return aspnetrole;
        }

        partial void OnAspNetRoleDeleted(DollyHotel.Server.Models.ConData.AspNetRole item);
        partial void OnAfterAspNetRoleDeleted(DollyHotel.Server.Models.ConData.AspNetRole item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRole> DeleteAspNetRole(string id)
        {
            var itemToDelete = Context.AspNetRoles
                              .Where(i => i.Id == id)
                              .Include(i => i.AspNetRoleClaims)
                              .Include(i => i.AspNetUserRoles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetRoleDeleted(itemToDelete);


            Context.AspNetRoles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetRoleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetUserClaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserClaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserClaimsRead(ref IQueryable<DollyHotel.Server.Models.ConData.AspNetUserClaim> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.AspNetUserClaim>> GetAspNetUserClaims(Query query = null)
        {
            var items = Context.AspNetUserClaims.AsQueryable();

            items = items.Include(i => i.AspNetUser);

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

            OnAspNetUserClaimsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserClaimGet(DollyHotel.Server.Models.ConData.AspNetUserClaim item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserClaim> GetAspNetUserClaimById(int id)
        {
            var items = Context.AspNetUserClaims
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AspNetUser);
  
            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserClaimGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetUserClaimCreated(DollyHotel.Server.Models.ConData.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimCreated(DollyHotel.Server.Models.ConData.AspNetUserClaim item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserClaim> CreateAspNetUserClaim(DollyHotel.Server.Models.ConData.AspNetUserClaim aspnetuserclaim)
        {
            OnAspNetUserClaimCreated(aspnetuserclaim);

            var existingItem = Context.AspNetUserClaims
                              .Where(i => i.Id == aspnetuserclaim.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserClaims.Add(aspnetuserclaim);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuserclaim).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserClaimCreated(aspnetuserclaim);

            return aspnetuserclaim;
        }

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserClaim> CancelAspNetUserClaimChanges(DollyHotel.Server.Models.ConData.AspNetUserClaim item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserClaimUpdated(DollyHotel.Server.Models.ConData.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimUpdated(DollyHotel.Server.Models.ConData.AspNetUserClaim item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserClaim> UpdateAspNetUserClaim(int id, DollyHotel.Server.Models.ConData.AspNetUserClaim aspnetuserclaim)
        {
            OnAspNetUserClaimUpdated(aspnetuserclaim);

            var itemToUpdate = Context.AspNetUserClaims
                              .Where(i => i.Id == aspnetuserclaim.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuserclaim);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetUserClaimUpdated(aspnetuserclaim);

            return aspnetuserclaim;
        }

        partial void OnAspNetUserClaimDeleted(DollyHotel.Server.Models.ConData.AspNetUserClaim item);
        partial void OnAfterAspNetUserClaimDeleted(DollyHotel.Server.Models.ConData.AspNetUserClaim item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserClaim> DeleteAspNetUserClaim(int id)
        {
            var itemToDelete = Context.AspNetUserClaims
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserClaimDeleted(itemToDelete);


            Context.AspNetUserClaims.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserClaimDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetUserLoginsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserLoginsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserLoginsRead(ref IQueryable<DollyHotel.Server.Models.ConData.AspNetUserLogin> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.AspNetUserLogin>> GetAspNetUserLogins(Query query = null)
        {
            var items = Context.AspNetUserLogins.AsQueryable();

            items = items.Include(i => i.AspNetUser);

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

            OnAspNetUserLoginsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserLoginGet(DollyHotel.Server.Models.ConData.AspNetUserLogin item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserLogin> GetAspNetUserLoginByLoginProviderAndProviderKey(string loginprovider, string providerkey)
        {
            var items = Context.AspNetUserLogins
                              .AsNoTracking()
                              .Where(i => i.LoginProvider == loginprovider && i.ProviderKey == providerkey);

            items = items.Include(i => i.AspNetUser);
  
            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserLoginGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetUserLoginCreated(DollyHotel.Server.Models.ConData.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginCreated(DollyHotel.Server.Models.ConData.AspNetUserLogin item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserLogin> CreateAspNetUserLogin(DollyHotel.Server.Models.ConData.AspNetUserLogin aspnetuserlogin)
        {
            OnAspNetUserLoginCreated(aspnetuserlogin);

            var existingItem = Context.AspNetUserLogins
                              .Where(i => i.LoginProvider == aspnetuserlogin.LoginProvider && i.ProviderKey == aspnetuserlogin.ProviderKey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserLogins.Add(aspnetuserlogin);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuserlogin).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserLoginCreated(aspnetuserlogin);

            return aspnetuserlogin;
        }

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserLogin> CancelAspNetUserLoginChanges(DollyHotel.Server.Models.ConData.AspNetUserLogin item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserLoginUpdated(DollyHotel.Server.Models.ConData.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginUpdated(DollyHotel.Server.Models.ConData.AspNetUserLogin item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserLogin> UpdateAspNetUserLogin(string loginprovider, string providerkey, DollyHotel.Server.Models.ConData.AspNetUserLogin aspnetuserlogin)
        {
            OnAspNetUserLoginUpdated(aspnetuserlogin);

            var itemToUpdate = Context.AspNetUserLogins
                              .Where(i => i.LoginProvider == aspnetuserlogin.LoginProvider && i.ProviderKey == aspnetuserlogin.ProviderKey)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuserlogin);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetUserLoginUpdated(aspnetuserlogin);

            return aspnetuserlogin;
        }

        partial void OnAspNetUserLoginDeleted(DollyHotel.Server.Models.ConData.AspNetUserLogin item);
        partial void OnAfterAspNetUserLoginDeleted(DollyHotel.Server.Models.ConData.AspNetUserLogin item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserLogin> DeleteAspNetUserLogin(string loginprovider, string providerkey)
        {
            var itemToDelete = Context.AspNetUserLogins
                              .Where(i => i.LoginProvider == loginprovider && i.ProviderKey == providerkey)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserLoginDeleted(itemToDelete);


            Context.AspNetUserLogins.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserLoginDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetUserRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserRolesRead(ref IQueryable<DollyHotel.Server.Models.ConData.AspNetUserRole> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.AspNetUserRole>> GetAspNetUserRoles(Query query = null)
        {
            var items = Context.AspNetUserRoles.AsQueryable();

            items = items.Include(i => i.AspNetRole);
            items = items.Include(i => i.AspNetUser);

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

            OnAspNetUserRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserRoleGet(DollyHotel.Server.Models.ConData.AspNetUserRole item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserRole> GetAspNetUserRoleByUserIdAndRoleId(string userid, string roleid)
        {
            var items = Context.AspNetUserRoles
                              .AsNoTracking()
                              .Where(i => i.UserId == userid && i.RoleId == roleid);

            items = items.Include(i => i.AspNetRole);
            items = items.Include(i => i.AspNetUser);
  
            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetUserRoleCreated(DollyHotel.Server.Models.ConData.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleCreated(DollyHotel.Server.Models.ConData.AspNetUserRole item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserRole> CreateAspNetUserRole(DollyHotel.Server.Models.ConData.AspNetUserRole aspnetuserrole)
        {
            OnAspNetUserRoleCreated(aspnetuserrole);

            var existingItem = Context.AspNetUserRoles
                              .Where(i => i.UserId == aspnetuserrole.UserId && i.RoleId == aspnetuserrole.RoleId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserRoles.Add(aspnetuserrole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuserrole).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserRoleCreated(aspnetuserrole);

            return aspnetuserrole;
        }

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserRole> CancelAspNetUserRoleChanges(DollyHotel.Server.Models.ConData.AspNetUserRole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserRoleUpdated(DollyHotel.Server.Models.ConData.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleUpdated(DollyHotel.Server.Models.ConData.AspNetUserRole item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserRole> UpdateAspNetUserRole(string userid, string roleid, DollyHotel.Server.Models.ConData.AspNetUserRole aspnetuserrole)
        {
            OnAspNetUserRoleUpdated(aspnetuserrole);

            var itemToUpdate = Context.AspNetUserRoles
                              .Where(i => i.UserId == aspnetuserrole.UserId && i.RoleId == aspnetuserrole.RoleId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuserrole);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetUserRoleUpdated(aspnetuserrole);

            return aspnetuserrole;
        }

        partial void OnAspNetUserRoleDeleted(DollyHotel.Server.Models.ConData.AspNetUserRole item);
        partial void OnAfterAspNetUserRoleDeleted(DollyHotel.Server.Models.ConData.AspNetUserRole item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserRole> DeleteAspNetUserRole(string userid, string roleid)
        {
            var itemToDelete = Context.AspNetUserRoles
                              .Where(i => i.UserId == userid && i.RoleId == roleid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserRoleDeleted(itemToDelete);


            Context.AspNetUserRoles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserRoleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUsersRead(ref IQueryable<DollyHotel.Server.Models.ConData.AspNetUser> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.AspNetUser>> GetAspNetUsers(Query query = null)
        {
            var items = Context.AspNetUsers.AsQueryable();


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

            OnAspNetUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserGet(DollyHotel.Server.Models.ConData.AspNetUser item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUser> GetAspNetUserById(string id)
        {
            var items = Context.AspNetUsers
                              .AsNoTracking()
                              .Where(i => i.Id == id);

  
            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetUserCreated(DollyHotel.Server.Models.ConData.AspNetUser item);
        partial void OnAfterAspNetUserCreated(DollyHotel.Server.Models.ConData.AspNetUser item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUser> CreateAspNetUser(DollyHotel.Server.Models.ConData.AspNetUser aspnetuser)
        {
            OnAspNetUserCreated(aspnetuser);

            var existingItem = Context.AspNetUsers
                              .Where(i => i.Id == aspnetuser.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUsers.Add(aspnetuser);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuser).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserCreated(aspnetuser);

            return aspnetuser;
        }

        public async Task<DollyHotel.Server.Models.ConData.AspNetUser> CancelAspNetUserChanges(DollyHotel.Server.Models.ConData.AspNetUser item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserUpdated(DollyHotel.Server.Models.ConData.AspNetUser item);
        partial void OnAfterAspNetUserUpdated(DollyHotel.Server.Models.ConData.AspNetUser item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUser> UpdateAspNetUser(string id, DollyHotel.Server.Models.ConData.AspNetUser aspnetuser)
        {
            OnAspNetUserUpdated(aspnetuser);

            var itemToUpdate = Context.AspNetUsers
                              .Where(i => i.Id == aspnetuser.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuser);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetUserUpdated(aspnetuser);

            return aspnetuser;
        }

        partial void OnAspNetUserDeleted(DollyHotel.Server.Models.ConData.AspNetUser item);
        partial void OnAfterAspNetUserDeleted(DollyHotel.Server.Models.ConData.AspNetUser item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUser> DeleteAspNetUser(string id)
        {
            var itemToDelete = Context.AspNetUsers
                              .Where(i => i.Id == id)
                              .Include(i => i.AspNetUserClaims)
                              .Include(i => i.AspNetUserLogins)
                              .Include(i => i.AspNetUserRoles)
                              .Include(i => i.AspNetUserTokens)
                              .Include(i => i.RoomBookings)
                              .Include(i => i.Searches)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserDeleted(itemToDelete);


            Context.AspNetUsers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspNetUserTokensToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspNetUserTokensToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspNetUserTokensRead(ref IQueryable<DollyHotel.Server.Models.ConData.AspNetUserToken> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.AspNetUserToken>> GetAspNetUserTokens(Query query = null)
        {
            var items = Context.AspNetUserTokens.AsQueryable();

            items = items.Include(i => i.AspNetUser);

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

            OnAspNetUserTokensRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspNetUserTokenGet(DollyHotel.Server.Models.ConData.AspNetUserToken item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserToken> GetAspNetUserTokenByUserIdAndLoginProviderAndName(string userid, string loginprovider, string name)
        {
            var items = Context.AspNetUserTokens
                              .AsNoTracking()
                              .Where(i => i.UserId == userid && i.LoginProvider == loginprovider && i.Name == name);

            items = items.Include(i => i.AspNetUser);
  
            var itemToReturn = items.FirstOrDefault();

            OnAspNetUserTokenGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspNetUserTokenCreated(DollyHotel.Server.Models.ConData.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenCreated(DollyHotel.Server.Models.ConData.AspNetUserToken item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserToken> CreateAspNetUserToken(DollyHotel.Server.Models.ConData.AspNetUserToken aspnetusertoken)
        {
            OnAspNetUserTokenCreated(aspnetusertoken);

            var existingItem = Context.AspNetUserTokens
                              .Where(i => i.UserId == aspnetusertoken.UserId && i.LoginProvider == aspnetusertoken.LoginProvider && i.Name == aspnetusertoken.Name)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AspNetUserTokens.Add(aspnetusertoken);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetusertoken).State = EntityState.Detached;
                throw;
            }

            OnAfterAspNetUserTokenCreated(aspnetusertoken);

            return aspnetusertoken;
        }

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserToken> CancelAspNetUserTokenChanges(DollyHotel.Server.Models.ConData.AspNetUserToken item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspNetUserTokenUpdated(DollyHotel.Server.Models.ConData.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenUpdated(DollyHotel.Server.Models.ConData.AspNetUserToken item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserToken> UpdateAspNetUserToken(string userid, string loginprovider, string name, DollyHotel.Server.Models.ConData.AspNetUserToken aspnetusertoken)
        {
            OnAspNetUserTokenUpdated(aspnetusertoken);

            var itemToUpdate = Context.AspNetUserTokens
                              .Where(i => i.UserId == aspnetusertoken.UserId && i.LoginProvider == aspnetusertoken.LoginProvider && i.Name == aspnetusertoken.Name)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetusertoken);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspNetUserTokenUpdated(aspnetusertoken);

            return aspnetusertoken;
        }

        partial void OnAspNetUserTokenDeleted(DollyHotel.Server.Models.ConData.AspNetUserToken item);
        partial void OnAfterAspNetUserTokenDeleted(DollyHotel.Server.Models.ConData.AspNetUserToken item);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserToken> DeleteAspNetUserToken(string userid, string loginprovider, string name)
        {
            var itemToDelete = Context.AspNetUserTokens
                              .Where(i => i.UserId == userid && i.LoginProvider == loginprovider && i.Name == name)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspNetUserTokenDeleted(itemToDelete);


            Context.AspNetUserTokens.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspNetUserTokenDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportBedTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/bedtypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/bedtypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBedTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/bedtypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/bedtypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBedTypesRead(ref IQueryable<DollyHotel.Server.Models.ConData.BedType> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.BedType>> GetBedTypes(Query query = null)
        {
            var items = Context.BedTypes.AsQueryable();

            items = items.Include(i => i.Hotel);

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

            OnBedTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBedTypeGet(DollyHotel.Server.Models.ConData.BedType item);

        public async Task<DollyHotel.Server.Models.ConData.BedType> GetBedTypeByBedTypeId(long bedtypeid)
        {
            var items = Context.BedTypes
                              .AsNoTracking()
                              .Where(i => i.BedTypeID == bedtypeid);

            items = items.Include(i => i.Hotel);
  
            var itemToReturn = items.FirstOrDefault();

            OnBedTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBedTypeCreated(DollyHotel.Server.Models.ConData.BedType item);
        partial void OnAfterBedTypeCreated(DollyHotel.Server.Models.ConData.BedType item);

        public async Task<DollyHotel.Server.Models.ConData.BedType> CreateBedType(DollyHotel.Server.Models.ConData.BedType bedtype)
        {
            OnBedTypeCreated(bedtype);

            var existingItem = Context.BedTypes
                              .Where(i => i.BedTypeID == bedtype.BedTypeID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.BedTypes.Add(bedtype);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(bedtype).State = EntityState.Detached;
                throw;
            }

            OnAfterBedTypeCreated(bedtype);

            return bedtype;
        }

        public async Task<DollyHotel.Server.Models.ConData.BedType> CancelBedTypeChanges(DollyHotel.Server.Models.ConData.BedType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBedTypeUpdated(DollyHotel.Server.Models.ConData.BedType item);
        partial void OnAfterBedTypeUpdated(DollyHotel.Server.Models.ConData.BedType item);

        public async Task<DollyHotel.Server.Models.ConData.BedType> UpdateBedType(long bedtypeid, DollyHotel.Server.Models.ConData.BedType bedtype)
        {
            OnBedTypeUpdated(bedtype);

            var itemToUpdate = Context.BedTypes
                              .Where(i => i.BedTypeID == bedtype.BedTypeID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(bedtype);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBedTypeUpdated(bedtype);

            return bedtype;
        }

        partial void OnBedTypeDeleted(DollyHotel.Server.Models.ConData.BedType item);
        partial void OnAfterBedTypeDeleted(DollyHotel.Server.Models.ConData.BedType item);

        public async Task<DollyHotel.Server.Models.ConData.BedType> DeleteBedType(long bedtypeid)
        {
            var itemToDelete = Context.BedTypes
                              .Where(i => i.BedTypeID == bedtypeid)
                              .Include(i => i.RoomTypes)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBedTypeDeleted(itemToDelete);


            Context.BedTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBedTypeDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportBookingStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/bookingstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/bookingstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBookingStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/bookingstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/bookingstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBookingStatusesRead(ref IQueryable<DollyHotel.Server.Models.ConData.BookingStatus> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.BookingStatus>> GetBookingStatuses(Query query = null)
        {
            var items = Context.BookingStatuses.AsQueryable();


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

            OnBookingStatusesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBookingStatusGet(DollyHotel.Server.Models.ConData.BookingStatus item);

        public async Task<DollyHotel.Server.Models.ConData.BookingStatus> GetBookingStatusByBookingStatusId(int bookingstatusid)
        {
            var items = Context.BookingStatuses
                              .AsNoTracking()
                              .Where(i => i.BookingStatusID == bookingstatusid);

  
            var itemToReturn = items.FirstOrDefault();

            OnBookingStatusGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBookingStatusCreated(DollyHotel.Server.Models.ConData.BookingStatus item);
        partial void OnAfterBookingStatusCreated(DollyHotel.Server.Models.ConData.BookingStatus item);

        public async Task<DollyHotel.Server.Models.ConData.BookingStatus> CreateBookingStatus(DollyHotel.Server.Models.ConData.BookingStatus bookingstatus)
        {
            OnBookingStatusCreated(bookingstatus);

            var existingItem = Context.BookingStatuses
                              .Where(i => i.BookingStatusID == bookingstatus.BookingStatusID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.BookingStatuses.Add(bookingstatus);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(bookingstatus).State = EntityState.Detached;
                throw;
            }

            OnAfterBookingStatusCreated(bookingstatus);

            return bookingstatus;
        }

        public async Task<DollyHotel.Server.Models.ConData.BookingStatus> CancelBookingStatusChanges(DollyHotel.Server.Models.ConData.BookingStatus item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBookingStatusUpdated(DollyHotel.Server.Models.ConData.BookingStatus item);
        partial void OnAfterBookingStatusUpdated(DollyHotel.Server.Models.ConData.BookingStatus item);

        public async Task<DollyHotel.Server.Models.ConData.BookingStatus> UpdateBookingStatus(int bookingstatusid, DollyHotel.Server.Models.ConData.BookingStatus bookingstatus)
        {
            OnBookingStatusUpdated(bookingstatus);

            var itemToUpdate = Context.BookingStatuses
                              .Where(i => i.BookingStatusID == bookingstatus.BookingStatusID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(bookingstatus);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBookingStatusUpdated(bookingstatus);

            return bookingstatus;
        }

        partial void OnBookingStatusDeleted(DollyHotel.Server.Models.ConData.BookingStatus item);
        partial void OnAfterBookingStatusDeleted(DollyHotel.Server.Models.ConData.BookingStatus item);

        public async Task<DollyHotel.Server.Models.ConData.BookingStatus> DeleteBookingStatus(int bookingstatusid)
        {
            var itemToDelete = Context.BookingStatuses
                              .Where(i => i.BookingStatusID == bookingstatusid)
                              .Include(i => i.RoomBookingDetails)
                              .Include(i => i.RoomBookings)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBookingStatusDeleted(itemToDelete);


            Context.BookingStatuses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBookingStatusDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/cities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/cities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/cities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/cities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCitiesRead(ref IQueryable<DollyHotel.Server.Models.ConData.City> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.City>> GetCities(Query query = null)
        {
            var items = Context.Cities.AsQueryable();


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

            OnCitiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCityGet(DollyHotel.Server.Models.ConData.City item);

        public async Task<DollyHotel.Server.Models.ConData.City> GetCityByCityId(long cityid)
        {
            var items = Context.Cities
                              .AsNoTracking()
                              .Where(i => i.CityID == cityid);

  
            var itemToReturn = items.FirstOrDefault();

            OnCityGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCityCreated(DollyHotel.Server.Models.ConData.City item);
        partial void OnAfterCityCreated(DollyHotel.Server.Models.ConData.City item);

        public async Task<DollyHotel.Server.Models.ConData.City> CreateCity(DollyHotel.Server.Models.ConData.City city)
        {
            OnCityCreated(city);

            var existingItem = Context.Cities
                              .Where(i => i.CityID == city.CityID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Cities.Add(city);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(city).State = EntityState.Detached;
                throw;
            }

            OnAfterCityCreated(city);

            return city;
        }

        public async Task<DollyHotel.Server.Models.ConData.City> CancelCityChanges(DollyHotel.Server.Models.ConData.City item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCityUpdated(DollyHotel.Server.Models.ConData.City item);
        partial void OnAfterCityUpdated(DollyHotel.Server.Models.ConData.City item);

        public async Task<DollyHotel.Server.Models.ConData.City> UpdateCity(long cityid, DollyHotel.Server.Models.ConData.City city)
        {
            OnCityUpdated(city);

            var itemToUpdate = Context.Cities
                              .Where(i => i.CityID == city.CityID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(city);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCityUpdated(city);

            return city;
        }

        partial void OnCityDeleted(DollyHotel.Server.Models.ConData.City item);
        partial void OnAfterCityDeleted(DollyHotel.Server.Models.ConData.City item);

        public async Task<DollyHotel.Server.Models.ConData.City> DeleteCity(long cityid)
        {
            var itemToDelete = Context.Cities
                              .Where(i => i.CityID == cityid)
                              .Include(i => i.Hotels)
                              .Include(i => i.Searches)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCityDeleted(itemToDelete);


            Context.Cities.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCityDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportHotelFacilitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotelfacilities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotelfacilities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportHotelFacilitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotelfacilities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotelfacilities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnHotelFacilitiesRead(ref IQueryable<DollyHotel.Server.Models.ConData.HotelFacility> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.HotelFacility>> GetHotelFacilities(Query query = null)
        {
            var items = Context.HotelFacilities.AsQueryable();

            items = items.Include(i => i.Hotel);

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

            OnHotelFacilitiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnHotelFacilityGet(DollyHotel.Server.Models.ConData.HotelFacility item);

        public async Task<DollyHotel.Server.Models.ConData.HotelFacility> GetHotelFacilityByFacilityId(long facilityid)
        {
            var items = Context.HotelFacilities
                              .AsNoTracking()
                              .Where(i => i.FacilityID == facilityid);

            items = items.Include(i => i.Hotel);
  
            var itemToReturn = items.FirstOrDefault();

            OnHotelFacilityGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnHotelFacilityCreated(DollyHotel.Server.Models.ConData.HotelFacility item);
        partial void OnAfterHotelFacilityCreated(DollyHotel.Server.Models.ConData.HotelFacility item);

        public async Task<DollyHotel.Server.Models.ConData.HotelFacility> CreateHotelFacility(DollyHotel.Server.Models.ConData.HotelFacility hotelfacility)
        {
            OnHotelFacilityCreated(hotelfacility);

            var existingItem = Context.HotelFacilities
                              .Where(i => i.FacilityID == hotelfacility.FacilityID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.HotelFacilities.Add(hotelfacility);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(hotelfacility).State = EntityState.Detached;
                throw;
            }

            OnAfterHotelFacilityCreated(hotelfacility);

            return hotelfacility;
        }

        public async Task<DollyHotel.Server.Models.ConData.HotelFacility> CancelHotelFacilityChanges(DollyHotel.Server.Models.ConData.HotelFacility item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnHotelFacilityUpdated(DollyHotel.Server.Models.ConData.HotelFacility item);
        partial void OnAfterHotelFacilityUpdated(DollyHotel.Server.Models.ConData.HotelFacility item);

        public async Task<DollyHotel.Server.Models.ConData.HotelFacility> UpdateHotelFacility(long facilityid, DollyHotel.Server.Models.ConData.HotelFacility hotelfacility)
        {
            OnHotelFacilityUpdated(hotelfacility);

            var itemToUpdate = Context.HotelFacilities
                              .Where(i => i.FacilityID == hotelfacility.FacilityID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(hotelfacility);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterHotelFacilityUpdated(hotelfacility);

            return hotelfacility;
        }

        partial void OnHotelFacilityDeleted(DollyHotel.Server.Models.ConData.HotelFacility item);
        partial void OnAfterHotelFacilityDeleted(DollyHotel.Server.Models.ConData.HotelFacility item);

        public async Task<DollyHotel.Server.Models.ConData.HotelFacility> DeleteHotelFacility(long facilityid)
        {
            var itemToDelete = Context.HotelFacilities
                              .Where(i => i.FacilityID == facilityid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnHotelFacilityDeleted(itemToDelete);


            Context.HotelFacilities.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterHotelFacilityDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportHotelRoomsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotelrooms/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotelrooms/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportHotelRoomsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotelrooms/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotelrooms/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnHotelRoomsRead(ref IQueryable<DollyHotel.Server.Models.ConData.HotelRoom> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.HotelRoom>> GetHotelRooms(Query query = null)
        {
            var items = Context.HotelRooms.AsQueryable();

            items = items.Include(i => i.Hotel);
            items = items.Include(i => i.RoomStatus);
            items = items.Include(i => i.RoomType);

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

            OnHotelRoomsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnHotelRoomGet(DollyHotel.Server.Models.ConData.HotelRoom item);

        public async Task<DollyHotel.Server.Models.ConData.HotelRoom> GetHotelRoomByRoomId(long roomid)
        {
            var items = Context.HotelRooms
                              .AsNoTracking()
                              .Where(i => i.RoomID == roomid);

            items = items.Include(i => i.Hotel);
            items = items.Include(i => i.RoomStatus);
            items = items.Include(i => i.RoomType);
  
            var itemToReturn = items.FirstOrDefault();

            OnHotelRoomGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnHotelRoomCreated(DollyHotel.Server.Models.ConData.HotelRoom item);
        partial void OnAfterHotelRoomCreated(DollyHotel.Server.Models.ConData.HotelRoom item);

        public async Task<DollyHotel.Server.Models.ConData.HotelRoom> CreateHotelRoom(DollyHotel.Server.Models.ConData.HotelRoom hotelroom)
        {
            OnHotelRoomCreated(hotelroom);

            var existingItem = Context.HotelRooms
                              .Where(i => i.RoomID == hotelroom.RoomID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.HotelRooms.Add(hotelroom);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(hotelroom).State = EntityState.Detached;
                throw;
            }

            OnAfterHotelRoomCreated(hotelroom);

            return hotelroom;
        }

        public async Task<DollyHotel.Server.Models.ConData.HotelRoom> CancelHotelRoomChanges(DollyHotel.Server.Models.ConData.HotelRoom item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnHotelRoomUpdated(DollyHotel.Server.Models.ConData.HotelRoom item);
        partial void OnAfterHotelRoomUpdated(DollyHotel.Server.Models.ConData.HotelRoom item);

        public async Task<DollyHotel.Server.Models.ConData.HotelRoom> UpdateHotelRoom(long roomid, DollyHotel.Server.Models.ConData.HotelRoom hotelroom)
        {
            OnHotelRoomUpdated(hotelroom);

            var itemToUpdate = Context.HotelRooms
                              .Where(i => i.RoomID == hotelroom.RoomID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(hotelroom);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterHotelRoomUpdated(hotelroom);

            return hotelroom;
        }

        partial void OnHotelRoomDeleted(DollyHotel.Server.Models.ConData.HotelRoom item);
        partial void OnAfterHotelRoomDeleted(DollyHotel.Server.Models.ConData.HotelRoom item);

        public async Task<DollyHotel.Server.Models.ConData.HotelRoom> DeleteHotelRoom(long roomid)
        {
            var itemToDelete = Context.HotelRooms
                              .Where(i => i.RoomID == roomid)
                              .Include(i => i.RoomBookingDetails)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnHotelRoomDeleted(itemToDelete);


            Context.HotelRooms.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterHotelRoomDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportHotelsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotels/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotels/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportHotelsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotels/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotels/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnHotelsRead(ref IQueryable<DollyHotel.Server.Models.ConData.Hotel> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.Hotel>> GetHotels(Query query = null)
        {
            var items = Context.Hotels.AsQueryable();

            items = items.Include(i => i.HotelType);
            items = items.Include(i => i.City);

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

            OnHotelsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnHotelGet(DollyHotel.Server.Models.ConData.Hotel item);

        public async Task<DollyHotel.Server.Models.ConData.Hotel> GetHotelByHotelId(long hotelid)
        {
            var items = Context.Hotels
                              .AsNoTracking()
                              .Where(i => i.HotelID == hotelid);

            items = items.Include(i => i.HotelType);
            items = items.Include(i => i.City);
  
            var itemToReturn = items.FirstOrDefault();

            OnHotelGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnHotelCreated(DollyHotel.Server.Models.ConData.Hotel item);
        partial void OnAfterHotelCreated(DollyHotel.Server.Models.ConData.Hotel item);

        public async Task<DollyHotel.Server.Models.ConData.Hotel> CreateHotel(DollyHotel.Server.Models.ConData.Hotel hotel)
        {
            OnHotelCreated(hotel);

            var existingItem = Context.Hotels
                              .Where(i => i.HotelID == hotel.HotelID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Hotels.Add(hotel);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(hotel).State = EntityState.Detached;
                throw;
            }

            OnAfterHotelCreated(hotel);

            return hotel;
        }

        public async Task<DollyHotel.Server.Models.ConData.Hotel> CancelHotelChanges(DollyHotel.Server.Models.ConData.Hotel item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnHotelUpdated(DollyHotel.Server.Models.ConData.Hotel item);
        partial void OnAfterHotelUpdated(DollyHotel.Server.Models.ConData.Hotel item);

        public async Task<DollyHotel.Server.Models.ConData.Hotel> UpdateHotel(long hotelid, DollyHotel.Server.Models.ConData.Hotel hotel)
        {
            OnHotelUpdated(hotel);

            var itemToUpdate = Context.Hotels
                              .Where(i => i.HotelID == hotel.HotelID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(hotel);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterHotelUpdated(hotel);

            return hotel;
        }

        partial void OnHotelDeleted(DollyHotel.Server.Models.ConData.Hotel item);
        partial void OnAfterHotelDeleted(DollyHotel.Server.Models.ConData.Hotel item);

        public async Task<DollyHotel.Server.Models.ConData.Hotel> DeleteHotel(long hotelid)
        {
            var itemToDelete = Context.Hotels
                              .Where(i => i.HotelID == hotelid)
                              .Include(i => i.BedTypes)
                              .Include(i => i.HotelFacilities)
                              .Include(i => i.HotelRooms)
                              .Include(i => i.RoomBookings)
                              .Include(i => i.RoomTypeFacilities)
                              .Include(i => i.RoomTypes)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnHotelDeleted(itemToDelete);


            Context.Hotels.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterHotelDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportHotelTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hoteltypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hoteltypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportHotelTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hoteltypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hoteltypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnHotelTypesRead(ref IQueryable<DollyHotel.Server.Models.ConData.HotelType> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.HotelType>> GetHotelTypes(Query query = null)
        {
            var items = Context.HotelTypes.AsQueryable();


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

            OnHotelTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnHotelTypeGet(DollyHotel.Server.Models.ConData.HotelType item);

        public async Task<DollyHotel.Server.Models.ConData.HotelType> GetHotelTypeByHotelTypeId(int hoteltypeid)
        {
            var items = Context.HotelTypes
                              .AsNoTracking()
                              .Where(i => i.HotelTypeID == hoteltypeid);

  
            var itemToReturn = items.FirstOrDefault();

            OnHotelTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnHotelTypeCreated(DollyHotel.Server.Models.ConData.HotelType item);
        partial void OnAfterHotelTypeCreated(DollyHotel.Server.Models.ConData.HotelType item);

        public async Task<DollyHotel.Server.Models.ConData.HotelType> CreateHotelType(DollyHotel.Server.Models.ConData.HotelType hoteltype)
        {
            OnHotelTypeCreated(hoteltype);

            var existingItem = Context.HotelTypes
                              .Where(i => i.HotelTypeID == hoteltype.HotelTypeID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.HotelTypes.Add(hoteltype);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(hoteltype).State = EntityState.Detached;
                throw;
            }

            OnAfterHotelTypeCreated(hoteltype);

            return hoteltype;
        }

        public async Task<DollyHotel.Server.Models.ConData.HotelType> CancelHotelTypeChanges(DollyHotel.Server.Models.ConData.HotelType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnHotelTypeUpdated(DollyHotel.Server.Models.ConData.HotelType item);
        partial void OnAfterHotelTypeUpdated(DollyHotel.Server.Models.ConData.HotelType item);

        public async Task<DollyHotel.Server.Models.ConData.HotelType> UpdateHotelType(int hoteltypeid, DollyHotel.Server.Models.ConData.HotelType hoteltype)
        {
            OnHotelTypeUpdated(hoteltype);

            var itemToUpdate = Context.HotelTypes
                              .Where(i => i.HotelTypeID == hoteltype.HotelTypeID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(hoteltype);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterHotelTypeUpdated(hoteltype);

            return hoteltype;
        }

        partial void OnHotelTypeDeleted(DollyHotel.Server.Models.ConData.HotelType item);
        partial void OnAfterHotelTypeDeleted(DollyHotel.Server.Models.ConData.HotelType item);

        public async Task<DollyHotel.Server.Models.ConData.HotelType> DeleteHotelType(int hoteltypeid)
        {
            var itemToDelete = Context.HotelTypes
                              .Where(i => i.HotelTypeID == hoteltypeid)
                              .Include(i => i.Hotels)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnHotelTypeDeleted(itemToDelete);


            Context.HotelTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterHotelTypeDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportPaymentStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/paymentstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/paymentstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPaymentStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/paymentstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/paymentstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPaymentStatusesRead(ref IQueryable<DollyHotel.Server.Models.ConData.PaymentStatus> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.PaymentStatus>> GetPaymentStatuses(Query query = null)
        {
            var items = Context.PaymentStatuses.AsQueryable();


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

            OnPaymentStatusesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPaymentStatusGet(DollyHotel.Server.Models.ConData.PaymentStatus item);

        public async Task<DollyHotel.Server.Models.ConData.PaymentStatus> GetPaymentStatusByPaymentStatusId(int paymentstatusid)
        {
            var items = Context.PaymentStatuses
                              .AsNoTracking()
                              .Where(i => i.PaymentStatusID == paymentstatusid);

  
            var itemToReturn = items.FirstOrDefault();

            OnPaymentStatusGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnPaymentStatusCreated(DollyHotel.Server.Models.ConData.PaymentStatus item);
        partial void OnAfterPaymentStatusCreated(DollyHotel.Server.Models.ConData.PaymentStatus item);

        public async Task<DollyHotel.Server.Models.ConData.PaymentStatus> CreatePaymentStatus(DollyHotel.Server.Models.ConData.PaymentStatus paymentstatus)
        {
            OnPaymentStatusCreated(paymentstatus);

            var existingItem = Context.PaymentStatuses
                              .Where(i => i.PaymentStatusID == paymentstatus.PaymentStatusID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.PaymentStatuses.Add(paymentstatus);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(paymentstatus).State = EntityState.Detached;
                throw;
            }

            OnAfterPaymentStatusCreated(paymentstatus);

            return paymentstatus;
        }

        public async Task<DollyHotel.Server.Models.ConData.PaymentStatus> CancelPaymentStatusChanges(DollyHotel.Server.Models.ConData.PaymentStatus item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPaymentStatusUpdated(DollyHotel.Server.Models.ConData.PaymentStatus item);
        partial void OnAfterPaymentStatusUpdated(DollyHotel.Server.Models.ConData.PaymentStatus item);

        public async Task<DollyHotel.Server.Models.ConData.PaymentStatus> UpdatePaymentStatus(int paymentstatusid, DollyHotel.Server.Models.ConData.PaymentStatus paymentstatus)
        {
            OnPaymentStatusUpdated(paymentstatus);

            var itemToUpdate = Context.PaymentStatuses
                              .Where(i => i.PaymentStatusID == paymentstatus.PaymentStatusID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(paymentstatus);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterPaymentStatusUpdated(paymentstatus);

            return paymentstatus;
        }

        partial void OnPaymentStatusDeleted(DollyHotel.Server.Models.ConData.PaymentStatus item);
        partial void OnAfterPaymentStatusDeleted(DollyHotel.Server.Models.ConData.PaymentStatus item);

        public async Task<DollyHotel.Server.Models.ConData.PaymentStatus> DeletePaymentStatus(int paymentstatusid)
        {
            var itemToDelete = Context.PaymentStatuses
                              .Where(i => i.PaymentStatusID == paymentstatusid)
                              .Include(i => i.RoomBookings)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPaymentStatusDeleted(itemToDelete);


            Context.PaymentStatuses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPaymentStatusDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRoomBookingDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roombookingdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roombookingdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRoomBookingDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roombookingdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roombookingdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRoomBookingDetailsRead(ref IQueryable<DollyHotel.Server.Models.ConData.RoomBookingDetail> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.RoomBookingDetail>> GetRoomBookingDetails(Query query = null)
        {
            var items = Context.RoomBookingDetails.AsQueryable();

            items = items.Include(i => i.RoomBooking);
            items = items.Include(i => i.BookingStatus);
            items = items.Include(i => i.HotelRoom);

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

            OnRoomBookingDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRoomBookingDetailGet(DollyHotel.Server.Models.ConData.RoomBookingDetail item);

        public async Task<DollyHotel.Server.Models.ConData.RoomBookingDetail> GetRoomBookingDetailByBookingDetailsId(long bookingdetailsid)
        {
            var items = Context.RoomBookingDetails
                              .AsNoTracking()
                              .Where(i => i.BookingDetailsID == bookingdetailsid);

            items = items.Include(i => i.RoomBooking);
            items = items.Include(i => i.BookingStatus);
            items = items.Include(i => i.HotelRoom);
  
            var itemToReturn = items.FirstOrDefault();

            OnRoomBookingDetailGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRoomBookingDetailCreated(DollyHotel.Server.Models.ConData.RoomBookingDetail item);
        partial void OnAfterRoomBookingDetailCreated(DollyHotel.Server.Models.ConData.RoomBookingDetail item);

        public async Task<DollyHotel.Server.Models.ConData.RoomBookingDetail> CreateRoomBookingDetail(DollyHotel.Server.Models.ConData.RoomBookingDetail roombookingdetail)
        {
            OnRoomBookingDetailCreated(roombookingdetail);

            var existingItem = Context.RoomBookingDetails
                              .Where(i => i.BookingDetailsID == roombookingdetail.BookingDetailsID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RoomBookingDetails.Add(roombookingdetail);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(roombookingdetail).State = EntityState.Detached;
                throw;
            }

            OnAfterRoomBookingDetailCreated(roombookingdetail);

            return roombookingdetail;
        }

        public async Task<DollyHotel.Server.Models.ConData.RoomBookingDetail> CancelRoomBookingDetailChanges(DollyHotel.Server.Models.ConData.RoomBookingDetail item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRoomBookingDetailUpdated(DollyHotel.Server.Models.ConData.RoomBookingDetail item);
        partial void OnAfterRoomBookingDetailUpdated(DollyHotel.Server.Models.ConData.RoomBookingDetail item);

        public async Task<DollyHotel.Server.Models.ConData.RoomBookingDetail> UpdateRoomBookingDetail(long bookingdetailsid, DollyHotel.Server.Models.ConData.RoomBookingDetail roombookingdetail)
        {
            OnRoomBookingDetailUpdated(roombookingdetail);

            var itemToUpdate = Context.RoomBookingDetails
                              .Where(i => i.BookingDetailsID == roombookingdetail.BookingDetailsID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(roombookingdetail);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRoomBookingDetailUpdated(roombookingdetail);

            return roombookingdetail;
        }

        partial void OnRoomBookingDetailDeleted(DollyHotel.Server.Models.ConData.RoomBookingDetail item);
        partial void OnAfterRoomBookingDetailDeleted(DollyHotel.Server.Models.ConData.RoomBookingDetail item);

        public async Task<DollyHotel.Server.Models.ConData.RoomBookingDetail> DeleteRoomBookingDetail(long bookingdetailsid)
        {
            var itemToDelete = Context.RoomBookingDetails
                              .Where(i => i.BookingDetailsID == bookingdetailsid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRoomBookingDetailDeleted(itemToDelete);


            Context.RoomBookingDetails.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRoomBookingDetailDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRoomBookingsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roombookings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roombookings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRoomBookingsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roombookings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roombookings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRoomBookingsRead(ref IQueryable<DollyHotel.Server.Models.ConData.RoomBooking> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.RoomBooking>> GetRoomBookings(Query query = null)
        {
            var items = Context.RoomBookings.AsQueryable();

            items = items.Include(i => i.BookingStatus);
            items = items.Include(i => i.Hotel);
            items = items.Include(i => i.AspNetUser);
            items = items.Include(i => i.PaymentStatus);

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

            OnRoomBookingsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRoomBookingGet(DollyHotel.Server.Models.ConData.RoomBooking item);

        public async Task<DollyHotel.Server.Models.ConData.RoomBooking> GetRoomBookingByBookingId(long bookingid)
        {
            var items = Context.RoomBookings
                              .AsNoTracking()
                              .Where(i => i.BookingID == bookingid);

            items = items.Include(i => i.BookingStatus);
            items = items.Include(i => i.Hotel);
            items = items.Include(i => i.AspNetUser);
            items = items.Include(i => i.PaymentStatus);
  
            var itemToReturn = items.FirstOrDefault();

            OnRoomBookingGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRoomBookingCreated(DollyHotel.Server.Models.ConData.RoomBooking item);
        partial void OnAfterRoomBookingCreated(DollyHotel.Server.Models.ConData.RoomBooking item);

        public async Task<DollyHotel.Server.Models.ConData.RoomBooking> CreateRoomBooking(DollyHotel.Server.Models.ConData.RoomBooking roombooking)
        {
            OnRoomBookingCreated(roombooking);

            var existingItem = Context.RoomBookings
                              .Where(i => i.BookingID == roombooking.BookingID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RoomBookings.Add(roombooking);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(roombooking).State = EntityState.Detached;
                throw;
            }

            OnAfterRoomBookingCreated(roombooking);

            return roombooking;
        }

        public async Task<DollyHotel.Server.Models.ConData.RoomBooking> CancelRoomBookingChanges(DollyHotel.Server.Models.ConData.RoomBooking item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRoomBookingUpdated(DollyHotel.Server.Models.ConData.RoomBooking item);
        partial void OnAfterRoomBookingUpdated(DollyHotel.Server.Models.ConData.RoomBooking item);

        public async Task<DollyHotel.Server.Models.ConData.RoomBooking> UpdateRoomBooking(long bookingid, DollyHotel.Server.Models.ConData.RoomBooking roombooking)
        {
            OnRoomBookingUpdated(roombooking);

            var itemToUpdate = Context.RoomBookings
                              .Where(i => i.BookingID == roombooking.BookingID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(roombooking);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRoomBookingUpdated(roombooking);

            return roombooking;
        }

        partial void OnRoomBookingDeleted(DollyHotel.Server.Models.ConData.RoomBooking item);
        partial void OnAfterRoomBookingDeleted(DollyHotel.Server.Models.ConData.RoomBooking item);

        public async Task<DollyHotel.Server.Models.ConData.RoomBooking> DeleteRoomBooking(long bookingid)
        {
            var itemToDelete = Context.RoomBookings
                              .Where(i => i.BookingID == bookingid)
                              .Include(i => i.RoomBookingDetails)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRoomBookingDeleted(itemToDelete);


            Context.RoomBookings.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRoomBookingDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRoomStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRoomStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRoomStatusesRead(ref IQueryable<DollyHotel.Server.Models.ConData.RoomStatus> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.RoomStatus>> GetRoomStatuses(Query query = null)
        {
            var items = Context.RoomStatuses.AsQueryable();


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

            OnRoomStatusesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRoomStatusGet(DollyHotel.Server.Models.ConData.RoomStatus item);

        public async Task<DollyHotel.Server.Models.ConData.RoomStatus> GetRoomStatusByRoomStatusId(int roomstatusid)
        {
            var items = Context.RoomStatuses
                              .AsNoTracking()
                              .Where(i => i.RoomStatusID == roomstatusid);

  
            var itemToReturn = items.FirstOrDefault();

            OnRoomStatusGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRoomStatusCreated(DollyHotel.Server.Models.ConData.RoomStatus item);
        partial void OnAfterRoomStatusCreated(DollyHotel.Server.Models.ConData.RoomStatus item);

        public async Task<DollyHotel.Server.Models.ConData.RoomStatus> CreateRoomStatus(DollyHotel.Server.Models.ConData.RoomStatus roomstatus)
        {
            OnRoomStatusCreated(roomstatus);

            var existingItem = Context.RoomStatuses
                              .Where(i => i.RoomStatusID == roomstatus.RoomStatusID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RoomStatuses.Add(roomstatus);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(roomstatus).State = EntityState.Detached;
                throw;
            }

            OnAfterRoomStatusCreated(roomstatus);

            return roomstatus;
        }

        public async Task<DollyHotel.Server.Models.ConData.RoomStatus> CancelRoomStatusChanges(DollyHotel.Server.Models.ConData.RoomStatus item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRoomStatusUpdated(DollyHotel.Server.Models.ConData.RoomStatus item);
        partial void OnAfterRoomStatusUpdated(DollyHotel.Server.Models.ConData.RoomStatus item);

        public async Task<DollyHotel.Server.Models.ConData.RoomStatus> UpdateRoomStatus(int roomstatusid, DollyHotel.Server.Models.ConData.RoomStatus roomstatus)
        {
            OnRoomStatusUpdated(roomstatus);

            var itemToUpdate = Context.RoomStatuses
                              .Where(i => i.RoomStatusID == roomstatus.RoomStatusID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(roomstatus);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRoomStatusUpdated(roomstatus);

            return roomstatus;
        }

        partial void OnRoomStatusDeleted(DollyHotel.Server.Models.ConData.RoomStatus item);
        partial void OnAfterRoomStatusDeleted(DollyHotel.Server.Models.ConData.RoomStatus item);

        public async Task<DollyHotel.Server.Models.ConData.RoomStatus> DeleteRoomStatus(int roomstatusid)
        {
            var itemToDelete = Context.RoomStatuses
                              .Where(i => i.RoomStatusID == roomstatusid)
                              .Include(i => i.HotelRooms)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRoomStatusDeleted(itemToDelete);


            Context.RoomStatuses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRoomStatusDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRoomTypeFacilitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomtypefacilities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomtypefacilities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRoomTypeFacilitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomtypefacilities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomtypefacilities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRoomTypeFacilitiesRead(ref IQueryable<DollyHotel.Server.Models.ConData.RoomTypeFacility> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.RoomTypeFacility>> GetRoomTypeFacilities(Query query = null)
        {
            var items = Context.RoomTypeFacilities.AsQueryable();

            items = items.Include(i => i.Hotel);
            items = items.Include(i => i.RoomType);

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

            OnRoomTypeFacilitiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRoomTypeFacilityGet(DollyHotel.Server.Models.ConData.RoomTypeFacility item);

        public async Task<DollyHotel.Server.Models.ConData.RoomTypeFacility> GetRoomTypeFacilityByRoomTypeFacilityId(long roomtypefacilityid)
        {
            var items = Context.RoomTypeFacilities
                              .AsNoTracking()
                              .Where(i => i.RoomTypeFacilityID == roomtypefacilityid);

            items = items.Include(i => i.Hotel);
            items = items.Include(i => i.RoomType);
  
            var itemToReturn = items.FirstOrDefault();

            OnRoomTypeFacilityGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRoomTypeFacilityCreated(DollyHotel.Server.Models.ConData.RoomTypeFacility item);
        partial void OnAfterRoomTypeFacilityCreated(DollyHotel.Server.Models.ConData.RoomTypeFacility item);

        public async Task<DollyHotel.Server.Models.ConData.RoomTypeFacility> CreateRoomTypeFacility(DollyHotel.Server.Models.ConData.RoomTypeFacility roomtypefacility)
        {
            OnRoomTypeFacilityCreated(roomtypefacility);

            var existingItem = Context.RoomTypeFacilities
                              .Where(i => i.RoomTypeFacilityID == roomtypefacility.RoomTypeFacilityID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RoomTypeFacilities.Add(roomtypefacility);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(roomtypefacility).State = EntityState.Detached;
                throw;
            }

            OnAfterRoomTypeFacilityCreated(roomtypefacility);

            return roomtypefacility;
        }

        public async Task<DollyHotel.Server.Models.ConData.RoomTypeFacility> CancelRoomTypeFacilityChanges(DollyHotel.Server.Models.ConData.RoomTypeFacility item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRoomTypeFacilityUpdated(DollyHotel.Server.Models.ConData.RoomTypeFacility item);
        partial void OnAfterRoomTypeFacilityUpdated(DollyHotel.Server.Models.ConData.RoomTypeFacility item);

        public async Task<DollyHotel.Server.Models.ConData.RoomTypeFacility> UpdateRoomTypeFacility(long roomtypefacilityid, DollyHotel.Server.Models.ConData.RoomTypeFacility roomtypefacility)
        {
            OnRoomTypeFacilityUpdated(roomtypefacility);

            var itemToUpdate = Context.RoomTypeFacilities
                              .Where(i => i.RoomTypeFacilityID == roomtypefacility.RoomTypeFacilityID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(roomtypefacility);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRoomTypeFacilityUpdated(roomtypefacility);

            return roomtypefacility;
        }

        partial void OnRoomTypeFacilityDeleted(DollyHotel.Server.Models.ConData.RoomTypeFacility item);
        partial void OnAfterRoomTypeFacilityDeleted(DollyHotel.Server.Models.ConData.RoomTypeFacility item);

        public async Task<DollyHotel.Server.Models.ConData.RoomTypeFacility> DeleteRoomTypeFacility(long roomtypefacilityid)
        {
            var itemToDelete = Context.RoomTypeFacilities
                              .Where(i => i.RoomTypeFacilityID == roomtypefacilityid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRoomTypeFacilityDeleted(itemToDelete);


            Context.RoomTypeFacilities.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRoomTypeFacilityDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRoomTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomtypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomtypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRoomTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomtypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomtypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRoomTypesRead(ref IQueryable<DollyHotel.Server.Models.ConData.RoomType> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.RoomType>> GetRoomTypes(Query query = null)
        {
            var items = Context.RoomTypes.AsQueryable();

            items = items.Include(i => i.BedType);
            items = items.Include(i => i.Hotel);

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

            OnRoomTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRoomTypeGet(DollyHotel.Server.Models.ConData.RoomType item);

        public async Task<DollyHotel.Server.Models.ConData.RoomType> GetRoomTypeByRoomTypeId(long roomtypeid)
        {
            var items = Context.RoomTypes
                              .AsNoTracking()
                              .Where(i => i.RoomTypeID == roomtypeid);

            items = items.Include(i => i.BedType);
            items = items.Include(i => i.Hotel);
  
            var itemToReturn = items.FirstOrDefault();

            OnRoomTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRoomTypeCreated(DollyHotel.Server.Models.ConData.RoomType item);
        partial void OnAfterRoomTypeCreated(DollyHotel.Server.Models.ConData.RoomType item);

        public async Task<DollyHotel.Server.Models.ConData.RoomType> CreateRoomType(DollyHotel.Server.Models.ConData.RoomType roomtype)
        {
            OnRoomTypeCreated(roomtype);

            var existingItem = Context.RoomTypes
                              .Where(i => i.RoomTypeID == roomtype.RoomTypeID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RoomTypes.Add(roomtype);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(roomtype).State = EntityState.Detached;
                throw;
            }

            OnAfterRoomTypeCreated(roomtype);

            return roomtype;
        }

        public async Task<DollyHotel.Server.Models.ConData.RoomType> CancelRoomTypeChanges(DollyHotel.Server.Models.ConData.RoomType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRoomTypeUpdated(DollyHotel.Server.Models.ConData.RoomType item);
        partial void OnAfterRoomTypeUpdated(DollyHotel.Server.Models.ConData.RoomType item);

        public async Task<DollyHotel.Server.Models.ConData.RoomType> UpdateRoomType(long roomtypeid, DollyHotel.Server.Models.ConData.RoomType roomtype)
        {
            OnRoomTypeUpdated(roomtype);

            var itemToUpdate = Context.RoomTypes
                              .Where(i => i.RoomTypeID == roomtype.RoomTypeID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(roomtype);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRoomTypeUpdated(roomtype);

            return roomtype;
        }

        partial void OnRoomTypeDeleted(DollyHotel.Server.Models.ConData.RoomType item);
        partial void OnAfterRoomTypeDeleted(DollyHotel.Server.Models.ConData.RoomType item);

        public async Task<DollyHotel.Server.Models.ConData.RoomType> DeleteRoomType(long roomtypeid)
        {
            var itemToDelete = Context.RoomTypes
                              .Where(i => i.RoomTypeID == roomtypeid)
                              .Include(i => i.HotelRooms)
                              .Include(i => i.RoomTypeFacilities)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRoomTypeDeleted(itemToDelete);


            Context.RoomTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRoomTypeDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSearchesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/searches/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/searches/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSearchesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/searches/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/searches/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSearchesRead(ref IQueryable<DollyHotel.Server.Models.ConData.Search> items);

        public async Task<IQueryable<DollyHotel.Server.Models.ConData.Search>> GetSearches(Query query = null)
        {
            var items = Context.Searches.AsQueryable();

            items = items.Include(i => i.City);
            items = items.Include(i => i.AspNetUser);

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

            OnSearchesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSearchGet(DollyHotel.Server.Models.ConData.Search item);

        public async Task<DollyHotel.Server.Models.ConData.Search> GetSearchBySearchId(long searchid)
        {
            var items = Context.Searches
                              .AsNoTracking()
                              .Where(i => i.SearchID == searchid);

            items = items.Include(i => i.City);
            items = items.Include(i => i.AspNetUser);
  
            var itemToReturn = items.FirstOrDefault();

            OnSearchGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSearchCreated(DollyHotel.Server.Models.ConData.Search item);
        partial void OnAfterSearchCreated(DollyHotel.Server.Models.ConData.Search item);

        public async Task<DollyHotel.Server.Models.ConData.Search> CreateSearch(DollyHotel.Server.Models.ConData.Search search)
        {
            OnSearchCreated(search);

            var existingItem = Context.Searches
                              .Where(i => i.SearchID == search.SearchID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Searches.Add(search);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(search).State = EntityState.Detached;
                throw;
            }

            OnAfterSearchCreated(search);

            return search;
        }

        public async Task<DollyHotel.Server.Models.ConData.Search> CancelSearchChanges(DollyHotel.Server.Models.ConData.Search item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSearchUpdated(DollyHotel.Server.Models.ConData.Search item);
        partial void OnAfterSearchUpdated(DollyHotel.Server.Models.ConData.Search item);

        public async Task<DollyHotel.Server.Models.ConData.Search> UpdateSearch(long searchid, DollyHotel.Server.Models.ConData.Search search)
        {
            OnSearchUpdated(search);

            var itemToUpdate = Context.Searches
                              .Where(i => i.SearchID == search.SearchID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(search);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSearchUpdated(search);

            return search;
        }

        partial void OnSearchDeleted(DollyHotel.Server.Models.ConData.Search item);
        partial void OnAfterSearchDeleted(DollyHotel.Server.Models.ConData.Search item);

        public async Task<DollyHotel.Server.Models.ConData.Search> DeleteSearch(long searchid)
        {
            var itemToDelete = Context.Searches
                              .Where(i => i.SearchID == searchid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSearchDeleted(itemToDelete);


            Context.Searches.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSearchDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}