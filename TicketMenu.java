//CARTER LEE

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Scanner;

public class TicketMenu {
    public ArrayList<String> agencies;
    public List<Ticket> tickets;

    public TicketMenu(){
        agencies = new ArrayList<>();
        tickets = new ArrayList<>();

//        tickets.add(new Ticket(1, "Dart") );
//        tickets.add(new Ticket(2, "Greyhound") );
//        agencies.add("Dart");
//        agencies.add("Greyhound");
    }

    public void applySearchFilters(){
        Scanner scan = new Scanner(System.in);
        System.out.println("Input desired filter");
        String filter = scan.next();

        if(filter.compareTo("Cheapest") == 0){
            Collections.sort(tickets);

        }else if(filter.compareTo("Expensive") == 0){
            Collections.sort(tickets);
            Collections.reverse(tickets);
        }else{
            System.out.println("Invalid filter");
        }
        scan.close();
        

    }

    public List<Ticket> getTicket(){
        return tickets;
    }

    public void cameraQRCode(){
        // camera screen should pop up
    }

//    public void addTicketToCart(Cart myCart. Ticket t){
//        myCart.ticketList.add(t);
//    }

}


public class Ticket implements Comparable<Ticket> {
    public int price;
    public String agency;

    public Ticket(int price, String agency) {
        this.price = price;
        this.agency = agency;
    }


    @Override
    public int compareTo(Ticket t) {
        if (this.price > t.price) {
            return 1;
        } else if (this.price < t.price) {
            return -1;
        } else
            return 0;
    }

    public int getPrice() {
        return price;
    }

    public String getAgency() {
        return agency;
    }
}
