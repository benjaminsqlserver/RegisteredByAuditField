using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Components.Authorization;

namespace DollyHotel.Client.Pages
{
    public partial class AddHotel
    {
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
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
            // hotel = new DollyHotel.Server.Models.ConData.Hotel();
            hotel = new DollyHotel.Server.Models.ConData.Hotel { HotelFacilities = null, HotelRooms = null, HotelType = null, RoomBookings = null, RoomTypeFacilities = null, RoomTypes = null };
        }
        protected bool errorVisible;
        protected DollyHotel.Server.Models.ConData.Hotel hotel;

        protected IEnumerable<DollyHotel.Server.Models.ConData.HotelType> hotelTypesForHotelTypeID;

        protected IEnumerable<DollyHotel.Server.Models.ConData.City> citiesForLocationID;


        protected int hotelTypesForHotelTypeIDCount;
        protected DollyHotel.Server.Models.ConData.HotelType hotelTypesForHotelTypeIDValue;
        protected async Task hotelTypesForHotelTypeIDLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await ConDataService.GetHotelTypes();
                hotelTypesForHotelTypeID = result.Value.AsODataEnumerable();
                hotelTypesForHotelTypeIDCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load HotelType" });
            }
        }

        protected int citiesForLocationIDCount;
        protected DollyHotel.Server.Models.ConData.City citiesForLocationIDValue;
        protected async Task citiesForLocationIDLoadData(LoadDataArgs args)
        {
            try
            {
                
                var result = await ConDataService.GetCities();
                citiesForLocationID = result.Value.AsODataEnumerable();
                citiesForLocationIDCount = result.Count;

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load City" });
               // NotificationService.Notify(NotificationSeverity.Error, "Hotel Create Error!", ex.Message, 3600000);
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var authenticateState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                if(authenticateState.User.Identity.IsAuthenticated)
                {
                    hotel.RegisteredBy = authenticateState.User.Identity.Name;

                    if (hotel.LocationID < 1)
                    {
                        NotificationService.Notify(NotificationSeverity.Error, "City Validation Error!", "City Is Required!", 7000);
                        return;
                    }
                    else if (hotel.HotelTypeID < 1)
                    {
                        NotificationService.Notify(NotificationSeverity.Error, "Hotel Type Validation Error!", "Hotel Type Is Required!", 7000);
                        return;
                    }
                    else
                    {
                        var result = await ConDataService.CreateHotel(hotel);
                        //reset hotel page after creation
                        hotel = new DollyHotel.Server.Models.ConData.Hotel { HotelFacilities = null, HotelRooms = null, HotelType = null, RoomBookings = null, RoomTypeFacilities = null, RoomTypes = null };


                        // DialogService.Close(hotel);
                    }
                }
                else
                {
                    NavigationManager.NavigateTo("/Identity/Account/Login", true);
                }

               

            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Hotel Create Error!", ex.Message, 3600000);
                //errorVisible = true;
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