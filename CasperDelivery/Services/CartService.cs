using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces;
using CasperDelivery.Interfaces.Specifications;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http.Json;


namespace CasperDelivery.Services
{
    public class CartService : ICartService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IGenericRepository<Products> _productsRepo;
        private readonly IGenericRepository<Restaurants> _restRepo;
        private readonly IGenericRepository<Basket> _basketRepo;
        private readonly IGenericRepository<BasketItem> _basketItemRepo;
        private readonly HttpClient _http;

        public CartService(AuthenticationStateProvider authenticationStateProvider,
                            IGenericRepository<Products> productsRepo,
                            IGenericRepository<Restaurants> restRepo,
                            IGenericRepository<Basket> basketRepo,
                            IGenericRepository<BasketItem> basketItemRepo,
                            HttpClient http)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _productsRepo = productsRepo;
            _restRepo = restRepo;
            _basketRepo = basketRepo;
            _basketItemRepo = basketItemRepo;
            _http = http;
        }


        public async Task AddItemToCart(int id)
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

                var existingItem = basket.Items.FirstOrDefault(x => x.ProductId == id);
                if (existingItem == null)
                {
                    basket.Items.Add(new BasketItem
                    {
                        Id = basket.Id,
                        Product = await _productsRepo.GetOneAsync(id),
                        Quantity = 1
                    });
                }
                else
                {
                    existingItem.Quantity += 1;
                }

                await _basketRepo.UpdateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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

                await _basketRepo.UpdateAsync();
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
