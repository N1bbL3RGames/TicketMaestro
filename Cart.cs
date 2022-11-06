//ALICIA MONCRIEF

/*
* Unit Testing: 	Tested creating and adding a ticket
*			Tested removing the ticket
*			Tested for invalid payment methods
*/

//cart class for the shopping cart
class Cart
{
	  //attributes: list of ticket in cart, and chosen payment method
        public List<Ticket> ticketList;
        public PaymentMethod method;

	  //default constructor
        public Cart()
        {
            ticketList = new List<Ticket>();
            method = new PaymentMethod();
        }

	  //if cart had tickets previously, but no chosen payment method
	  //rebuild cart with previous tickets and default payment method
        public Cart(List<Ticket> prevTickets)
        {
            ticketList = prevTickets;
            method = new PaymentMethod();
        }

	  //if cart had a chosen payment method previously, but no tickets
	  //rebuild cart with empty ticket list and previously chosen payment method
        public Cart(string method)
        {
            ticketList = new List<Ticket>();
            this.method = new PaymentMethod(method);
        }
	  
	  //if cart had a chosen payment method and tickets previously
	  //rebuild cart using previous ticket list and chosen payment method
        public Cart(List<Ticket> prevTickets, string method)
        {
            ticketList = prevTickets;
            this.method = new PaymentMethod(method);
        }
	  
	  //add a ticket to the cart
        public void addTicket(Ticket ticket)
        {
            ticketList.Add(ticket);
        }
        
	  //remove a ticket from the cart
        public void removeTicket(Ticket ticket)
        {
            ticketList.Remove(ticket);
        }

	  //when ready for payment, initiate secure third party with payment method
	  //and tickets in cart
        public void payForTickets(PaymentMethod method, List<Ticket> ticketList)
        {
            //navigate to third party payment handler
        }
}

//payment method class, only certain options allowed
class PaymentMethod
{
	  //attributes, chosen method and options available
        String[] options = {"Debit Card", "Credit Card", "GooglePay", "SamsungPay", "AndroidPay"};
        string method;
        
 	  //default constructor
        public PaymentMethod()
        {
            method = "Debit Card";
        }

	  //constructor for a selected payment method
        public PaymentMethod(string method)
        {   
            if(options.Contains(method))
                this.method = method;
            else
                Console.WriteLine("Invalid Payment Method");
        }
}