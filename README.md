# FeastFreedom

<p>FeastFreedom is a food service site for where restaurants upload menu items and users can order food off these local menus. 
Users and Restaurants need to register and log in to use FeastFreedom services.</p>

<p>Unfortunately, website only exists on localhost for right now. </p>

## Built With 
C#, MVC, Entity Framework, Bootstrap, CSHTML, CSS, SQL Server


<img src="project-images/FeastFreedomHomePage.jpg" width="100%">

<p>Restaurant registeration requires the Restaurant Name, Email, Password for account, Checkbox for Days Open, Hours Open, and an Image. 
After registration is complete, the restaurant is able to add multiple menu items to their menu.</p>

<p>The images that the restaurants upload are saved into a folder within the application.</p>

```
if (file != null)
      {
             kitchen.Image = System.IO.Path.GetFileName(file.FileName);
             string physicalPath = Server.MapPath("~/Images/" + kitchen.Image);

             file.SaveAs(physicalPath);
      }
```


<img src="project-images/RestaurantRegistrationWithImage.jpg" width="100%" alt="Restaurant Registration">
<img src="project-images/RestaurantRegisteredAddMenuItem.jpg" width="100%">
<img src="project-images/AddedMenuItem.jpg" width="100%">

<p>The Restaurants can always access their account by logging in with their Restaurat Name and Password.
By logging in, they are able to view their menu, add more items to their menu, as well as delete items from their menu.</p>

<img src="project-images/RestaurantLoginPage.jpg" width="100%">
<img src="project-images/RestaurantLoggedIn.jpg" width="100%">
<img src="project-images/RestaurantMenuViewAddDelete.jpg" width="100%">

<p>User is required to register/login with their account before they can use the FeastFreedom services 
of viewing restaurants in the area and ordering off of their menus.</p>

<p>User must provide their First Name, Last Name, Email, Password, and two Security Passwords.</p>

<p>After a user registers, they are informed of a successful registration and brought to the login page. 
Once the user logs in, they are able to view all the restaurants provided. When they click on the menu,
they are able to see the restuarant's menu as well as add menu items to their cart. 
When they finish their order, they click on the cart with the number of items 
that they placed in their cart to redirect them to the order page 
where they can review their order and delete any items if they changed their minds. </p>

<img src="project-images/UserRegistration.jpg" width="100%">
<img src="project-images/UserRegisteredSuccess.jpg" width="100%">

<img src="project-images/UserLoggedInHome.jpg" width="100%">
<img src="project-images/UserViewOfRestaurantMenu.jpg" width="100%">

<img src="project-images/UserOrder.jpg" width="100%">
<img src="project-images/UserConfirmOrder.jpg" width="100%">
<img src="project-images/UserOrderPlaced.jpg" width="100%">

<p>Once an order is placed, an email is sent to both user and restaurant.</p>


```
//Configuring webMail class to send emails 
//gmail smtp server 
       WebMail.SmtpServer = "smtp.gmail.com";
       //gmail port to send emails 
       WebMail.SmtpPort = 587;
       WebMail.SmtpUseDefaultCredentials = false;
       //sending emails with secure protocol 
       WebMail.EnableSsl = true;
       //EmailId used to send emails from application 
       WebMail.UserName = "feastfreedom2@gmail.com";
       WebMail.Password = ""; //password goes here

       //Sender email address. 
       WebMail.From = "feastfreedom2@gmail.com";

                    

       //Send email 
       WebMail.Send(to: userEmail, subject: "Feast Freedom Order", body: "Thank you for placing an order with " + kitchenName + ".");
       WebMail.Send(to: kitchenEmail, subject: "Feast Freedom Order", body: "A customer has placed an order at your restaurant.");
       ViewBag.Status = "Email Sent Successfully.";

```





<img src="project-images/EmailToRestaurant.jpg" width="100%">
<img src="project-images/EmailToUser.jpg" width="100%">

## Future Development
<ul>
<li>Allow Restaurants to edit their menu items</li>
<li>Add Paypal payment option</li>
<li>Make emails include orders information such as quantities, menu items, price total </li>
<li>Fix design and any lingering bugs</li>
</ul>

### Credits
Olivia Kalinowski and David Zhang