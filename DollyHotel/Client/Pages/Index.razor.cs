using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using DollyHotel.Server.Models.ConData;

namespace DollyHotel.Client.Pages
{
    public partial class Index
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
        public ConDataService ConDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            search = new DollyHotel.Server.Models.ConData.Search();
            await SetDefaultSearchParameters();
            //var result = await ConDataService.GetCities();
            //citiesForLocationID = result.Value.AsODataEnumerable();

        }

        private async Task SetDefaultSearchParameters()
        {
            search.LocationID = 1;
            search.NumberOfAdults = 2;
            search.NumberOfChildren = 0;
            search.NumberOfRooms = 1;
            search.CheckInDate = DateTime.Now.AddDays(1);//check in date is set to tomorrow by default
            search.CheckOutDate = search.CheckInDate.AddDays(1);///check out date is set to two days from now by default
            search.SearchDate = DateTime.Now;
        }
        protected bool errorVisible;
        protected DollyHotel.Server.Models.ConData.Search search;

        protected IEnumerable<DollyHotel.Server.Models.ConData.City> citiesForLocationID;

        protected IEnumerable<DollyHotel.Server.Models.ConData.AspNetUser> aspNetUsersForSearchedBy;


        protected int citiesForLocationIDCount;
        protected DollyHotel.Server.Models.ConData.City citiesForLocationIDValue;

        //method to load city dropdown
        protected async Task citiesForLocationIDLoadData(LoadDataArgs args)
        {
            try
            {
                //fetching cities from data service
                ODataServiceResult<City> result = await ConDataService.GetCities(top: args.Top, skip: args.Skip, count: args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                citiesForLocationID = result.Value.AsODataEnumerable();//convert from ODataServiceResult<City> to IEnumerable<City>
                citiesForLocationIDCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = ex.InnerException.Message });
            }
        }


        protected async Task FormSubmit()
        {
            try
            {
                if(search.CheckInDate >=search.CheckOutDate)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Input Validation Error!", "Check In Date Can Not Be After Check Out Date!");
                    return;
                }
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


        protected bool hasChanges = false;
        protected bool canEdit = true;
    }
}
