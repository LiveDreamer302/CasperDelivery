
# Casper Delivery Blazor App

This project is an exciting e-commerce Blazor application, designed as a dynamic food delivery site created for a university project. It features robust user authentication, seamless browsing of products from various restaurants, and a convenient basket system for easy ordering. The checkout process is powered by Stripe, ensuring secure and efficient transactions. Dive into the world of online food delivery with my innovative app!



## Run my App

Requirements: Microsoft SQL Server, .NET SDK (utilized version .NET 7), installation of the Stripe CLI and Git CLI.


### First Step - Clone my repository
```bash
  git clone https://github.com/LiveDreamer302/CasperDelivery.git
```
### Second Step - Change SQL connection and Stripe Webhook

Open appsettings.Development.json.

```json
{
  "DetailedErrors": true,
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection" : "{Your Server Name};Database=CasperDeliveryDb;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "StripeWh": "{Your Webhook Secret}"
}
```
#### For SQL connection 
Modify "{Your Server Name}" with your Microsoft SQL Server name.

#### For Stripe you should run  (Ensure that you are in the directory containing CasperDelivery.csproj.)
```bash
  stripe login
```

After a successful login, execute the following command:
```bash
  stripe listen -f https://localhost:5001/payment/webhook -e payment_intent.succeeded,payment_intent.payment_failed,checkout.session.completed
```
And modify "{Your Webhook Secret}" with the whsecret given to you by Stripe

### Third Step - Run my Blazor app

From same directory where you run stripe commands run following command:

```bash
  dotnet run
```

## Your app should now be up and running. Enjoy!
## Authors

- [@Stefan Manoil](https://github.com/LiveDreamer302)

