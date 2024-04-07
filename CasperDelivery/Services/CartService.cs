using CasperDelivery.Data.Models;
using CasperDelivery.Interfaces;
using CasperDelivery.Interfaces.Specifications;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;


namespace CasperDelivery.Services
{
    public class CartService : ICartService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IGenericRepository<Products> _productsRepo;
        private readonly IGenericRepository<Restaurants> restRepo;
        private readonly IGenericRepository<Basket> _basketRepo;

        public CartService(AuthenticationStateProvider authenticationStateProvider,
                            IGenericRepository<Products> productsRepo,
                            IGenericRepository<Restaurants> RestRepo,
                            IGenericRepository<Basket> basketRepo)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _productsRepo = productsRepo;
            restRepo = RestRepo;
            _basketRepo = basketRepo;
        }


        public async Task AddItemToCart(int id)
        {
            try
            {
                var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authenticationState.User;
                if (!user.Identity.IsAuthenticated) return;

                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                var basketSpec = new GetBasketWithItemsSpecification(userId);
                var basket = await _basketRepo.GetEntityWithSpecAsync(basketSpec)
                    ?? throw new Exception($"We couldn't find user basket with user id {userId}");

                var existingItem = basket.Items.FirstOrDefault(x => x.ProductId == id);
                if (existingItem == null)
                {
                    basket.Items.Add(new BasketItem
                    {
                        BasketId = basket.Id,
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

        public async Task<Restaurants> GetRestaurantInfo(int id)
        {
            Restaurants Restaurant;
            GetRestaurantWithProductSpecification spec = new GetRestaurantWithProductSpecification(id);
            Restaurant = await restRepo.GetEntityWithSpecAsync(spec);
            return Restaurant;
        }
    }
}
