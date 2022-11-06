//COLE ELFSTROM

public class Plan
{
    public int day;
    public int month;
    public int year;
    public List<Ticket> purchasedTickets;

    public Plan(int d, int m, int y)
    {
        this.day = d;
        this.month = m;
        this.year = y;
    }

    public List<Ticket> getPurchasedTickets()
    {
        purchasedTickets = Tickets.getTickets().applySearchFilters(TicketState.purchased);
    }
}