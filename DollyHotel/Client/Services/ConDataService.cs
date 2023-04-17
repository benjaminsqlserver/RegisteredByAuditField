
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Radzen;

namespace DollyHotel.Client
{
    public partial class ConDataService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public ConDataService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/ConData/");
        }


        public async System.Threading.Tasks.Task ExportAspNetRoleClaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetRoleClaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetRoleClaims(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetRoleClaim>> GetAspNetRoleClaims(Query query)
        {
            return await GetAspNetRoleClaims(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetRoleClaim>> GetAspNetRoleClaims(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetRoleClaims");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetRoleClaims(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetRoleClaim>>(response);
        }

        partial void OnCreateAspNetRoleClaim(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRoleClaim> CreateAspNetRoleClaim(DollyHotel.Server.Models.ConData.AspNetRoleClaim aspNetRoleClaim = default(DollyHotel.Server.Models.ConData.AspNetRoleClaim))
        {
            var uri = new Uri(baseUri, $"AspNetRoleClaims");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetRoleClaim), Encoding.UTF8, "application/json");

            OnCreateAspNetRoleClaim(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetRoleClaim>(response);
        }

        partial void OnDeleteAspNetRoleClaim(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetRoleClaim(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AspNetRoleClaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetRoleClaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetRoleClaimById(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRoleClaim> GetAspNetRoleClaimById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AspNetRoleClaims({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetRoleClaimById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetRoleClaim>(response);
        }

        partial void OnUpdateAspNetRoleClaim(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspNetRoleClaim(int id = default(int), DollyHotel.Server.Models.ConData.AspNetRoleClaim aspNetRoleClaim = default(DollyHotel.Server.Models.ConData.AspNetRoleClaim))
        {
            var uri = new Uri(baseUri, $"AspNetRoleClaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetRoleClaim), Encoding.UTF8, "application/json");

            OnUpdateAspNetRoleClaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetRoles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetRole>> GetAspNetRoles(Query query)
        {
            return await GetAspNetRoles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetRole>> GetAspNetRoles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetRoles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetRoles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetRole>>(response);
        }

        partial void OnCreateAspNetRole(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRole> CreateAspNetRole(DollyHotel.Server.Models.ConData.AspNetRole aspNetRole = default(DollyHotel.Server.Models.ConData.AspNetRole))
        {
            var uri = new Uri(baseUri, $"AspNetRoles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetRole), Encoding.UTF8, "application/json");

            OnCreateAspNetRole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetRole>(response);
        }

        partial void OnDeleteAspNetRole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetRole(string id = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetRoles('{HttpUtility.UrlEncode(id.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetRoleById(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetRole> GetAspNetRoleById(string expand = default(string), string id = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetRoles('{HttpUtility.UrlEncode(id.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetRoleById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetRole>(response);
        }

        partial void OnUpdateAspNetRole(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspNetRole(string id = default(string), DollyHotel.Server.Models.ConData.AspNetRole aspNetRole = default(DollyHotel.Server.Models.ConData.AspNetRole))
        {
            var uri = new Uri(baseUri, $"AspNetRoles('{HttpUtility.UrlEncode(id.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

        

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetRole), Encoding.UTF8, "application/json");

            OnUpdateAspNetRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserClaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserClaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetUserClaims(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserClaim>> GetAspNetUserClaims(Query query)
        {
            return await GetAspNetUserClaims(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserClaim>> GetAspNetUserClaims(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserClaims");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserClaims(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserClaim>>(response);
        }

        partial void OnCreateAspNetUserClaim(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserClaim> CreateAspNetUserClaim(DollyHotel.Server.Models.ConData.AspNetUserClaim aspNetUserClaim = default(DollyHotel.Server.Models.ConData.AspNetUserClaim))
        {
            var uri = new Uri(baseUri, $"AspNetUserClaims");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserClaim), Encoding.UTF8, "application/json");

            OnCreateAspNetUserClaim(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetUserClaim>(response);
        }

        partial void OnDeleteAspNetUserClaim(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetUserClaim(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AspNetUserClaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetUserClaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetUserClaimById(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserClaim> GetAspNetUserClaimById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AspNetUserClaims({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserClaimById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetUserClaim>(response);
        }

        partial void OnUpdateAspNetUserClaim(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspNetUserClaim(int id = default(int), DollyHotel.Server.Models.ConData.AspNetUserClaim aspNetUserClaim = default(DollyHotel.Server.Models.ConData.AspNetUserClaim))
        {
            var uri = new Uri(baseUri, $"AspNetUserClaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

        

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserClaim), Encoding.UTF8, "application/json");

            OnUpdateAspNetUserClaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserLoginsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserLoginsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetUserLogins(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserLogin>> GetAspNetUserLogins(Query query)
        {
            return await GetAspNetUserLogins(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserLogin>> GetAspNetUserLogins(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserLogins");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserLogins(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserLogin>>(response);
        }

        partial void OnCreateAspNetUserLogin(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserLogin> CreateAspNetUserLogin(DollyHotel.Server.Models.ConData.AspNetUserLogin aspNetUserLogin = default(DollyHotel.Server.Models.ConData.AspNetUserLogin))
        {
            var uri = new Uri(baseUri, $"AspNetUserLogins");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserLogin), Encoding.UTF8, "application/json");

            OnCreateAspNetUserLogin(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetUserLogin>(response);
        }

        partial void OnDeleteAspNetUserLogin(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetUserLogin(string loginProvider = default(string), string providerKey = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserLogins(LoginProvider='{HttpUtility.UrlEncode(loginProvider.Trim().Replace("'", "''").Replace(" ","%20"))}',ProviderKey='{HttpUtility.UrlEncode(providerKey.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetUserLogin(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetUserLoginByLoginProviderAndProviderKey(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserLogin> GetAspNetUserLoginByLoginProviderAndProviderKey(string expand = default(string), string loginProvider = default(string), string providerKey = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserLogins(LoginProvider='{HttpUtility.UrlEncode(loginProvider.Trim().Replace("'", "''").Replace(" ","%20"))}',ProviderKey='{HttpUtility.UrlEncode(providerKey.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserLoginByLoginProviderAndProviderKey(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetUserLogin>(response);
        }

        partial void OnUpdateAspNetUserLogin(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspNetUserLogin(string loginProvider = default(string), string providerKey = default(string), DollyHotel.Server.Models.ConData.AspNetUserLogin aspNetUserLogin = default(DollyHotel.Server.Models.ConData.AspNetUserLogin))
        {
            var uri = new Uri(baseUri, $"AspNetUserLogins(LoginProvider='{HttpUtility.UrlEncode(loginProvider.Trim().Replace("'", "''").Replace(" ","%20"))}',ProviderKey='{HttpUtility.UrlEncode(providerKey.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

           

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserLogin), Encoding.UTF8, "application/json");

            OnUpdateAspNetUserLogin(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetUserRoles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserRole>> GetAspNetUserRoles(Query query)
        {
            return await GetAspNetUserRoles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserRole>> GetAspNetUserRoles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserRoles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserRoles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserRole>>(response);
        }

        partial void OnCreateAspNetUserRole(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserRole> CreateAspNetUserRole(DollyHotel.Server.Models.ConData.AspNetUserRole aspNetUserRole = default(DollyHotel.Server.Models.ConData.AspNetUserRole))
        {
            var uri = new Uri(baseUri, $"AspNetUserRoles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserRole), Encoding.UTF8, "application/json");

            OnCreateAspNetUserRole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetUserRole>(response);
        }

        partial void OnDeleteAspNetUserRole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetUserRole(string userId = default(string), string roleId = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserRoles(UserId='{HttpUtility.UrlEncode(userId.Trim().Replace("'", "''").Replace(" ","%20"))}',RoleId='{HttpUtility.UrlEncode(roleId.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetUserRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetUserRoleByUserIdAndRoleId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserRole> GetAspNetUserRoleByUserIdAndRoleId(string expand = default(string), string userId = default(string), string roleId = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserRoles(UserId='{HttpUtility.UrlEncode(userId.Trim().Replace("'", "''").Replace(" ","%20"))}',RoleId='{HttpUtility.UrlEncode(roleId.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserRoleByUserIdAndRoleId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetUserRole>(response);
        }

        partial void OnUpdateAspNetUserRole(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspNetUserRole(string userId = default(string), string roleId = default(string), DollyHotel.Server.Models.ConData.AspNetUserRole aspNetUserRole = default(DollyHotel.Server.Models.ConData.AspNetUserRole))
        {
            var uri = new Uri(baseUri, $"AspNetUserRoles(UserId='{HttpUtility.UrlEncode(userId.Trim().Replace("'", "''").Replace(" ","%20"))}',RoleId='{HttpUtility.UrlEncode(roleId.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

      

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserRole), Encoding.UTF8, "application/json");

            OnUpdateAspNetUserRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetUsers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUser>> GetAspNetUsers(Query query)
        {
            return await GetAspNetUsers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUser>> GetAspNetUsers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUsers");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUsers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUser>>(response);
        }

        partial void OnCreateAspNetUser(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUser> CreateAspNetUser(DollyHotel.Server.Models.ConData.AspNetUser aspNetUser = default(DollyHotel.Server.Models.ConData.AspNetUser))
        {
            var uri = new Uri(baseUri, $"AspNetUsers");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUser), Encoding.UTF8, "application/json");

            OnCreateAspNetUser(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetUser>(response);
        }

        partial void OnDeleteAspNetUser(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetUser(string id = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUsers('{HttpUtility.UrlEncode(id.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetUserById(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUser> GetAspNetUserById(string expand = default(string), string id = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUsers('{HttpUtility.UrlEncode(id.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetUser>(response);
        }

        partial void OnUpdateAspNetUser(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspNetUser(string id = default(string), DollyHotel.Server.Models.ConData.AspNetUser aspNetUser = default(DollyHotel.Server.Models.ConData.AspNetUser))
        {
            var uri = new Uri(baseUri, $"AspNetUsers('{HttpUtility.UrlEncode(id.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

        

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUser), Encoding.UTF8, "application/json");

            OnUpdateAspNetUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserTokensToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspNetUserTokensToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspNetUserTokens(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserToken>> GetAspNetUserTokens(Query query)
        {
            return await GetAspNetUserTokens(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserToken>> GetAspNetUserTokens(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserTokens");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserTokens(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.AspNetUserToken>>(response);
        }

        partial void OnCreateAspNetUserToken(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserToken> CreateAspNetUserToken(DollyHotel.Server.Models.ConData.AspNetUserToken aspNetUserToken = default(DollyHotel.Server.Models.ConData.AspNetUserToken))
        {
            var uri = new Uri(baseUri, $"AspNetUserTokens");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserToken), Encoding.UTF8, "application/json");

            OnCreateAspNetUserToken(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetUserToken>(response);
        }

        partial void OnDeleteAspNetUserToken(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspNetUserToken(string userId = default(string), string loginProvider = default(string), string name = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserTokens(UserId='{HttpUtility.UrlEncode(userId.Trim().Replace("'", "''").Replace(" ","%20"))}',LoginProvider='{HttpUtility.UrlEncode(loginProvider.Trim().Replace("'", "''").Replace(" ","%20"))}',Name='{HttpUtility.UrlEncode(name.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspNetUserToken(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspNetUserTokenByUserIdAndLoginProviderAndName(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.AspNetUserToken> GetAspNetUserTokenByUserIdAndLoginProviderAndName(string expand = default(string), string userId = default(string), string loginProvider = default(string), string name = default(string))
        {
            var uri = new Uri(baseUri, $"AspNetUserTokens(UserId='{HttpUtility.UrlEncode(userId.Trim().Replace("'", "''").Replace(" ","%20"))}',LoginProvider='{HttpUtility.UrlEncode(loginProvider.Trim().Replace("'", "''").Replace(" ","%20"))}',Name='{HttpUtility.UrlEncode(name.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspNetUserTokenByUserIdAndLoginProviderAndName(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.AspNetUserToken>(response);
        }

        partial void OnUpdateAspNetUserToken(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspNetUserToken(string userId = default(string), string loginProvider = default(string), string name = default(string), DollyHotel.Server.Models.ConData.AspNetUserToken aspNetUserToken = default(DollyHotel.Server.Models.ConData.AspNetUserToken))
        {
            var uri = new Uri(baseUri, $"AspNetUserTokens(UserId='{HttpUtility.UrlEncode(userId.Trim().Replace("'", "''").Replace(" ","%20"))}',LoginProvider='{HttpUtility.UrlEncode(loginProvider.Trim().Replace("'", "''").Replace(" ","%20"))}',Name='{HttpUtility.UrlEncode(name.Trim().Replace("'", "''").Replace(" ","%20"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspNetUserToken), Encoding.UTF8, "application/json");

            OnUpdateAspNetUserToken(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportBedTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/bedtypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/bedtypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBedTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/bedtypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/bedtypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBedTypes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.BedType>> GetBedTypes(Query query)
        {
            return await GetBedTypes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.BedType>> GetBedTypes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"BedTypes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBedTypes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.BedType>>(response);
        }

        partial void OnCreateBedType(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.BedType> CreateBedType(DollyHotel.Server.Models.ConData.BedType bedType = default(DollyHotel.Server.Models.ConData.BedType))
        {
            var uri = new Uri(baseUri, $"BedTypes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bedType), Encoding.UTF8, "application/json");

            OnCreateBedType(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.BedType>(response);
        }

        partial void OnDeleteBedType(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBedType(long bedTypeId = default(long))
        {
            var uri = new Uri(baseUri, $"BedTypes({bedTypeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBedType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBedTypeByBedTypeId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.BedType> GetBedTypeByBedTypeId(string expand = default(string), long bedTypeId = default(long))
        {
            var uri = new Uri(baseUri, $"BedTypes({bedTypeId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBedTypeByBedTypeId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.BedType>(response);
        }

        partial void OnUpdateBedType(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBedType(long bedTypeId = default(long), DollyHotel.Server.Models.ConData.BedType bedType = default(DollyHotel.Server.Models.ConData.BedType))
        {
            var uri = new Uri(baseUri, $"BedTypes({bedTypeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

          

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bedType), Encoding.UTF8, "application/json");

            OnUpdateBedType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportBookingStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/bookingstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/bookingstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBookingStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/bookingstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/bookingstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBookingStatuses(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.BookingStatus>> GetBookingStatuses(Query query)
        {
            return await GetBookingStatuses(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.BookingStatus>> GetBookingStatuses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"BookingStatuses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBookingStatuses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.BookingStatus>>(response);
        }

        partial void OnCreateBookingStatus(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.BookingStatus> CreateBookingStatus(DollyHotel.Server.Models.ConData.BookingStatus bookingStatus = default(DollyHotel.Server.Models.ConData.BookingStatus))
        {
            var uri = new Uri(baseUri, $"BookingStatuses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bookingStatus), Encoding.UTF8, "application/json");

            OnCreateBookingStatus(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.BookingStatus>(response);
        }

        partial void OnDeleteBookingStatus(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBookingStatus(int bookingStatusId = default(int))
        {
            var uri = new Uri(baseUri, $"BookingStatuses({bookingStatusId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBookingStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBookingStatusByBookingStatusId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.BookingStatus> GetBookingStatusByBookingStatusId(string expand = default(string), int bookingStatusId = default(int))
        {
            var uri = new Uri(baseUri, $"BookingStatuses({bookingStatusId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBookingStatusByBookingStatusId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.BookingStatus>(response);
        }

        partial void OnUpdateBookingStatus(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBookingStatus(int bookingStatusId = default(int), DollyHotel.Server.Models.ConData.BookingStatus bookingStatus = default(DollyHotel.Server.Models.ConData.BookingStatus))
        {
            var uri = new Uri(baseUri, $"BookingStatuses({bookingStatusId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

          

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(bookingStatus), Encoding.UTF8, "application/json");

            OnUpdateBookingStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportCitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/cities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/cities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/cities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/cities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetCities(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.City>> GetCities(Query query)
        {
            return await GetCities(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.City>> GetCities(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Cities");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCities(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.City>>(response);
        }

        partial void OnCreateCity(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.City> CreateCity(DollyHotel.Server.Models.ConData.City city = default(DollyHotel.Server.Models.ConData.City))
        {
            var uri = new Uri(baseUri, $"Cities");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(city), Encoding.UTF8, "application/json");

            OnCreateCity(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.City>(response);
        }

        partial void OnDeleteCity(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteCity(long cityId = default(long))
        {
            var uri = new Uri(baseUri, $"Cities({cityId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetCityByCityId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.City> GetCityByCityId(string expand = default(string), long cityId = default(long))
        {
            var uri = new Uri(baseUri, $"Cities({cityId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCityByCityId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.City>(response);
        }

        partial void OnUpdateCity(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateCity(long cityId = default(long), DollyHotel.Server.Models.ConData.City city = default(DollyHotel.Server.Models.ConData.City))
        {
            var uri = new Uri(baseUri, $"Cities({cityId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

           

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(city), Encoding.UTF8, "application/json");

            OnUpdateCity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportHotelFacilitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotelfacilities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotelfacilities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportHotelFacilitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotelfacilities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotelfacilities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetHotelFacilities(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.HotelFacility>> GetHotelFacilities(Query query)
        {
            return await GetHotelFacilities(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.HotelFacility>> GetHotelFacilities(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"HotelFacilities");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetHotelFacilities(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.HotelFacility>>(response);
        }

        partial void OnCreateHotelFacility(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.HotelFacility> CreateHotelFacility(DollyHotel.Server.Models.ConData.HotelFacility hotelFacility = default(DollyHotel.Server.Models.ConData.HotelFacility))
        {
            var uri = new Uri(baseUri, $"HotelFacilities");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(hotelFacility), Encoding.UTF8, "application/json");

            OnCreateHotelFacility(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.HotelFacility>(response);
        }

        partial void OnDeleteHotelFacility(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteHotelFacility(long facilityId = default(long))
        {
            var uri = new Uri(baseUri, $"HotelFacilities({facilityId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteHotelFacility(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetHotelFacilityByFacilityId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.HotelFacility> GetHotelFacilityByFacilityId(string expand = default(string), long facilityId = default(long))
        {
            var uri = new Uri(baseUri, $"HotelFacilities({facilityId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetHotelFacilityByFacilityId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.HotelFacility>(response);
        }

        partial void OnUpdateHotelFacility(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateHotelFacility(long facilityId = default(long), DollyHotel.Server.Models.ConData.HotelFacility hotelFacility = default(DollyHotel.Server.Models.ConData.HotelFacility))
        {
            var uri = new Uri(baseUri, $"HotelFacilities({facilityId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

           

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(hotelFacility), Encoding.UTF8, "application/json");

            OnUpdateHotelFacility(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportHotelRoomsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotelrooms/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotelrooms/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportHotelRoomsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotelrooms/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotelrooms/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetHotelRooms(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.HotelRoom>> GetHotelRooms(Query query)
        {
            return await GetHotelRooms(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.HotelRoom>> GetHotelRooms(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"HotelRooms");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetHotelRooms(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.HotelRoom>>(response);
        }

        partial void OnCreateHotelRoom(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.HotelRoom> CreateHotelRoom(DollyHotel.Server.Models.ConData.HotelRoom hotelRoom = default(DollyHotel.Server.Models.ConData.HotelRoom))
        {
            var uri = new Uri(baseUri, $"HotelRooms");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(hotelRoom), Encoding.UTF8, "application/json");

            OnCreateHotelRoom(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.HotelRoom>(response);
        }

        partial void OnDeleteHotelRoom(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteHotelRoom(long roomId = default(long))
        {
            var uri = new Uri(baseUri, $"HotelRooms({roomId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteHotelRoom(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetHotelRoomByRoomId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.HotelRoom> GetHotelRoomByRoomId(string expand = default(string), long roomId = default(long))
        {
            var uri = new Uri(baseUri, $"HotelRooms({roomId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetHotelRoomByRoomId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.HotelRoom>(response);
        }

        partial void OnUpdateHotelRoom(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateHotelRoom(long roomId = default(long), DollyHotel.Server.Models.ConData.HotelRoom hotelRoom = default(DollyHotel.Server.Models.ConData.HotelRoom))
        {
            var uri = new Uri(baseUri, $"HotelRooms({roomId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

         

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(hotelRoom), Encoding.UTF8, "application/json");

            OnUpdateHotelRoom(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportHotelsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotels/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotels/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportHotelsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hotels/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hotels/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetHotels(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.Hotel>> GetHotels(Query query)
        {
            return await GetHotels(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.Hotel>> GetHotels(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Hotels");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetHotels(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.Hotel>>(response);
        }

        partial void OnCreateHotel(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.Hotel> CreateHotel(DollyHotel.Server.Models.ConData.Hotel hotel = default(DollyHotel.Server.Models.ConData.Hotel))
        {
            var uri = new Uri(baseUri, $"Hotels");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(hotel), Encoding.UTF8, "application/json");

            OnCreateHotel(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.Hotel>(response);
        }

        partial void OnDeleteHotel(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteHotel(long hotelId = default(long))
        {
            var uri = new Uri(baseUri, $"Hotels({hotelId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteHotel(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetHotelByHotelId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.Hotel> GetHotelByHotelId(string expand = default(string), long hotelId = default(long))
        {
            var uri = new Uri(baseUri, $"Hotels({hotelId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetHotelByHotelId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.Hotel>(response);
        }

        partial void OnUpdateHotel(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateHotel(long hotelId = default(long), DollyHotel.Server.Models.ConData.Hotel hotel = default(DollyHotel.Server.Models.ConData.Hotel))
        {
            var uri = new Uri(baseUri, $"Hotels({hotelId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

             

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(hotel), Encoding.UTF8, "application/json");

            OnUpdateHotel(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportHotelTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hoteltypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hoteltypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportHotelTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/hoteltypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/hoteltypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetHotelTypes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.HotelType>> GetHotelTypes(Query query)
        {
            return await GetHotelTypes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.HotelType>> GetHotelTypes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"HotelTypes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetHotelTypes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.HotelType>>(response);
        }

        partial void OnCreateHotelType(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.HotelType> CreateHotelType(DollyHotel.Server.Models.ConData.HotelType hotelType = default(DollyHotel.Server.Models.ConData.HotelType))
        {
            var uri = new Uri(baseUri, $"HotelTypes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(hotelType), Encoding.UTF8, "application/json");

            OnCreateHotelType(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.HotelType>(response);
        }

        partial void OnDeleteHotelType(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteHotelType(int hotelTypeId = default(int))
        {
            var uri = new Uri(baseUri, $"HotelTypes({hotelTypeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteHotelType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetHotelTypeByHotelTypeId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.HotelType> GetHotelTypeByHotelTypeId(string expand = default(string), int hotelTypeId = default(int))
        {
            var uri = new Uri(baseUri, $"HotelTypes({hotelTypeId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetHotelTypeByHotelTypeId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.HotelType>(response);
        }

        partial void OnUpdateHotelType(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateHotelType(int hotelTypeId = default(int), DollyHotel.Server.Models.ConData.HotelType hotelType = default(DollyHotel.Server.Models.ConData.HotelType))
        {
            var uri = new Uri(baseUri, $"HotelTypes({hotelTypeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

        

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(hotelType), Encoding.UTF8, "application/json");

            OnUpdateHotelType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportPaymentStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/paymentstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/paymentstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportPaymentStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/paymentstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/paymentstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetPaymentStatuses(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.PaymentStatus>> GetPaymentStatuses(Query query)
        {
            return await GetPaymentStatuses(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.PaymentStatus>> GetPaymentStatuses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"PaymentStatuses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPaymentStatuses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.PaymentStatus>>(response);
        }

        partial void OnCreatePaymentStatus(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.PaymentStatus> CreatePaymentStatus(DollyHotel.Server.Models.ConData.PaymentStatus paymentStatus = default(DollyHotel.Server.Models.ConData.PaymentStatus))
        {
            var uri = new Uri(baseUri, $"PaymentStatuses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(paymentStatus), Encoding.UTF8, "application/json");

            OnCreatePaymentStatus(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.PaymentStatus>(response);
        }

        partial void OnDeletePaymentStatus(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeletePaymentStatus(int paymentStatusId = default(int))
        {
            var uri = new Uri(baseUri, $"PaymentStatuses({paymentStatusId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeletePaymentStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetPaymentStatusByPaymentStatusId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.PaymentStatus> GetPaymentStatusByPaymentStatusId(string expand = default(string), int paymentStatusId = default(int))
        {
            var uri = new Uri(baseUri, $"PaymentStatuses({paymentStatusId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPaymentStatusByPaymentStatusId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.PaymentStatus>(response);
        }

        partial void OnUpdatePaymentStatus(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdatePaymentStatus(int paymentStatusId = default(int), DollyHotel.Server.Models.ConData.PaymentStatus paymentStatus = default(DollyHotel.Server.Models.ConData.PaymentStatus))
        {
            var uri = new Uri(baseUri, $"PaymentStatuses({paymentStatusId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

          

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(paymentStatus), Encoding.UTF8, "application/json");

            OnUpdatePaymentStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRoomBookingDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roombookingdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roombookingdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRoomBookingDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roombookingdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roombookingdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRoomBookingDetails(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomBookingDetail>> GetRoomBookingDetails(Query query)
        {
            return await GetRoomBookingDetails(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomBookingDetail>> GetRoomBookingDetails(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RoomBookingDetails");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoomBookingDetails(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomBookingDetail>>(response);
        }

        partial void OnCreateRoomBookingDetail(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.RoomBookingDetail> CreateRoomBookingDetail(DollyHotel.Server.Models.ConData.RoomBookingDetail roomBookingDetail = default(DollyHotel.Server.Models.ConData.RoomBookingDetail))
        {
            var uri = new Uri(baseUri, $"RoomBookingDetails");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roomBookingDetail), Encoding.UTF8, "application/json");

            OnCreateRoomBookingDetail(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.RoomBookingDetail>(response);
        }

        partial void OnDeleteRoomBookingDetail(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRoomBookingDetail(long bookingDetailsId = default(long))
        {
            var uri = new Uri(baseUri, $"RoomBookingDetails({bookingDetailsId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRoomBookingDetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRoomBookingDetailByBookingDetailsId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.RoomBookingDetail> GetRoomBookingDetailByBookingDetailsId(string expand = default(string), long bookingDetailsId = default(long))
        {
            var uri = new Uri(baseUri, $"RoomBookingDetails({bookingDetailsId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoomBookingDetailByBookingDetailsId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.RoomBookingDetail>(response);
        }

        partial void OnUpdateRoomBookingDetail(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRoomBookingDetail(long bookingDetailsId = default(long), DollyHotel.Server.Models.ConData.RoomBookingDetail roomBookingDetail = default(DollyHotel.Server.Models.ConData.RoomBookingDetail))
        {
            var uri = new Uri(baseUri, $"RoomBookingDetails({bookingDetailsId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

        

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roomBookingDetail), Encoding.UTF8, "application/json");

            OnUpdateRoomBookingDetail(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRoomBookingsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roombookings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roombookings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRoomBookingsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roombookings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roombookings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRoomBookings(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomBooking>> GetRoomBookings(Query query)
        {
            return await GetRoomBookings(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomBooking>> GetRoomBookings(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RoomBookings");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoomBookings(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomBooking>>(response);
        }

        partial void OnCreateRoomBooking(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.RoomBooking> CreateRoomBooking(DollyHotel.Server.Models.ConData.RoomBooking roomBooking = default(DollyHotel.Server.Models.ConData.RoomBooking))
        {
            var uri = new Uri(baseUri, $"RoomBookings");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roomBooking), Encoding.UTF8, "application/json");

            OnCreateRoomBooking(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.RoomBooking>(response);
        }

        partial void OnDeleteRoomBooking(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRoomBooking(long bookingId = default(long))
        {
            var uri = new Uri(baseUri, $"RoomBookings({bookingId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRoomBooking(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRoomBookingByBookingId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.RoomBooking> GetRoomBookingByBookingId(string expand = default(string), long bookingId = default(long))
        {
            var uri = new Uri(baseUri, $"RoomBookings({bookingId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoomBookingByBookingId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.RoomBooking>(response);
        }

        partial void OnUpdateRoomBooking(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRoomBooking(long bookingId = default(long), DollyHotel.Server.Models.ConData.RoomBooking roomBooking = default(DollyHotel.Server.Models.ConData.RoomBooking))
        {
            var uri = new Uri(baseUri, $"RoomBookings({bookingId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

     

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roomBooking), Encoding.UTF8, "application/json");

            OnUpdateRoomBooking(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRoomStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRoomStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRoomStatuses(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomStatus>> GetRoomStatuses(Query query)
        {
            return await GetRoomStatuses(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomStatus>> GetRoomStatuses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RoomStatuses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoomStatuses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomStatus>>(response);
        }

        partial void OnCreateRoomStatus(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.RoomStatus> CreateRoomStatus(DollyHotel.Server.Models.ConData.RoomStatus roomStatus = default(DollyHotel.Server.Models.ConData.RoomStatus))
        {
            var uri = new Uri(baseUri, $"RoomStatuses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roomStatus), Encoding.UTF8, "application/json");

            OnCreateRoomStatus(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.RoomStatus>(response);
        }

        partial void OnDeleteRoomStatus(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRoomStatus(int roomStatusId = default(int))
        {
            var uri = new Uri(baseUri, $"RoomStatuses({roomStatusId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRoomStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRoomStatusByRoomStatusId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.RoomStatus> GetRoomStatusByRoomStatusId(string expand = default(string), int roomStatusId = default(int))
        {
            var uri = new Uri(baseUri, $"RoomStatuses({roomStatusId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoomStatusByRoomStatusId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.RoomStatus>(response);
        }

        partial void OnUpdateRoomStatus(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRoomStatus(int roomStatusId = default(int), DollyHotel.Server.Models.ConData.RoomStatus roomStatus = default(DollyHotel.Server.Models.ConData.RoomStatus))
        {
            var uri = new Uri(baseUri, $"RoomStatuses({roomStatusId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

             

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roomStatus), Encoding.UTF8, "application/json");

            OnUpdateRoomStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRoomTypeFacilitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomtypefacilities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomtypefacilities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRoomTypeFacilitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomtypefacilities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomtypefacilities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRoomTypeFacilities(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomTypeFacility>> GetRoomTypeFacilities(Query query)
        {
            return await GetRoomTypeFacilities(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomTypeFacility>> GetRoomTypeFacilities(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RoomTypeFacilities");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoomTypeFacilities(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomTypeFacility>>(response);
        }

        partial void OnCreateRoomTypeFacility(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.RoomTypeFacility> CreateRoomTypeFacility(DollyHotel.Server.Models.ConData.RoomTypeFacility roomTypeFacility = default(DollyHotel.Server.Models.ConData.RoomTypeFacility))
        {
            var uri = new Uri(baseUri, $"RoomTypeFacilities");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roomTypeFacility), Encoding.UTF8, "application/json");

            OnCreateRoomTypeFacility(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.RoomTypeFacility>(response);
        }

        partial void OnDeleteRoomTypeFacility(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRoomTypeFacility(long roomTypeFacilityId = default(long))
        {
            var uri = new Uri(baseUri, $"RoomTypeFacilities({roomTypeFacilityId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRoomTypeFacility(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRoomTypeFacilityByRoomTypeFacilityId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.RoomTypeFacility> GetRoomTypeFacilityByRoomTypeFacilityId(string expand = default(string), long roomTypeFacilityId = default(long))
        {
            var uri = new Uri(baseUri, $"RoomTypeFacilities({roomTypeFacilityId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoomTypeFacilityByRoomTypeFacilityId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.RoomTypeFacility>(response);
        }

        partial void OnUpdateRoomTypeFacility(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRoomTypeFacility(long roomTypeFacilityId = default(long), DollyHotel.Server.Models.ConData.RoomTypeFacility roomTypeFacility = default(DollyHotel.Server.Models.ConData.RoomTypeFacility))
        {
            var uri = new Uri(baseUri, $"RoomTypeFacilities({roomTypeFacilityId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

        

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roomTypeFacility), Encoding.UTF8, "application/json");

            OnUpdateRoomTypeFacility(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRoomTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomtypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomtypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRoomTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/roomtypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/roomtypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRoomTypes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomType>> GetRoomTypes(Query query)
        {
            return await GetRoomTypes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomType>> GetRoomTypes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"RoomTypes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoomTypes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.RoomType>>(response);
        }

        partial void OnCreateRoomType(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.RoomType> CreateRoomType(DollyHotel.Server.Models.ConData.RoomType roomType = default(DollyHotel.Server.Models.ConData.RoomType))
        {
            var uri = new Uri(baseUri, $"RoomTypes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roomType), Encoding.UTF8, "application/json");

            OnCreateRoomType(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.RoomType>(response);
        }

        partial void OnDeleteRoomType(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRoomType(long roomTypeId = default(long))
        {
            var uri = new Uri(baseUri, $"RoomTypes({roomTypeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRoomType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRoomTypeByRoomTypeId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.RoomType> GetRoomTypeByRoomTypeId(string expand = default(string), long roomTypeId = default(long))
        {
            var uri = new Uri(baseUri, $"RoomTypes({roomTypeId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoomTypeByRoomTypeId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.RoomType>(response);
        }

        partial void OnUpdateRoomType(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRoomType(long roomTypeId = default(long), DollyHotel.Server.Models.ConData.RoomType roomType = default(DollyHotel.Server.Models.ConData.RoomType))
        {
            var uri = new Uri(baseUri, $"RoomTypes({roomTypeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

         

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roomType), Encoding.UTF8, "application/json");

            OnUpdateRoomType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportSearchesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/searches/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/searches/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSearchesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/searches/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/searches/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetSearches(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.Search>> GetSearches(Query query)
        {
            return await GetSearches(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.Search>> GetSearches(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Searches");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSearches(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<DollyHotel.Server.Models.ConData.Search>>(response);
        }

        partial void OnCreateSearch(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.Search> CreateSearch(DollyHotel.Server.Models.ConData.Search search = default(DollyHotel.Server.Models.ConData.Search))
        {
            var uri = new Uri(baseUri, $"Searches");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(search), Encoding.UTF8, "application/json");

            OnCreateSearch(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.Search>(response);
        }

        partial void OnDeleteSearch(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteSearch(long searchId = default(long))
        {
            var uri = new Uri(baseUri, $"Searches({searchId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSearch(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetSearchBySearchId(HttpRequestMessage requestMessage);

        public async Task<DollyHotel.Server.Models.ConData.Search> GetSearchBySearchId(string expand = default(string), long searchId = default(long))
        {
            var uri = new Uri(baseUri, $"Searches({searchId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSearchBySearchId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<DollyHotel.Server.Models.ConData.Search>(response);
        }

        partial void OnUpdateSearch(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateSearch(long searchId = default(long), DollyHotel.Server.Models.ConData.Search search = default(DollyHotel.Server.Models.ConData.Search))
        {
            var uri = new Uri(baseUri, $"Searches({searchId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

               

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(search), Encoding.UTF8, "application/json");

            OnUpdateSearch(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}