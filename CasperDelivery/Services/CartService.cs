using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces;
using CasperDelivery.Interfaces.Specifications;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http.Json;
using Blazored.Toast.Services;
using CasperDelivery.Interfaces.Repositories;


namespace CasperDelivery.Services
{
    public class CartService : ICartService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IGenericRepository<Products> _productsRepo;
        private readonly IGenericRepository<Restaurants> _restRepo;
        private readonly IGenericRepository<Basket> _basketRepo;
        private readonly ICartRepository _cartRepository;
        private readonly IGenericRepository<BasketItem> _basketItemRepo;
        private readonly HttpClient _http;
        private readonly IToastService _toastService;

        public CartService(AuthenticationStateProvider authenticationStateProvider,
                            IGenericRepository<Products> productsRepo,
                            IGenericRepository<Restaurants> restRepo,
                            IGenericRepository<Basket> basketRepo,
                            ICartRepository cartRepository,
                            IGenericRepository<BasketItem> basketItemRepo,
                            HttpClient http,
                            IToastService toastService)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _productsRepo = productsRepo;
            _restRepo = restRepo;
            _basketRepo = basketRepo;
            _cartRepository = cartRepository;
            _basketItemRepo = basketItemRepo;
            _http = http;
            _toastService = toastService;
        }


        public async Task AddItemToCart(int id)
        {
            try
            {
                var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authenticationState.User;
                if (user.Identity != null && !user.Identity.IsAuthenticated) return;

                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                var basket = await _cartRepository.GetBasketByUserId(userId);
                var spec = new GetBasketWithItemsSpecification(1);
                var _basketItems = await _basketItemRepo.ListAsync(spec);
                var biIems = _basketItems.ToList();

                var existingItem = biIems.FirstOrDefault(x => x.ProductId == id);
                if (existingItem == null)
                {
                    biIems.Add(new BasketItem
                    {
                        BasketId = basket.Id,
                        ProductId = id,
                        Quantity = 1
                    });
                }
                else
                {
                    existingItem.Quantity += 1;
                }

                await _basketItemRepo.UpdateAsync(biIems);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _toastService.ShowError("Failed to add product to the cart");
            }
        }

        public async Task DeleteOneItemFromCart(int id)
        {
            try
            {
                var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authenticationState.User;
                if (user.Identity != null && !user.Identity.IsAuthenticated) return;

                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                var basketSpec = new GetBasketSpecification(userId);
                var basket = await _basketRepo.GetEntityWithSpecAsync(basketSpec)
                             ?? throw new Exception($"We couldn't find user basket with user id {userId}");

                var itemToDelete = basket.Items.FirstOrDefault(x => x.ProductId == id);
                if (itemToDelete != null && itemToDelete.Quantity > 1)
                {
                    itemToDelete.Quantity -= 1;
                }
                else
                {
                    if (itemToDelete != null) await _basketItemRepo.DeleteAsync(itemToDelete.Id);
                }
                await _basketRepo.UpdateAsync(basket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task<Restaurants> GetRestaurantInfo(int id)
        {
            GetRestaurantWithProductSpecification spec = new GetRestaurantWithProductSpecification(id);
            var restaurant = await _restRepo.GetEntityWithSpecAsync(spec);
            return restaurant;
        }

        public async Task<string> Checkout(int basketId, string userId)
        {
            var spec = new GetBasketWithItemsSpecification(basketId);
            var items = (await _basketItemRepo.ListAsync(spec)).ToList();
            var data = new CheckoutPostData
            {
                UserId = userId,
                Items = items
            };

            var response = await _http.PostAsJsonAsync("https://localhost:5001/payment/checkout", data);

            var url = await response.Content.ReadAsStringAsync();
            return url;
        }

    }
}
