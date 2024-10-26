# About app
This is a simulation of a web store with functionalities such as: 
- Login
- Register with email verification
- Profile management
- Adding products to cart
- Removing products from cart
- Applying promocodes
- Leaving reviews
- Making payments
- Viewing order history

The app comprises two roles: "User" and "Admin". Upon registration, each new account is automatically assigned the "User" role. The "Admin" role is directly assigned from the database, granting administrators access to a distinct layout on the website compared to regular users. Admins possess full CRUD (Create, Read, Update, Delete) capabilities over Products, Users, Promocodes, and Categories through their dedicated administration panel.

# Hosting
The app is hosted here: [https://techstoreweb.azurewebsites.net](https://techstoreweb.azurewebsites.net)
> [!NOTE]
> If you want to run the app locally, you would have to add a connection string and do a migration.
